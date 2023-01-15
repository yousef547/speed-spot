using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using portal.speedspot.BizLayer.BizMethods;
using portal.speedspot.BizLayer.IdentityModule;
using portal.speedspot.Models.ViewModels;
using portal.speedspot.WebUI.Controllers.Infrastructure;
using portal.speedspot.WebUI.Infrastracture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static portal.speedspot.Models.Constants.ApplicationEnums;

namespace portal.speedspot.WebUI.Controllers
{
    public class NotificationsController : BaseController
    {
        private readonly NotificationUsersBiz _notificationUsersBiz;
        private readonly IHubNotificationHelper _hubNotificationHelper;
        private readonly TasksBiz _tasksBiz;
        private readonly GeneralRequestsBiz _generalRequestsBiz;
        public NotificationsController(NotificationUsersBiz notificationUsersBiz,
            IHubNotificationHelper hubNotificationHelper,
            GeneralRequestsBiz generalRequestsBiz,
        ApplicationUserManager userManager,
            IMapper mapper, TasksBiz tasksBiz) : base(mapper, userManager)
        {
            _notificationUsersBiz = notificationUsersBiz;
            _hubNotificationHelper = hubNotificationHelper;
            _tasksBiz = tasksBiz;
            _generalRequestsBiz = generalRequestsBiz;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetNotifications(bool? isRead = null, int offset = 0)
        {
            List<NotificationUserViewModel> model;
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var notifications = await _notificationUsersBiz.GetUserNotificationsAsync(user.Id, isRead);
            model = _mapper.Map<List<NotificationUserViewModel>>(notifications).ToList();
            model.ForEach(x => x.CreatedDate = x.CreatedDate.AddMinutes(-offset));

            return PartialView("_NotificationsPartial", model);
        }

        public async Task<IActionResult> GetNotificationsById(int id)
        {
            var notification = await _notificationUsersBiz.GetByIdAsync(id);
            if (notification == null)
            {
                return NotFound();
            }
            NotificationUserViewModel resultNotification = _mapper.Map<NotificationUserViewModel>(notification);
            return PartialView("_ShowNotificationModelPartial", resultNotification);
        }

        public IActionResult ReloadViewComponent(int offset = 0)
        {
            return ViewComponent("Notifications", new { offset });
        }

        public async Task<IActionResult> IsRead(bool isRead, int notificationId)
        {
            var notification = await _notificationUsersBiz.GetByIdAsync(notificationId);
            if (notification == null)
            {
                return NotFound();
            }
            var result = await _notificationUsersBiz.UpdateIsRead(notification.Id, isRead);
            return Json(result);
        }

        public async Task<IActionResult> TaskReminder(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var task = await _tasksBiz.GetByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            else
            {
                NotificationViewModel notificationViewModel = new NotificationViewModel
                {
                    CreatedById = user.Id,
                    CreatedDate = DateTime.UtcNow,
                    Type = NotificationTypeEnum.Task_Reminder,
                    Route = "/",
                    Details = task.Description,
                    NotificationUserVMs = new List<NotificationUserViewModel>
                    {
                        new NotificationUserViewModel
                        {
                            SendToId = task.AssignedToId,
                            IsRead = false,
                        }
                    }
                };

                await _hubNotificationHelper.SendNotification(notificationViewModel);
            }
            return Ok(task);
        }

        public async Task<IActionResult> RequestReminder(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var request = await _generalRequestsBiz.GetByIdAsync(id);
            if (request == null)
            {
                return NotFound();
            }
            else
            {
                NotificationViewModel notificationViewModel = new NotificationViewModel
                {
                    CreatedById = user.Id,
                    CreatedDate = DateTime.UtcNow,
                    Type = NotificationTypeEnum.Request_Reminder,
                    Route = "/",
                    Details = request.Description,
                    NotificationUserVMs = new List<NotificationUserViewModel>
                    {
                        new NotificationUserViewModel
                        {
                            SendToId = request.RequestFromId,
                            IsRead = false,
                        }
                    }
                };
                await _hubNotificationHelper.SendNotification(notificationViewModel);
            }
            return Ok(request);
        }

        public async Task<IActionResult> GetNotificationsNotDelivered()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var NotificationDelivered = await _notificationUsersBiz.GetUserNotificationsNotDeliveredAsync(user.Id);
            return Json(new
            {
                notification_count = NotificationDelivered.Count,
            });

        }

        public async Task<IActionResult> updateDelivered()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var NotificationDelivered = await _notificationUsersBiz.UpdateAllIsDelivered(user.Id);
            return Ok(NotificationDelivered);
        }
    }
}

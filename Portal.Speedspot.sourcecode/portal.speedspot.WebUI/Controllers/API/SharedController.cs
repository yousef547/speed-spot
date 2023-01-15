using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using portal.speedspot.BizLayer.BizMethods;
using portal.speedspot.BizLayer.IdentityModule;
using portal.speedspot.BizLayer.Services;
using portal.speedspot.Models.Concretes;
using portal.speedspot.Models.ViewModels;
using portal.speedspot.WebUI.Controllers.Infrastructure;
using portal.speedspot.WebUI.Helpers;
using portal.speedspot.WebUI.Hubs;
using portal.speedspot.WebUI.Infrastracture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static portal.speedspot.Models.Constants.ApplicationEnums;

namespace portal.speedspot.WebUI.Controllers.API
{
    [Route("api/[controller]")]
    public class SharedController : BaseAPIController
    {
        private readonly TasksBiz _tasksBiz;
        private readonly GeneralRequestsBiz _generalRequestsBiz;
        private readonly IHubNotificationHelper _hubNotificationHelper;
        private readonly StickyNotesBiz _stickyNotesBiz;

        public SharedController(TasksBiz tasksBiz,
            GeneralRequestsBiz generalRequestsBiz,
            IHubNotificationHelper hubNotificationHelper,
            IMapper mapper,
            ApplicationUserManager userManager, StickyNotesBiz stickyNotesBiz) : base(mapper, userManager)
        {
            _tasksBiz = tasksBiz;
            _generalRequestsBiz = generalRequestsBiz;
            _hubNotificationHelper = hubNotificationHelper;
            _stickyNotesBiz = stickyNotesBiz;
        }

        [HttpPost("CreateTask")]
        public async Task<IActionResult> CreateTask([FromForm] TaskViewModel model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            model.CreatedDate = DateTime.UtcNow;
            model.CreatedById = user.Id;
            if (model.IsSavePage)
            {
                model.PagePath = Request.GetTypedHeaders().Referer.AbsolutePath;
            }
            Tasks task = _mapper.Map<Tasks>(model);
            var Result = await _tasksBiz.AddAsync(task, user.Id);
            string requestPath = Request.GetTypedHeaders().Referer.AbsolutePath;
            bool refresh = requestPath.ToLower() == "/";

            if (Result)
            {
                NotificationViewModel notificationViewModel = new NotificationViewModel
                {
                    CreatedById = user.Id,
                    CreatedDate = DateTime.UtcNow,
                    Type = NotificationTypeEnum.Task_New,
                    Route = "/",
                    Details = model.Description,
                    NotificationUserVMs = new List<NotificationUserViewModel>
                    {
                        new NotificationUserViewModel
                        {
                            SendToId = model.AssignedToId,
                            IsRead = false,
                        }
                    }
                };

                await _hubNotificationHelper.SendNotification(notificationViewModel);
            }

            return Ok(new
            {
                result = Result,
                refreshDashboard = refresh
            });
        }

        [HttpPost("EditTask")]
        public async Task<IActionResult> EditTask([FromForm] TaskViewModel model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (model.IsSavePage == false)
            {
                model.PagePath = null;
            }
            Tasks task = _mapper.Map<Tasks>(model);
            bool result = await _tasksBiz.UpdateAsync(task, user.Id);
            return Ok(result);

        }

        [HttpPost("CreateGeneralRequest")]
        public async Task<IActionResult> CreateGeneralRequest([FromForm] GeneralRequestViewModel model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            model.RequestedDate = DateTime.UtcNow;
            model.RequestedById = user.Id;
            model.Status = RequestStatusEnum.Pending;
            GeneralRequest request = _mapper.Map<GeneralRequest>(model);
            var Result = await _generalRequestsBiz.AddAsync(request, user.Id);

            string requestPath = Request.GetTypedHeaders().Referer.AbsolutePath;
            bool refresh = requestPath.ToLower() == "/";
            if (Result)
            {
                NotificationViewModel notificationViewModel = new NotificationViewModel
                {
                    CreatedById = user.Id,
                    CreatedDate = DateTime.UtcNow,
                    Type = NotificationTypeEnum.Request_New,
                    Route = "/",
                    Details = model.Description,
                    NotificationUserVMs = new List<NotificationUserViewModel>
                    {
                        new NotificationUserViewModel
                        {
                            SendToId = model.RequestFromId,
                            IsRead = false,
                        }
                    }
                };

                await _hubNotificationHelper.SendNotification(notificationViewModel);
            }

            return Ok(new
            {
                result = Result,
                refreshDashboard = refresh
            });
        }

        [HttpPost("CreateStickyNote")]
        public async Task<IActionResult> CreateStickyNote([FromForm] StickyNoteViewModel model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            model.CreatedById = user.Id;
            model.CreatedDate = DateTime.UtcNow;
            if (model.IsSaveUrl)
            {
                model.PageUrl = Request.GetTypedHeaders().Referer.AbsolutePath;
            }
            else
            {
                model.PageController = null;
                model.PageAction = null;
                model.PageUrl = null;
            }

            var stickNote = _mapper.Map<StickyNote>(model);
            var Result = await _stickyNotesBiz.AddAsync(stickNote);

            return Ok(new
            {
                result = Result,
            });
        }

        [HttpGet("DeleteStickyNote/{id}")]
        public async Task<IActionResult> DeleteStickyNote(int Id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            var result = await _stickyNotesBiz.DeleteAsync(Id, userId);
            return Ok(new
            {
                result = result,
            });
        }



        //[HttpGet("justify/{value}")]
        //public IActionResult ArabicNumberJustification(decimal value)
        //{
        //    NumberJustificationService numberJustificationService = new(value);

        //    numberJustificationService.Number = value;
        //    string resultar = numberJustificationService.ConvertToArabic();
        //    string result = numberJustificationService.ConvertToEnglish();
        //    return Ok(new
        //    {
        //        Arabic = resultar,
        //        English = result
        //    });
        //}
    }
}

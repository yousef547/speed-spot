using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using portal.speedspot.BizLayer.BizMethods;
using portal.speedspot.BizLayer.IdentityModule;
using portal.speedspot.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.WebUI.ViewComponents
{
    [ViewComponent(Name = "Notifications")]
    public class NotificationsViewComponent : ViewComponent
    {
        private readonly IMapper _mapper;
        private readonly NotificationUsersBiz _notificationUsersBiz;
        private readonly ApplicationUserManager _userManager;

        public NotificationsViewComponent(NotificationUsersBiz notificationUsersBiz,
            IMapper mapper,
            ApplicationUserManager userManager)
        {
            _notificationUsersBiz = notificationUsersBiz;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int offset = 0)
        {
            List<NotificationUserViewModel> model;
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var notifications = await _notificationUsersBiz.GetUserNotificationsAsync(user.Id,false);
            model = _mapper.Map<List<NotificationUserViewModel>>(notifications).ToList();
            model.ForEach(x => x.CreatedDate = x.CreatedDate.AddMinutes(-offset));

            return View(model);

        }
    }
}

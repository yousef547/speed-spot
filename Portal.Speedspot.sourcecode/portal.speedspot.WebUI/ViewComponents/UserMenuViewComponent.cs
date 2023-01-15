using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using portal.speedspot.BizLayer.BizMethods;
using portal.speedspot.BizLayer.IdentityModule;
using portal.speedspot.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.WebUI.ViewComponents
{
    [ViewComponent(Name = "UserMenu")]
    public class UserMenuViewComponent : ViewComponent
    {
        private readonly ApplicationUserManager _userManager;
        private readonly UserDepartmentsBiz _userDepartmentsBiz;
        public readonly IMapper _mapper;

        public UserMenuViewComponent(IMapper mapper,
            ApplicationUserManager userManager,
            UserDepartmentsBiz userDepartmentsBiz)
        {
            _mapper = mapper;
            _userManager = userManager;
            _userDepartmentsBiz = userDepartmentsBiz;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(Request.HttpContext.User);
            var userDepartments = _mapper.Map<List<UserDepartmentViewModel>>(await _userDepartmentsBiz.GetFullByUserAsync(user.Id));

            ViewData["UserObj"] = user;
            return View(userDepartments);
        }
    }
}

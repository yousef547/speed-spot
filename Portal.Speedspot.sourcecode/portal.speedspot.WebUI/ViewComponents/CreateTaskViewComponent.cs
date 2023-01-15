using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using portal.speedspot.BizLayer.BizMethods;
using portal.speedspot.BizLayer.IdentityModule;
using portal.speedspot.Models.Concretes.Identity;
using portal.speedspot.Models.Constants;
using portal.speedspot.Models.ViewModels;
using portal.speedspot.WebUI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace portal.speedspot.WebUI.ViewComponents
{
    [ViewComponent(Name = "CreateTask")]
    public class CreateTaskViewComponent : ViewComponent
    {
        private readonly ApplicationUserManager _userManager;
        public readonly LocalizationService _localizationService;
        public readonly IMapper _mapper;

        public CreateTaskViewComponent(IMapper mapper,
            ApplicationUserManager usermanager,
            LocalizationService localizationService)
        {
            _mapper = mapper;
            _userManager = usermanager;
            _localizationService = localizationService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(Request.HttpContext.User);
            var myEmployees = await _userManager.GetMyEmployeesAsync(user.Id);
            myEmployees.Add(user);
            ViewData["TaskAssignedToId"] = myEmployees.Select(u => _mapper.Map<UserViewModel>(u)).ToList();

            ViewData["PriorityLevels"] = PriorityList();
            return View("Index", new TaskViewModel());
        }

        public SelectList PriorityList()
        {
            try
            {
                var list = new List<SelectListItem>();
                foreach (ApplicationEnums.PriorityLevelEnum item in Enum.GetValues(typeof(ApplicationEnums.PriorityLevelEnum)))
                {
                    list.Add(new SelectListItem
                    {
                        Text = _localizationService[Enum.GetName(typeof(ApplicationEnums.PriorityLevelEnum), item)].Value,
                        Value = item.ToString()
                    });
                }
                return new SelectList(list, "Value", "Text");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                throw;
            }

        }
    }
}
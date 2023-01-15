using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using portal.speedspot.BizLayer.BizMethods;
using portal.speedspot.BizLayer.IdentityModule;
using portal.speedspot.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static portal.speedspot.Models.Constants.ApplicationEnums;

namespace portal.speedspot.WebUI.ViewComponents
{
    [ViewComponent(Name = "OpportunitiesArchive")]
    public class OpportunitiesArchiveViewComponent : ViewComponent
    {
        private readonly OpportunitiesBiz _opportunitiesBiz;
        private readonly OpportunityTypesBiz _opportunityTypesBiz;
        private readonly OpportunityStatusesBiz _opportunityStatusesBiz;
        private readonly DepartmentsBiz _departmentsBiz;
        private readonly ApplicationUserManager _userManager;
        private readonly UserDepartmentsBiz _userDepartmentsBiz;
        private readonly FavouritesBiz _favouritesBiz;
        private readonly FavouriteTypesBiz _favouriteTypesBiz;
        private readonly IMapper _mapper;

        public OpportunitiesArchiveViewComponent(
            OpportunitiesBiz opportunitiesBiz,
            OpportunityTypesBiz opportunityTypesBiz,
            OpportunityStatusesBiz opportunityStatusesBiz,
            DepartmentsBiz departmentsBiz,
            UserDepartmentsBiz userDepartmentsBiz,
            FavouritesBiz favouritesBiz,
            FavouriteTypesBiz favouriteTypesBiz,
            IMapper mapper,
            ApplicationUserManager userManager)
        {
            _opportunitiesBiz = opportunitiesBiz;
            _opportunityTypesBiz = opportunityTypesBiz;
            _opportunityStatusesBiz = opportunityStatusesBiz;
            _departmentsBiz = departmentsBiz;
            _mapper = mapper;
            _userManager = userManager;
            _userDepartmentsBiz = userDepartmentsBiz;
            _favouriteTypesBiz = favouriteTypesBiz;
            _favouritesBiz = favouritesBiz;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userDepartments = await _userDepartmentsBiz.GetByUserAsync(user.Id);

            List<OpportunityViewModel> opportunityVMs;
            if (await _userManager.IsSuperAdmin(user))
                opportunityVMs = _mapper.Map<List<OpportunityViewModel>>(await _opportunitiesBiz.GetAllArchivedAsync());
            else
                opportunityVMs = _mapper.Map<List<OpportunityViewModel>>(await _opportunitiesBiz.GetAllArchivedByUserAsync(user.Id, user.DepartmentId,
                    userDepartments.Select(x => x.DepartmentId).ToList()));

            ViewData["OpportunityTypes"] = await _opportunityTypesBiz.GetAllAsync();
            ViewData["OpportunityStatuses"] = await _opportunityStatusesBiz.GetAllAsync();

            if (await _userManager.IsSuperAdmin(user))
                ViewData["Departments"] = await _departmentsBiz.GetAllUnarchivedAsync();
            else
                ViewData["Departments"] = await _departmentsBiz.GetAllUnarchivedByUserAsync(user.DepartmentId, userDepartments.Select(x => x.DepartmentId).ToList());

            var favourityType = await _favouriteTypesBiz.GetByEnumValue((int)FavouriteTypeEnum.Opportunities);
            foreach (var opportunityVM in opportunityVMs)
            {
                opportunityVM.IsFavourite = await _favouritesBiz.IsFavourite(favourityType.Id, user.Id, opportunityVM.Id);
            }

            return View("Index", opportunityVMs);
        }
    }
}

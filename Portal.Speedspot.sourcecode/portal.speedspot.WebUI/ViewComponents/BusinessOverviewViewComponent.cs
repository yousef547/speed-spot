using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using portal.speedspot.BizLayer.BizMethods;
using portal.speedspot.BizLayer.IdentityModule;
using portal.speedspot.Models.Concretes.Identity;
using portal.speedspot.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static portal.speedspot.Models.Constants.ApplicationEnums;

namespace portal.speedspot.WebUI.ViewComponents
{
    [ViewComponent(Name = "BusinessOverview")]
    public class BusinessOverviewViewComponent : ViewComponent
    {
        private readonly OpportunitiesBiz _opportunitiesBiz;
        private readonly ApplicationUserManager _userManager;
        private readonly UserDepartmentsBiz _userDepartmentsBiz;
        private readonly IMapper _mapper;
        private readonly UserActivitiesBiz _userActivitiesBiz;
        private readonly QuotationsBiz _quotationsBiz;
        private readonly QuotationStatusesBiz _quotationStatusesBiz;

        public BusinessOverviewViewComponent(OpportunitiesBiz opportunitiesBiz,
            ApplicationUserManager userManager,
            UserDepartmentsBiz userDepartmentsBiz,
            IMapper mapper,
            UserActivitiesBiz userActivitiesBiz,
            QuotationsBiz quotationsBiz,
            QuotationStatusesBiz quotationStatusesBiz)
        {
            _opportunitiesBiz = opportunitiesBiz;
            _userManager = userManager;
            _userDepartmentsBiz = userDepartmentsBiz;
            _mapper = mapper;
            _userActivitiesBiz = userActivitiesBiz;
            _quotationsBiz = quotationsBiz;
            _quotationStatusesBiz = quotationStatusesBiz;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userDepartments = await _userDepartmentsBiz.GetByUserAsync(user.Id);

            var myEmployees = await _userManager.GetMyEmployeesAsync(user.Id);

            List<OpportunityViewModel> opportunityVMs;
            List<QuotationViewModel> quotationVMs;
            if (await _userManager.IsSuperAdmin(user))
            {
                opportunityVMs = _mapper.Map<List<OpportunityViewModel>>(await _opportunitiesBiz.GetAllUnarchivedAsync());

                quotationVMs = _mapper.Map<List<QuotationViewModel>>(await _quotationsBiz
                    .GetAllUnarchivedExceptAsync((await _quotationStatusesBiz
                    .GetByEnumValue((int)QuotationStatusEnum.New)).Id));
            }
            else
            {
                opportunityVMs = _mapper.Map<List<OpportunityViewModel>>(await _opportunitiesBiz.GetAllUnarchivedByUserAsync(user.Id, user.DepartmentId,
                    userDepartments.Select(x => x.DepartmentId).ToList()));

                quotationVMs = _mapper.Map<List<QuotationViewModel>>(await _quotationsBiz.GetAllUnarchivedByUserExceptAsync(user.Id, user.DepartmentId,
                       (await _quotationStatusesBiz.GetByEnumValue((int)QuotationStatusEnum.New)).Id));
            }

            ViewData["OpportunitiesCount"] = opportunityVMs.Count;
            ViewData["OpportunitiesTotal"] = opportunityVMs.Sum(o => o.Budget);

            ViewData["QuotationsCount"] = quotationVMs.Count;
            ViewData["QuotationsTotal"] = quotationVMs.Sum(o => o.OpportunityVM.Budget);

            List<UserActivityViewModel> teamActivities = new();

            foreach (ApplicationUser employee in myEmployees)
            {
                teamActivities.AddRange(_mapper.Map<List<UserActivityViewModel>>(await _userActivitiesBiz.GetByUserAsync(employee.Id)));
            }

            ViewData["TeamActivities"] = teamActivities;

            return View();
        }
    }
}

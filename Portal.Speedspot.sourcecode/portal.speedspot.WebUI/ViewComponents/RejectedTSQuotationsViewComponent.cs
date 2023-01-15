using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using portal.speedspot.BizLayer.BizMethods;
using portal.speedspot.BizLayer.IdentityModule;
using portal.speedspot.Models.ViewModels;

using static portal.speedspot.Models.Constants.ApplicationEnums;

namespace portal.speedspot.WebUI.ViewComponents
{
    [ViewComponent(Name = "RejectedTS")]
    public class RejectedTSQuotationsViewComponent : ViewComponent
    {
        private readonly ApplicationUserManager _userManager;
        private readonly IMapper _mapper;
        private readonly QuotationsBiz _quotationsBiz;
        private readonly UserDepartmentsBiz _userDepartmentsBiz;
        private readonly FavouriteTypesBiz _favouriteTypesBiz;
        private readonly FavouritesBiz _favouritesBiz;

        public RejectedTSQuotationsViewComponent(
            IMapper mapper,
            ApplicationUserManager userManager,
            QuotationsBiz quotationsBiz,
            UserDepartmentsBiz userDepartmentsBiz,
            FavouriteTypesBiz favouriteTypesBiz,
            FavouritesBiz favouritesBiz)
        {
            _mapper = mapper;
            _userManager = userManager;
            _quotationsBiz = quotationsBiz;
            _userDepartmentsBiz = userDepartmentsBiz;
            _favouriteTypesBiz = favouriteTypesBiz;
            _favouritesBiz = favouritesBiz;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userDepartments = await _userDepartmentsBiz.GetByUserAsync(user.Id);

            List<QuotationViewModel> quotationVMs;
            if (await _userManager.IsSuperAdmin(user))
            {
                quotationVMs = _mapper.Map<List<QuotationViewModel>>(await _quotationsBiz.GetTechnicalSessionRejectedAsync());
            }
            else
            {
                quotationVMs = _mapper.Map<List<QuotationViewModel>>(await _quotationsBiz.GetTechnicalSessionRejectedAsync(user.Id, user.DepartmentId,
                    userDepartments.Select(x => x.DepartmentId).ToList()));
            }

            var favourityType = await _favouriteTypesBiz.GetByEnumValue((int)FavouriteTypeEnum.Quotations);
            foreach (var quotationVM in quotationVMs)
            {
                quotationVM.IsFavourite = await _favouritesBiz.IsFavourite(favourityType.Id, user.Id, quotationVM.Id);
            }

            return View(quotationVMs);
        }
    }
}

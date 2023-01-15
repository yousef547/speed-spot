using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using portal.speedspot.BizLayer.BizMethods;
using portal.speedspot.BizLayer.IdentityModule;
using portal.speedspot.Models.ViewModels;
using portal.speedspot.WebUI.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static portal.speedspot.Models.Constants.ApplicationEnums;

namespace portal.speedspot.WebUI.ViewComponents
{
    [ViewComponent(Name = "Favourite")]
    public class FavouritesViewComponent : ViewComponent
    {
        private readonly IMapper _mapper;
        private readonly FavouritesBiz _favouritesBiz;
        private readonly ApplicationUserManager _userManager;
        private readonly OpportunitiesBiz _opportunitiesBiz;
        private readonly SuppliersBiz _suppliersBiz;
        private readonly CustomersBiz _customersBiz;
        private readonly LogisticsBiz _logisticsBiz;
        private readonly CompetitorsBiz _competitorsBiz;
        private readonly QuotationsBiz _quotationsBiz;
        public readonly LocalizationService _localizationService;

        public FavouritesViewComponent(IMapper mapper,
            ApplicationUserManager userManager,
            FavouritesBiz favouritesBiz,
            OpportunitiesBiz opportunitiesBiz,
            CustomersBiz customersBiz,
             SuppliersBiz suppliersBiz,
             CompetitorsBiz competitorsBiz,
             LogisticsBiz logisticsBiz,
             QuotationsBiz quotationsBiz,
             LocalizationService localizationService
            )
        {
            _favouritesBiz = favouritesBiz;
            _mapper = mapper;
            _userManager = userManager;
            _opportunitiesBiz = opportunitiesBiz;
            _customersBiz = customersBiz;
            _suppliersBiz = suppliersBiz;
            _competitorsBiz = competitorsBiz;
            _logisticsBiz = logisticsBiz;
            _quotationsBiz = quotationsBiz;
            _localizationService = localizationService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<FavouriteViewModel> model;
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var Favourites = await _favouritesBiz.GetAllFavouriteAsync(user.Id);

            model = _mapper.Map<List<FavouriteViewModel>>(Favourites).ToList();
            foreach (var item in model)
            {
                if (item.ItemId == null)
                {
                    item.Description = _localizationService["List"].Value;
                }
                else
                {
                    switch (item.FavouriteEnum)
                    {
                        case FavouriteTypeEnum.Opportunities:
                            var opportunitName = await _opportunitiesBiz.GetNameByIdAsync((int)item.ItemId);
                            item.Description = opportunitName.Name;
                            break;
                        case FavouriteTypeEnum.Quotations:
                            var quotationName = _mapper.Map<QuotationViewModel>(await _quotationsBiz.GetNameByIdAsync((int)item.ItemId));
                            item.Description = quotationName.OpportunityName;
                            break;
                        case FavouriteTypeEnum.Customers:
                            var customersName = await _customersBiz.GetInfoByIdAsync((int)item.ItemId);
                            item.Description = customersName.Name;
                            break;
                        case FavouriteTypeEnum.Suppliers:
                            var SuppliersName = await _suppliersBiz.GetNameByIdAsync((int)item.ItemId);
                            item.Description = SuppliersName.Name;
                            break;
                        case FavouriteTypeEnum.Competitors:
                            var competitorsName = await _competitorsBiz.GetNameByIdAsync((int)item.ItemId);
                            item.Description = competitorsName.Name;
                            break;
                        case FavouriteTypeEnum.Logistics:
                            var logisticName = await _logisticsBiz.GetNameByIdAsync((int)item.ItemId);
                            item.Description = logisticName.Name;
                            break;
                    }

                }
            }
            return View(model);
        }
    }
}

using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using portal.speedspot.BizLayer.BizMethods;
using portal.speedspot.BizLayer.IdentityModule;
using portal.speedspot.DALRepositories.Migrations;
using portal.speedspot.Models.Concretes;
using portal.speedspot.Models.Constants;
using portal.speedspot.Models.ViewModels;
using portal.speedspot.WebUI.Controllers.Infrastructure;
using portal.speedspot.WebUI.Helpers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using static portal.speedspot.Models.Constants.ApplicationEnums;
using static portal.speedspot.Models.Constants.Permissions;
using static portal.speedspot.Models.Constants.UserActivities;

namespace portal.speedspot.WebUI.Controllers
{
    public class QuotationsController : BaseController
    {
        private readonly CustomerNegotiationBiz _customerNegotiationBiz;
        private readonly SupplierNegotiationBiz _supplierNegotiationBiz;
        private readonly ProjectsBiz _projectsBiz;
        private readonly QuotationsBiz _quotationsBiz;
        private readonly AttachmentsBiz _AttachmentsBiz;
        private readonly FavouriteTypesBiz _favouriteTypesBiz;
        private readonly FavouritesBiz _favouritesBiz;
        private readonly QuotationStatusesBiz _quotationStatusesBiz;
        private readonly CurrenciesBiz _currenciesBiz;
        private readonly QuotationCurrenciesBiz _quotationCurrenciesBiz;
        private readonly QuotationOffersBiz _quotationOffersBiz;
        private readonly QuotationOfferVersionsBiz _quotationOfferVersionsBiz;
        private readonly OfferValiditiesBiz _offerValiditiesBiz;
        private readonly DeliveryPlacesBiz _deliveryPlacesBiz;
        private readonly OriginDocumentsBiz _originDocumentsBiz;
        private readonly OfferCertificatesBiz _offerCertificatesBiz;
        private readonly OpportunitiesBiz _opportunitiesBiz;
        private readonly OpportunityTypesBiz _opportunityTypesBiz;
        private readonly GuaranteeTermsBiz _guaranteeTermsBiz;
        private readonly DepartmentsBiz _departmentsBiz;
        private readonly CompanyDataBiz _companyDataBiz;
        private readonly CompetitorsBiz _competitorsBiz;
        private readonly CompetitorOffersBiz _competitorOffersBiz;
        private readonly SuppliersBiz _suppliersBiz;
        private readonly SupplierConversationsBiz _supplierConversationsBiz;
        private readonly CustomerConversationsBiz _CustomerConversationsBiz;
        private readonly CountriesBiz _countriesBiz;
        private readonly VATValuesBiz _vatValuesBiz;
        private readonly RejectReasonsBiz _rejectReasonsBiz;
        private readonly QuotationCompetitorsBiz _quotationCompetitorsBiz;
        private readonly NegotiationResultsBiz _negotiationResultsBiz;
        private readonly OtherNegotiationResultsBiz _otherNegotiationResultsBiz;
        private readonly BanksBiz _banksBiz;
        private readonly PerformanceLGsBiz _performanceLGsBiz;
        private readonly FinalLGsBiz _finalLGsBiz;
        private readonly PerformanceLGRequestsBiz _performanceLGRequestsBiz;
        private readonly FinalLGRequestsBiz _finalLGRequestsBiz;
        private readonly TechnicalSpecificationProductsBiz _technicalSpecificationProductsBiz;
        private readonly PurchaseOrdersBiz _purchaseOrdersBiz;
        private readonly DepartmentBanksBiz _departmentBanksBiz;
        private readonly SupplierOffersBiz _supplierOffersBiz;
        private readonly AlternateVersionsBiz _alternateVersionsBiz;

        public QuotationsController(QuotationsBiz quotationsBiz,
            CustomerNegotiationBiz customerNegotiationBiz,
            FavouriteTypesBiz favouriteTypesBiz,
            ProjectsBiz projectsBiz,
            AttachmentsBiz attachmentsBiz,
            SuppliersBiz suppliersBiz,
            FavouritesBiz favouritesBiz,
            SupplierNegotiationBiz supplierNegotiationBiz,
            CustomerConversationsBiz customerConversationsBiz,
            QuotationStatusesBiz quotationStatusesBiz,
            CurrenciesBiz currenciesBiz,
            QuotationCurrenciesBiz quotationCurrenciesBiz,
            QuotationOffersBiz quotationOffersBiz,
            OfferValiditiesBiz offerValiditiesBiz,
            DeliveryPlacesBiz deliveryPlacesBiz,
            OriginDocumentsBiz originDocumentsBiz,
            OfferCertificatesBiz offerCertificatesBiz,
            OpportunitiesBiz opportunitiesBiz,
            OpportunityTypesBiz opportunityTypesBiz,
            GuaranteeTermsBiz guaranteeTermsBiz,
            QuotationOfferVersionsBiz quotationOfferVersionsBiz,
            DepartmentsBiz departmentsBiz,
            CompanyDataBiz companyDataBiz,
            CompetitorsBiz competitorsBiz,
            CompetitorOffersBiz competitorOffersBiz,
            ApplicationUserManager userManager,
            SupplierConversationsBiz supplierConversationsBiz,
            CountriesBiz countriesBiz,
            VATValuesBiz vatValuesBiz,
            RejectReasonsBiz rejectReasonsBiz,
            QuotationCompetitorsBiz quotationCompetitorsBiz,
            NegotiationResultsBiz negotiationResultsBiz,
            OtherNegotiationResultsBiz otherNegotiationResultsBiz,
            BanksBiz banksBiz,
            PerformanceLGsBiz performanceLGsBiz,
            FinalLGsBiz finalLGsBiz,
            PerformanceLGRequestsBiz performanceLGRequestsBiz,
            FinalLGRequestsBiz finalLGRequestsBiz,
            TechnicalSpecificationProductsBiz technicalSpecificationProductsBiz,
            PurchaseOrdersBiz purchaseOrdersBiz,
            DepartmentBanksBiz departmentBanksBiz,
            SupplierOffersBiz supplierOffersBiz,
            AlternateVersionsBiz alternateVersionsBiz,
            IMapper mapper,
            IWebHostEnvironment hostEnvironment,
            LocalizationService localizationService,
            UserActivitiesBiz userActivitiesBiz)
            : base(mapper, hostEnvironment, userManager, localizationService, userActivitiesBiz)
        {
            _customerNegotiationBiz = customerNegotiationBiz;
            _quotationsBiz = quotationsBiz;
            _favouriteTypesBiz = favouriteTypesBiz;
            _projectsBiz = projectsBiz;
            _favouritesBiz = favouritesBiz;
            _quotationStatusesBiz = quotationStatusesBiz;
            _currenciesBiz = currenciesBiz;
            _quotationCurrenciesBiz = quotationCurrenciesBiz;
            _quotationOffersBiz = quotationOffersBiz;
            _quotationOfferVersionsBiz = quotationOfferVersionsBiz;
            _offerValiditiesBiz = offerValiditiesBiz;
            _deliveryPlacesBiz = deliveryPlacesBiz;
            _originDocumentsBiz = originDocumentsBiz;
            _offerCertificatesBiz = offerCertificatesBiz;
            _opportunityTypesBiz = opportunityTypesBiz;
            _opportunitiesBiz = opportunitiesBiz;
            _guaranteeTermsBiz = guaranteeTermsBiz;
            _departmentsBiz = departmentsBiz;
            _companyDataBiz = companyDataBiz;
            _competitorsBiz = competitorsBiz;
            _competitorOffersBiz = competitorOffersBiz;
            _suppliersBiz = suppliersBiz;
            _supplierConversationsBiz = supplierConversationsBiz;
            _AttachmentsBiz = attachmentsBiz;
            _CustomerConversationsBiz = customerConversationsBiz;
            _countriesBiz = countriesBiz;
            _vatValuesBiz = vatValuesBiz;
            _rejectReasonsBiz = rejectReasonsBiz;
            _quotationCompetitorsBiz = quotationCompetitorsBiz;
            _supplierNegotiationBiz = supplierNegotiationBiz;
            _negotiationResultsBiz = negotiationResultsBiz;
            _otherNegotiationResultsBiz = otherNegotiationResultsBiz;
            _banksBiz = banksBiz;
            _performanceLGsBiz = performanceLGsBiz;
            _finalLGsBiz = finalLGsBiz;
            _performanceLGRequestsBiz = performanceLGRequestsBiz;
            _finalLGRequestsBiz = finalLGRequestsBiz;
            _technicalSpecificationProductsBiz = technicalSpecificationProductsBiz;
            _purchaseOrdersBiz = purchaseOrdersBiz;
            _departmentBanksBiz = departmentBanksBiz;
            _supplierOffersBiz = supplierOffersBiz;
            _alternateVersionsBiz = alternateVersionsBiz;
        }

        public async Task<IActionResult> ToggleFavourite(int? itemId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var favourityType = await _favouriteTypesBiz.GetByEnumValue((int)FavouriteTypeEnum.Quotations);
            var isFavourite = await _favouritesBiz.IsFavourite(favourityType.Id, user.Id, itemId);

            if (isFavourite)
            {
                await _favouritesBiz.DeleteFavourite(favourityType.Id, user.Id, itemId);
            }
            else
            {
                await _favouritesBiz.AddAsync(new Favourite
                {
                    ItemId = itemId,
                    TypeId = favourityType.Id,
                    UserId = user.Id
                });
            }

            return Json(!isFavourite);
        }

        [Authorize(Permissions.Quotations.View)]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var favourityType = await _favouriteTypesBiz.GetByEnumValue((int)FavouriteTypeEnum.Quotations);
            var isFavourite = await _favouritesBiz.IsFavourite(favourityType.Id, user.Id, null);
            ViewData["IsFavourite"] = isFavourite;
            return View();
        }

        public async Task<IActionResult> GetQuotations(int? statusEnum)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            List<QuotationViewModel> quotationVMs;
            if (await _userManager.IsSuperAdmin(user))
            {
                if (statusEnum == null)
                {
                    quotationVMs = _mapper.Map<List<QuotationViewModel>>(await _quotationsBiz.GetAllUnarchivedExceptAsync((await _quotationStatusesBiz.GetByEnumValue((int)QuotationStatusEnum.New)).Id));
                }
                else
                {
                    QuotationStatus status = await _quotationStatusesBiz.GetByEnumValue((int)statusEnum);
                    quotationVMs = _mapper.Map<List<QuotationViewModel>>(await _quotationsBiz.GetAllUnarchivedAsync(status.Id));
                }
            }
            else
            {
                if (statusEnum == null)
                {
                    quotationVMs = _mapper.Map<List<QuotationViewModel>>(await _quotationsBiz.GetAllUnarchivedByUserExceptAsync(user.Id, user.DepartmentId,
                        (await _quotationStatusesBiz.GetByEnumValue((int)QuotationStatusEnum.New)).Id));
                }
                else
                {
                    QuotationStatus status = await _quotationStatusesBiz.GetByEnumValue((int)statusEnum);
                    quotationVMs = _mapper.Map<List<QuotationViewModel>>(await _quotationsBiz.GetAllUnarchivedByUserAsync(user.Id, user.DepartmentId, status.Id));
                }
            }

            var favourityType = await _favouriteTypesBiz.GetByEnumValue((int)FavouriteTypeEnum.Quotations);
            foreach (var quotationVM in quotationVMs)
            {
                quotationVM.IsFavourite = await _favouritesBiz.IsFavourite(favourityType.Id, user.Id, quotationVM.Id);
            }

            var isFavourite = await _favouritesBiz.IsFavourite(favourityType.Id, user.Id, null);
            ViewData["IsFavourite"] = isFavourite;
            ViewData["StatusEnum"] = statusEnum;
            return PartialView("_ShowQuotationsPartial", quotationVMs);
        }

        [Authorize(Permissions.Quotations.Details)]
        public async Task<IActionResult> Details(int id, int? statusId)
        {
            ViewBag.user = await _userManager.FindByNameAsync(User.Identity.Name);
            var favourityType = await _favouriteTypesBiz.GetByEnumValue((int)FavouriteTypeEnum.Quotations);
            var quotation = await _quotationsBiz.GetByIdAsync(id);
            var quotationVM = _mapper.Map<QuotationViewModel>(quotation);
            quotationVM.IsFavourite = await _favouritesBiz.IsFavourite(favourityType.Id, ViewBag.user.Id, quotationVM.Id);

            var currencies = quotationVM.OpportunityVM.SupplierOfferVMs.Select(x => new CurrencyViewModel
            {
                ExchangeRate = x.ExchangeRate,
                Name = x.CurrencyName,
                NameAr = x.CurrencyNameAr,
                Symbol = x.CurrencySymbol
            }).ToList();

            ViewData["CurrencyVMs"] = currencies;
            ViewBag.CustomerInfo = new CustomerInfoViewModel
            {
                Id = quotationVM.OpportunityVM.CustomerId,
                Name = quotationVM.OpportunityVM.CustomerName,
                NameAr = quotationVM.OpportunityVM.CustomerNameAr,
                LogoPath = quotationVM.OpportunityVM.CustomerLogoPath
            };
            var companyData = _mapper.Map<CompanyDataViewModel>(await _companyDataBiz.GetAsync());
            ViewBag.CompanyData = companyData;
            ViewBag.StatusEnum = (QuotationStatusEnum)(await _quotationStatusesBiz.GetByIdAsync(quotationVM.StatusId)).EnumValue;
            List<IGrouping<string, CompetitorOfferViewModel>> competitorsGroup = new();
            if (_localizationService.IsRightToLeft())
            {
                competitorsGroup = quotationVM.CompetitorOfferVMs.GroupBy(o => o.CompetitorNameAr).ToList();
            }
            else
            {
                competitorsGroup = quotationVM.CompetitorOfferVMs.GroupBy(o => o.CompetitorName).ToList();
            }

            ViewData["CompetitorsGroup"] = competitorsGroup;
            ViewBag.Suppliers = quotationVM.OpportunityVM.SupplierOfferVMs.GroupBy(g => g.SupplierId).Select(x => new SupplierViewModel
            {
                Name = x.First().SupplierName,
                NameAr = x.First().SupplierNameAr,
                LogoAttachmentPath = x.First().SupplierLogoAttachmentPath,
                Id = x.Key,
            }).ToList();
            ViewData["DueDate"] = quotation.DueDate;
            ViewData["Origins"] = await _countriesBiz.GetAllUnarchivedAsync();

            ViewData["IsTwoEnvelopes"] = (await _opportunityTypesBiz.GetByIdAsync(quotation.Opportunity.TypeId)).EnumValue == (int)OpportunityTypeEnum.TwoEnvelopes;

            ViewData["RejectReasonId"] = await _rejectReasonsBiz.GetAllUnarchivedAsync();

            ViewData["IsTechnicalSessionDone"] = quotation.TechnicalSessionStatus != null;

            ViewData["StatusId"] = statusId;
            var customerMessages = _mapper.Map<List<CustomerConversationViewModel>>(await _CustomerConversationsBiz.GetCustomerMessages(id, null));
            var customerNegotiation = _mapper.Map<List<CustomerNegotiationViewModel>>(await _customerNegotiationBiz.GetCustomerNegotiation(id, null));

            ViewBag.CustomerLogoPath = quotation.Opportunity.Customer.LogoAttachment?.FilePath;

            ViewData["CustomerMessages"] = customerMessages;
            ViewData["customerNegotiation"] = customerNegotiation;

            var lastOfferVersion = quotationVM.OfferVMs.SelectMany(x => x.VersionVMs).FirstOrDefault(v => v.IsSelected);
            if (lastOfferVersion == null)
            {
                lastOfferVersion = quotationVM.OfferVMs.SelectMany(x => x.VersionVMs).LastOrDefault();
            }

            List<QuotationGroupViewModel> quotationGroups = new();
            try
            {
                quotationGroups.Add(new QuotationGroupViewModel
                {
                    ImagePath = companyData.ImagePath,
                    Name = _localizationService.IsRightToLeft() ? companyData.NameAr : companyData.Name,
                    SubTotal = lastOfferVersion.SubTotal,
                    TotalPrice = lastOfferVersion.TotalPrice,
                    IsCompetitor = false,
                    OfferVMs = quotationVM.OfferVMs
                });

                foreach (var competitor in competitorsGroup)
                {
                    if (quotationVM.CompetitorVMs.Where(c => c.IsTechnicalAccepted).Any(qc => qc.CompetitorId == competitor.FirstOrDefault().CompetitorId))
                    {
                        quotationGroups.Add(new QuotationGroupViewModel
                        {
                            Id = competitor.First().CompetitorId,
                            ImagePath = competitor.First().CompetitorImagePath,
                            Name = competitor.Key,
                            SubTotal = competitor.FirstOrDefault().TotalPrice,
                            TotalPrice = competitor.FirstOrDefault().TotalPrice,
                            IsCompetitor = true,
                            CompetitorOfferVMs = competitor.ToList()
                        });
                    }
                }
            }
            catch { }

            ViewData["QuotationGroups"] = quotationGroups;

            var departmentBanks = await _departmentBanksBiz.GetByDepartmentIdAsync((int)quotationVM.OpportunityVM.DepartmentId);
            ViewData["DepartmentBankId"] = departmentBanks.Select(db => db.Branch.Bank).ToList();

            ViewData["BankId"] = await _banksBiz.GetAllUnarchivedAsync();

            ViewData["AssignedToId"] = await _userManager.GetAllUsersAsync();

            ViewData["PerformancePeriods"] = new List<Period>
            {
                new Period
                {
                    Text = _localizationService["OnePeriod"].Value,
                    Value = 1
                },
                new Period
                {
                     Text = _localizationService["TwoPeriods"].Value,
                    Value = 2
                }
            };

            ViewData["FinalPeriods"] = new List<Period>
            {
                new Period
                {
                     Text = _localizationService["TwoPeriods"].Value,
                    Value = 2
                },
                new Period
                {
                    Text = _localizationService["FourPeriods"].Value,
                    Value = 4
                },
                new Period
                {
                    Text = _localizationService["SixPeriods"].Value,
                    Value = 6
                },
            };

            return View(quotationVM);
        }

        [Authorize(Permissions.Quotations.Delete)]
        public async Task<IActionResult> Archive(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var result = await _quotationsBiz.ArchiveAsync(id, user.Id);

            var quotation = _mapper.Map<QuotationViewModel>(await _quotationsBiz.GetInfoByIdAsync(id));
            await SaveUserActivity(user.Id, UserActivities.Quotations.Archive, quotation.OpportunityName);

            return Json(result);
        }

        [Authorize(Permissions.Quotations.Delete)]
        public async Task<IActionResult> Unarchive(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            await _quotationsBiz.UnarchiveAsync(id, user.Id);

            var quotation = _mapper.Map<QuotationViewModel>(await _quotationsBiz.GetInfoByIdAsync(id));
            await SaveUserActivity(user.Id, UserActivities.Quotations.Unarchive, quotation.OpportunityName);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Offers(int id)
        {
            var offers = _mapper.Map<List<QuotationOfferViewModel>>(await _quotationOffersBiz.GetByQuotationId(id));
            var model = _mapper.Map<QuotationViewModel>(await _quotationsBiz.GetByIdAsync(id));

            ViewData["QuotationId"] = id;
            ViewData["CurrencyId"] = await _currenciesBiz.GetAllUnarchivedAsync();
            ViewData["DepartmentCode"] = model.OpportunityVM.DepartmentCode;
            ViewData["AddOfferEnabled"] = model.HasRemainingOffers;
            return View(offers);
        }

        public async Task<IActionResult> GetCurrencyData(int currencyId)
        {
            Currency currency = await _currenciesBiz.GetByIdAsync(currencyId);

            return Json(currency, new JsonSerializerOptions
            {
                PropertyNamingPolicy = null
            });
        }

        public class CurrencyModel
        {
            public IList<QuotationCurrencyViewModel> Currencies { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> AddCurrency(int id, CurrencyModel model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (model.Currencies == null) model.Currencies = new List<QuotationCurrencyViewModel>();

            foreach (var currencyVM in model.Currencies)
            {
                if (currencyVM.Id == null)
                {
                    currencyVM.QuotationId = id;
                    var quotationCurrency = _mapper.Map<QuotationCurrency>(currencyVM);
                    await _quotationCurrenciesBiz.AddAsync(quotationCurrency, user.Id);
                }
            }

            model.Currencies = _mapper.Map<List<QuotationCurrencyViewModel>>(await _quotationCurrenciesBiz.GetByQuotationIdAsync(id));

            model.Currencies.Add(new QuotationCurrencyViewModel());

            ViewData["CurrencyId"] = await _currenciesBiz.GetAllUnarchivedAsync();
            return PartialView("_CurrenciesPartial", model.Currencies);
        }

        public async Task<IActionResult> DeleteDbCurrency(int id, int quotationCurrencyId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            await _quotationCurrenciesBiz.DeleteAsync(quotationCurrencyId, user.Id);

            var currencies = _mapper.Map<List<QuotationCurrencyViewModel>>(await _quotationCurrenciesBiz.GetByQuotationIdAsync(id));

            ViewData["CurrencyId"] = await _currenciesBiz.GetAllUnarchivedAsync();
            return PartialView("_CurrenciesPartial", currencies);
        }

        public async Task<IActionResult> AddOffer(int id)
        {
            var quotation = await _quotationsBiz.GetByIdAsync(id);
            var model = _mapper.Map<QuotationViewModel>(quotation);

            List<QuotationOfferProductInfoViewModel> productList = new();
            var techSpecsProducts = _mapper.Map<List<TechnicalSpecificationProductViewModel>>(await _technicalSpecificationProductsBiz.GetByQuotationIdAsync(id));
            productList = techSpecsProducts.Select(p => new QuotationOfferProductInfoViewModel
            {
                ProductVM = p,
                ProductId = p.Id,
                SupplierOfferProductVMs = _mapper.Map<List<SupplierOfferProductViewModel>>(quotation.Opportunity.SupplierOffers
                .SelectMany(o => o.Products
                .Where(op => op.TechnicalSpecificationProductId == p.Id))
                .ToList())
            }).OrderBy(o => o.ProductVM.Index).ToList();

            var department = await _departmentsBiz.GetByIdAsync(quotation.Opportunity.DepartmentId);
            var companyData = await _companyDataBiz.GetAsync();
            var quotationOffer = new QuotationOfferViewModel
            {
                QuotationId = quotation.Id,
                IsTwoEnvelopes = (await _opportunityTypesBiz.GetByIdAsync(model.OpportunityVM.TypeId)).EnumValue == (int)OpportunityTypeEnum.TwoEnvelopes,
                VersionVMs = new List<QuotationOfferVersionViewModel>()
                {
                    new QuotationOfferVersionViewModel
                    {
                        Version = 1,
                        Title = quotation.Opportunity.Name,
                        Notes = _localizationService.IsRightToLeft() ? companyData.DefaultOfferNotesAr : companyData.DefaultOfferNotes,
                        CoverLetter = _localizationService.IsRightToLeft() ? department.DefaultOfferCoverLetterAr : department.DefaultOfferCoverLetter
                    }
                }
            };

            var currencies = model.OpportunityVM.SupplierOfferVMs.Select(x => new CurrencyViewModel
            {
                ExchangeRate = x.ExchangeRate,
                Name = x.CurrencyName,
                NameAr = x.CurrencyNameAr,
                Symbol = x.CurrencySymbol
            }).ToList();

            ViewData["CurrencyVMs"] = currencies;
            ViewData["CurrencyId"] = await _currenciesBiz.GetAllUnarchivedAsync();
            ViewData["ValidityId"] = await _offerValiditiesBiz.GetAllUnarchivedAsync();
            ViewData["DeliveryPlaceId"] = await _deliveryPlacesBiz.GetAllUnarchivedAsync();
            ViewData["OriginDocumentId"] = await _originDocumentsBiz.GetAllUnarchivedAsync();
            ViewData["CertificateId"] = await _offerCertificatesBiz.GetAllUnarchivedAsync();
            ViewData["GuaranteeTermId"] = await _guaranteeTermsBiz.GetAllUnarchivedAsync();
            ViewData["SubTitle"] = model.OpportunityName;
            ViewData["OfferVMs"] = model.OfferVMs;
            ViewData["Origins"] = await _countriesBiz.GetAllUnarchivedAsync();
            ViewData["VATValueId"] = _mapper.Map<List<VATValueViewModel>>(await _vatValuesBiz.GetAllUnarchivedAsync());

            ViewData["ProductList"] = productList;

            return View(quotationOffer);
        }

        [HttpPost]
        public async Task<IActionResult> AddOffer(QuotationOfferViewModel model)
        {
            if (model.VersionVMs[0].GuaranteeTermId != null)
            {
                if (model.VersionVMs[0].DeliveryGuaranteeMonths != null && model.VersionVMs[0].DeliveryGuaranteeMonths <= 0)
                {
                    ModelState.AddModelError("VersionVMs[0].DeliveryGuaranteeMonths", _localizationService["CompleteDeliveryTerms"].Value);
                }

                if (model.VersionVMs[0].CommissionGuaranteeMonths != null && model.VersionVMs[0].CommissionGuaranteeMonths <= 0)
                {
                    ModelState.AddModelError("VersionVMs[0].CommissionGuaranteeMonths", _localizationService["CompleteDeliveryTerms"].Value);
                }
            }
            else
            {
                model.VersionVMs[0].DeliveryGuaranteeMonths = null;
                model.VersionVMs[0].CommissionGuaranteeMonths = null;
            }

            if (model.VersionVMs[0].DownPaymentPercentage + model.VersionVMs[0].UponDeliveryPercentage + model.VersionVMs[0].AfterInstallationPercentage != 100)
            {
                ModelState.AddModelError("VersionVMs[0].DownPaymentPercentage", _localizationService["CompletePaymentTerms"].Value);
            }

            int i = 0;
            bool isOneProductIncluded = false;
            foreach (var product in model.VersionVMs[0].ProductVMs)
            {
                if (product.IsIncluded)
                {
                    isOneProductIncluded = true;
                    if (product.OriginIds == null)
                    {
                        ModelState.AddModelError($"VersionVMs[0].ProductVMs[{i}].OriginIds", string.Format(_localizationService["RequiredField"].Value, _localizationService["SelectedOrigins"].Value));
                    }
                    if (product.UnitPrice == 0)
                    {
                        ModelState.AddModelError($"VersionVMs[0].ProductVMs[{i}].UnitPrice", string.Format(_localizationService["RequiredField"].Value, _localizationService["UnitPrice"].Value));
                    }
                    if (product.OfferDescription == null)
                    {
                        ModelState.AddModelError($"VersionVMs[0].ProductVMs[{i}].OfferDescription", string.Format(_localizationService["RequiredField"].Value, _localizationService["OfferDescription"].Value));
                    }
                }
                else
                {
                    product.UnitPrice = 0;
                    product.OfferDescription = "";
                    product.OriginIds = null;
                }
                i++;
            }

            if (!isOneProductIncluded)
            {
                ModelState.AddModelError("", _localizationService["IncludeOneProduct"].Value);
            }

            var quotationObj = await _quotationsBiz.GetByIdAsync(model.QuotationId);
            if (ModelState.IsValid)
            {
                model.VersionVMs[0].CertificateVMs = new List<QuotationOfferCertificateViewModel>();
                foreach (var certificateId in model.VersionVMs[0].CertificateIds)
                {
                    model.VersionVMs[0].CertificateVMs.Add(new QuotationOfferCertificateViewModel
                    {
                        CertificateId = certificateId
                    });
                }

                foreach (var product in model.VersionVMs[0].ProductVMs)
                {
                    product.SelectedOriginVMs = new List<QuotationOfferProductOriginViewModel>();
                    if (product.OriginIds != null)
                    {
                        foreach (var originId in product.OriginIds)
                        {
                            product.SelectedOriginVMs.Add(new QuotationOfferProductOriginViewModel
                            {
                                CountryId = originId
                            });
                        }
                    }
                }

                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                model.VersionVMs[0].CreatedDate = DateTime.UtcNow;
                model.Code = await _quotationOffersBiz.GenerateNewcode(quotationObj.Opportunity.Department.Code);

                var quotationOffer = _mapper.Map<QuotationOffer>(model);
                quotationOffer.Versions[0].CreatedById = user.Id;

                var result = await _quotationOffersBiz.AddAsync(quotationOffer, user.Id);
                if (result)
                {
                    var quotationVM = _mapper.Map<QuotationViewModel>(quotationObj);
                    await SaveUserActivity(user.Id, UserActivities.Quotations.AddOffer, quotationVM.OpportunityName);

                    ProjectStatus projectStatus;
                    var opportunityType = (OpportunityTypeEnum)(await _opportunityTypesBiz.GetByIdAsync(quotationObj.Opportunity.TypeId)).EnumValue;

                    if (opportunityType == OpportunityTypeEnum.TwoEnvelopes)
                    {
                        quotationObj.StatusId = (await _quotationStatusesBiz.GetByEnumValue((int)QuotationStatusEnum.TechnicalSession)).Id;
                        projectStatus = ApplicationEnums.ProjectStatus.TechnicalSession;
                    }
                    else
                    {
                        quotationObj.StatusId = (await _quotationStatusesBiz.GetByEnumValue((int)QuotationStatusEnum.FinancialSession)).Id;
                        projectStatus = ApplicationEnums.ProjectStatus.FinanialSession;
                    }

                    await _quotationsBiz.UpdateAsync(quotationObj);

                    var project = await _projectsBiz.GetByOpportunityId(quotationObj.OpportunityId);
                    if (project != null)
                    {
                        project.Status = projectStatus;
                        project.QuotationId = quotationObj.Id;
                        await _projectsBiz.UpdateAsync(project);
                    }
                    //return RedirectToAction(nameof(Offers), new { id = model.QuotationId });

                    return Json(new
                    {
                        success = true,
                        quotationId = model.QuotationId
                    });
                }

                return BadRequest();
            }
            else
            {
                ViewData["CurrencyId"] = await _currenciesBiz.GetAllUnarchivedAsync();
                ViewData["ValidityId"] = await _offerValiditiesBiz.GetAllUnarchivedAsync();
                ViewData["DeliveryPlaceId"] = await _deliveryPlacesBiz.GetAllUnarchivedAsync();
                ViewData["OriginDocumentId"] = await _originDocumentsBiz.GetAllUnarchivedAsync();
                ViewData["CertificateId"] = await _offerCertificatesBiz.GetAllUnarchivedAsync();
                ViewData["GuaranteeTermId"] = await _guaranteeTermsBiz.GetAllUnarchivedAsync();
                ViewData["Origins"] = await _countriesBiz.GetAllUnarchivedAsync();
                ViewData["VATValueId"] = _mapper.Map<List<VATValueViewModel>>(await _vatValuesBiz.GetAllUnarchivedAsync());

                List<QuotationOfferProductInfoViewModel> productList = new();
                var techSpecsProducts = _mapper.Map<List<TechnicalSpecificationProductViewModel>>(await _technicalSpecificationProductsBiz.GetByQuotationIdAsync(model.QuotationId));
                productList = techSpecsProducts.Select(p => new QuotationOfferProductInfoViewModel
                {
                    ProductVM = p,
                    ProductId = p.Id,
                    SupplierOfferProductVMs = _mapper.Map<List<SupplierOfferProductViewModel>>(quotationObj.Opportunity.SupplierOffers
                    .SelectMany(o => o.Products
                    .Where(op => op.TechnicalSpecificationProductId == p.Id))
                    .ToList())
                }).OrderBy(o => o.ProductVM.Index).ToList();
                ViewData["ProductList"] = productList;

                return PartialView("_AddOfferPartial", model);
            }
        }

        public async Task<IActionResult> GetOffers(int id)
        {
            var offers = _mapper.Map<List<QuotationOfferViewModel>>(await _quotationOffersBiz.GetByQuotationId(id));

            ViewData["ShowFinishButton"] = true;
            return PartialView("_OffersPartial", offers);
        }

        public async Task<IActionResult> ClearAddOffer(int id)
        {
            var quotation = await _quotationsBiz.GetByIdAsync(id);
            var model = _mapper.Map<QuotationViewModel>(quotation);

            var department = await _departmentsBiz.GetByIdAsync(quotation.Opportunity.DepartmentId);

            var companyData = await _companyDataBiz.GetAsync();

            var quotationOffer = new QuotationOfferViewModel
            {
                QuotationId = quotation.Id,
                IsTwoEnvelopes = (await _opportunityTypesBiz.GetByIdAsync(model.OpportunityVM.TypeId)).EnumValue == (int)OpportunityTypeEnum.TwoEnvelopes,
                VersionVMs = new List<QuotationOfferVersionViewModel>()
                {
                    new QuotationOfferVersionViewModel
                    {
                        Version = 1,
                        Title = quotation.Opportunity.Name,
                        Notes = _localizationService.IsRightToLeft() ? companyData.DefaultOfferNotesAr : companyData.DefaultOfferNotes,
                        CoverLetter = _localizationService.IsRightToLeft() ? department.DefaultOfferCoverLetterAr : department.DefaultOfferCoverLetter
                    }
                }
            };

            ViewData["CurrencyId"] = await _currenciesBiz.GetAllUnarchivedAsync();
            ViewData["ValidityId"] = await _offerValiditiesBiz.GetAllUnarchivedAsync();
            ViewData["DeliveryPlaceId"] = await _deliveryPlacesBiz.GetAllUnarchivedAsync();
            ViewData["OriginDocumentId"] = await _originDocumentsBiz.GetAllUnarchivedAsync();
            ViewData["CertificateId"] = await _offerCertificatesBiz.GetAllUnarchivedAsync();
            ViewData["GuaranteeTermId"] = await _guaranteeTermsBiz.GetAllUnarchivedAsync();
            ViewData["Origins"] = await _countriesBiz.GetAllUnarchivedAsync();
            ViewData["VATValueId"] = _mapper.Map<List<VATValueViewModel>>(await _vatValuesBiz.GetAllUnarchivedAsync());

            List<QuotationOfferProductInfoViewModel> productList = new();
            var techSpecsProducts = _mapper.Map<List<TechnicalSpecificationProductViewModel>>(await _technicalSpecificationProductsBiz.GetByQuotationIdAsync(id));
            productList = techSpecsProducts.Select(p => new QuotationOfferProductInfoViewModel
            {
                ProductVM = p,
                ProductId = p.Id,
                SupplierOfferProductVMs = _mapper.Map<List<SupplierOfferProductViewModel>>(quotation.Opportunity.SupplierOffers
                .SelectMany(o => o.Products
                .Where(op => op.TechnicalSpecificationProductId == p.Id))
                .ToList())
            }).OrderBy(o => o.ProductVM.Index).ToList();
            ViewData["ProductList"] = productList;

            return PartialView("_AddOfferPartial", quotationOffer);
        }

        public async Task<IActionResult> AddOfferVersion(int offerId)
        {
            var offer = _mapper.Map<QuotationOfferViewModel>(await _quotationOffersBiz.GetByIdAsync(offerId));
            var model = offer.VersionVMs.OrderBy(o => o.Version).LastOrDefault();

            var quotation = await _quotationsBiz.GetByIdAsync(offer.QuotationId);
            var department = await _departmentsBiz.GetByIdAsync(quotation.Opportunity.DepartmentId);
            var companyData = await _companyDataBiz.GetAsync();

            model.Id = 0;
            model.Version += 1;
            model.Notes = _localizationService.IsRightToLeft() ? companyData.DefaultOfferNotesAr : companyData.DefaultOfferNotes;
            model.CoverLetter = _localizationService.IsRightToLeft() ? department.DefaultOfferCoverLetterAr : department.DefaultOfferCoverLetter;

            ViewData["OfferVM"] = offer;
            ViewData["CurrencyId"] = await _currenciesBiz.GetAllUnarchivedAsync();
            ViewData["ValidityId"] = await _offerValiditiesBiz.GetAllUnarchivedAsync();
            ViewData["DeliveryPlaceId"] = await _deliveryPlacesBiz.GetAllUnarchivedAsync();
            ViewData["OriginDocumentId"] = await _originDocumentsBiz.GetAllUnarchivedAsync();
            ViewData["CertificateId"] = await _offerCertificatesBiz.GetAllUnarchivedAsync();
            ViewData["GuaranteeTermId"] = await _guaranteeTermsBiz.GetAllUnarchivedAsync();
            ViewData["Origins"] = await _countriesBiz.GetAllUnarchivedAsync();
            ViewData["VATValueId"] = _mapper.Map<List<VATValueViewModel>>(await _vatValuesBiz.GetAllUnarchivedAsync());

            List<QuotationOfferProductInfoViewModel> productList = new();
            var techSpecsProducts = _mapper.Map<List<TechnicalSpecificationProductViewModel>>(await _technicalSpecificationProductsBiz.GetByQuotationIdAsync(offer.QuotationId));
            productList = techSpecsProducts.Select(p => new QuotationOfferProductInfoViewModel
            {
                ProductVM = p,
                ProductId = p.Id,
                SupplierOfferProductVMs = _mapper.Map<List<SupplierOfferProductViewModel>>(quotation.Opportunity.SupplierOffers
                .SelectMany(o => o.Products
                .Where(op => op.TechnicalSpecificationProductId == p.Id))
                .ToList())
            }).OrderBy(o => o.ProductVM.Index).ToList();
            ViewData["ProductList"] = productList;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddOfferVersion(QuotationOfferVersionViewModel model, int quotationId)
        {
            if (model.GuaranteeTermId != null)
            {
                if (model.DeliveryGuaranteeMonths != null && model.DeliveryGuaranteeMonths <= 0)
                {
                    ModelState.AddModelError("DeliveryGuaranteeMonths", _localizationService["CompleteDeliveryTerms"].Value);
                }

                if (model.CommissionGuaranteeMonths != null && model.CommissionGuaranteeMonths <= 0)
                {
                    ModelState.AddModelError("CommissionGuaranteeMonths", _localizationService["CompleteDeliveryTerms"].Value);
                }
            }
            else
            {
                model.DeliveryGuaranteeMonths = null;
                model.CommissionGuaranteeMonths = null;
            }

            if (model.DownPaymentPercentage + model.UponDeliveryPercentage + model.AfterInstallationPercentage != 100)
            {
                ModelState.AddModelError("DownPaymentPercentage", _localizationService["CompletePaymentTerms"].Value);
            }

            int i = 0;
            bool isOneProductIncluded = false;
            foreach (var product in model.ProductVMs)
            {
                if (product.IsIncluded)
                {
                    isOneProductIncluded = true;
                    if (product.OriginIds == null)
                    {
                        ModelState.AddModelError($"VersionVMs[0].ProductVMs[{i}].OriginIds", string.Format(_localizationService["RequiredField"].Value, _localizationService["SelectedOrigins"].Value));
                    }
                    if (product.UnitPrice == 0)
                    {
                        ModelState.AddModelError($"VersionVMs[0].ProductVMs[{i}].UnitPrice", string.Format(_localizationService["RequiredField"].Value, _localizationService["UnitPrice"].Value));
                    }
                    if (product.OfferDescription == null)
                    {
                        ModelState.AddModelError($"VersionVMs[0].ProductVMs[{i}].OfferDescription", string.Format(_localizationService["RequiredField"].Value, _localizationService["OfferDescription"].Value));
                    }
                }
                else
                {
                    product.UnitPrice = 0;
                    product.OfferDescription = "";
                    product.OriginIds = null;
                }
                i++;
            }

            if (!isOneProductIncluded)
            {
                ModelState.AddModelError("", _localizationService["IncludeOneProduct"].Value);
            }

            model.CertificateVMs = new List<QuotationOfferCertificateViewModel>();
            foreach (var certificateId in model.CertificateIds)
            {
                model.CertificateVMs.Add(new QuotationOfferCertificateViewModel
                {
                    CertificateId = certificateId
                });
            }

            foreach (var product in model.ProductVMs)
            {
                product.SelectedOriginVMs = new List<QuotationOfferProductOriginViewModel>();
                if (product.OriginIds != null)
                {
                    foreach (var originId in product.OriginIds)
                    {
                        product.SelectedOriginVMs.Add(new QuotationOfferProductOriginViewModel
                        {
                            CountryId = originId
                        });
                    }
                }
            }

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                model.CreatedDate = DateTime.UtcNow;

                var version = _mapper.Map<QuotationOfferVersion>(model);
                version.CreatedById = user.Id;

                var result = await _quotationOfferVersionsBiz.AddAsync(version, user.Id);
                if (result)
                {
                    var quotationVM = _mapper.Map<QuotationViewModel>(await _quotationsBiz.GetInfoByIdAsync(quotationId));
                    await SaveUserActivity(user.Id, UserActivities.Quotations.AddOfferVersion, quotationVM.OpportunityName);

                    return RedirectToAction(nameof(Offers), new { id = quotationId });
                }
            }

            ViewData["OfferVM"] = _mapper.Map<QuotationOfferViewModel>(await _quotationOffersBiz.GetByIdAsync(model.OfferId));
            ViewData["CurrencyId"] = await _currenciesBiz.GetAllUnarchivedAsync();
            ViewData["ValidityId"] = await _offerValiditiesBiz.GetAllUnarchivedAsync();
            ViewData["DeliveryPlaceId"] = await _deliveryPlacesBiz.GetAllUnarchivedAsync();
            ViewData["OriginDocumentId"] = await _originDocumentsBiz.GetAllUnarchivedAsync();
            ViewData["CertificateId"] = await _offerCertificatesBiz.GetAllUnarchivedAsync();
            ViewData["GuaranteeTermId"] = await _guaranteeTermsBiz.GetAllUnarchivedAsync();
            ViewData["Origins"] = await _countriesBiz.GetAllUnarchivedAsync();
            ViewData["VATValueId"] = _mapper.Map<List<VATValueViewModel>>(await _vatValuesBiz.GetAllUnarchivedAsync());

            var quotation = await _quotationsBiz.GetByIdAsync(quotationId);
            List<QuotationOfferProductInfoViewModel> productList = new();
            var techSpecsProducts = _mapper.Map<List<TechnicalSpecificationProductViewModel>>(await _technicalSpecificationProductsBiz.GetByQuotationIdAsync(quotationId));
            productList = techSpecsProducts.Select(p => new QuotationOfferProductInfoViewModel
            {
                ProductVM = p,
                ProductId = p.Id,
                SupplierOfferProductVMs = _mapper.Map<List<SupplierOfferProductViewModel>>(quotation.Opportunity.SupplierOffers
                .SelectMany(o => o.Products
                .Where(op => op.TechnicalSpecificationProductId == p.Id))
                .ToList())
            }).OrderBy(o => o.ProductVM.Index).ToList();
            ViewData["ProductList"] = productList;

            return View(model);
        }

        public async Task<IActionResult> AddAlternateVersion(int versionId)
        {
            var versionVM = _mapper.Map<QuotationOfferVersionViewModel>(await _quotationOfferVersionsBiz.GetByIdAsync(versionId));

            var offerVM = _mapper.Map<QuotationOfferViewModel>(await _quotationOffersBiz.GetByIdAsync(versionVM.OfferId));

            AlternateVersionViewModel model = new()
            {
                QuotationOfferVersionId = versionId,
                CurrencyId = versionVM.CurrencyId,
                ValidityId = versionVM.ValidityId,
                DeliveryFromDays = versionVM.DeliveryFromDays,
                DeliveryToDays = versionVM.DeliveryToDays,
                DeliveryNotes = versionVM.DeliveryNotes,
                DeliveryPlaceId = versionVM.DeliveryPlaceId,
                OriginDocumentId = versionVM.OriginDocumentId,
                CertificateVMs = versionVM.CertificateVMs.Select(v => new AlternateVersionCertificateViewModel
                {
                    CertificateId = v.CertificateId
                }).ToList(),
                CertificateIds = versionVM.CertificateVMs.Select(v => v.CertificateId).ToList(),
                DeliveryGuaranteeMonths = versionVM.DeliveryGuaranteeMonths,
                CommissionGuaranteeMonths = versionVM.CommissionGuaranteeMonths,
                GuaranteeTermId = versionVM.GuaranteeTermId,
                DownPaymentPercentage = versionVM.DownPaymentPercentage,
                UponDeliveryPercentage = versionVM.UponDeliveryPercentage,
                AfterInstallationPercentage = versionVM.AfterInstallationPercentage,
                Factor = versionVM.Factor,
                ProductVMs = versionVM.ProductVMs.Select(x => new AlternateVersionProductViewModel
                {
                    IsIncluded = x.IsIncluded,
                    OfferDescription = x.OfferDescription,
                    ProductId = x.ProductId,
                    SelectedOriginVMs = x.SelectedOriginVMs.Select(y => new AlternateVersionProductOriginViewModel
                    {
                        CountryId = y.CountryId,
                        ProductId = y.ProductId
                    }).ToList(),
                    OriginIds = x.SelectedOriginVMs.Select(y => y.CountryId).ToList(),
                    SupplierPrice = x.SupplierPrice,
                    SupplierRate = x.SupplierRate,
                    TaxPercentage = x.TaxPercentage,
                    UnitPrice = x.UnitPrice
                }).ToList()
            };

            ViewBag.OfferVM = offerVM;
            ViewBag.VersionVM = versionVM;

            ViewData["CurrencyId"] = await _currenciesBiz.GetAllUnarchivedAsync();
            ViewData["ValidityId"] = await _offerValiditiesBiz.GetAllUnarchivedAsync();
            ViewData["DeliveryPlaceId"] = await _deliveryPlacesBiz.GetAllUnarchivedAsync();
            ViewData["OriginDocumentId"] = await _originDocumentsBiz.GetAllUnarchivedAsync();
            ViewData["CertificateId"] = await _offerCertificatesBiz.GetAllUnarchivedAsync();
            ViewData["GuaranteeTermId"] = await _guaranteeTermsBiz.GetAllUnarchivedAsync();
            ViewData["Origins"] = await _countriesBiz.GetAllUnarchivedAsync();
            ViewData["VATValueId"] = _mapper.Map<List<VATValueViewModel>>(await _vatValuesBiz.GetAllUnarchivedAsync());

            var quotation = await _quotationsBiz.GetByIdAsync(offerVM.QuotationId);

            List<QuotationOfferProductInfoViewModel> productList = new();
            var techSpecsProducts = _mapper.Map<List<TechnicalSpecificationProductViewModel>>(await _technicalSpecificationProductsBiz.GetByQuotationIdAsync(offerVM.QuotationId));
            productList = techSpecsProducts.Select(p => new QuotationOfferProductInfoViewModel
            {
                ProductVM = p,
                ProductId = p.Id,
                SupplierOfferProductVMs = _mapper.Map<List<SupplierOfferProductViewModel>>(quotation.Opportunity.SupplierOffers
                .SelectMany(o => o.Products
                .Where(op => op.TechnicalSpecificationProductId == p.Id))
                .ToList())
            }).OrderBy(o => o.ProductVM.Index).ToList();
            ViewData["ProductList"] = productList;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddAlternateVersion(AlternateVersionViewModel model, int quotationId)
        {
            if (model.GuaranteeTermId != null)
            {
                if (model.DeliveryGuaranteeMonths != null && model.DeliveryGuaranteeMonths <= 0)
                {
                    ModelState.AddModelError("DeliveryGuaranteeMonths", _localizationService["CompleteDeliveryTerms"].Value);
                }

                if (model.CommissionGuaranteeMonths != null && model.CommissionGuaranteeMonths <= 0)
                {
                    ModelState.AddModelError("CommissionGuaranteeMonths", _localizationService["CompleteDeliveryTerms"].Value);
                }
            }
            else
            {
                model.DeliveryGuaranteeMonths = null;
                model.CommissionGuaranteeMonths = null;
            }

            if (model.DownPaymentPercentage + model.UponDeliveryPercentage + model.AfterInstallationPercentage != 100)
            {
                ModelState.AddModelError("DownPaymentPercentage", _localizationService["CompletePaymentTerms"].Value);
            }

            int i = 0;
            bool isOneProductIncluded = false;
            foreach (var product in model.ProductVMs)
            {
                if (product.IsIncluded)
                {
                    isOneProductIncluded = true;
                    if (product.OriginIds == null)
                    {
                        ModelState.AddModelError($"VersionVMs[0].ProductVMs[{i}].OriginIds", string.Format(_localizationService["RequiredField"].Value, _localizationService["SelectedOrigins"].Value));
                    }
                    if (product.UnitPrice == 0)
                    {
                        ModelState.AddModelError($"VersionVMs[0].ProductVMs[{i}].UnitPrice", string.Format(_localizationService["RequiredField"].Value, _localizationService["UnitPrice"].Value));
                    }
                    if (product.OfferDescription == null)
                    {
                        ModelState.AddModelError($"VersionVMs[0].ProductVMs[{i}].OfferDescription", string.Format(_localizationService["RequiredField"].Value, _localizationService["OfferDescription"].Value));
                    }
                }
                else
                {
                    product.UnitPrice = 0;
                    product.OfferDescription = "";
                    product.OriginIds = null;
                }
                i++;
            }

            if (!isOneProductIncluded)
            {
                ModelState.AddModelError("", _localizationService["IncludeOneProduct"].Value);
            }

            model.CertificateVMs = new List<AlternateVersionCertificateViewModel>();
            foreach (var certificateId in model.CertificateIds)
            {
                model.CertificateVMs.Add(new AlternateVersionCertificateViewModel
                {
                    CertificateId = certificateId
                });
            }

            foreach (var product in model.ProductVMs)
            {
                product.SelectedOriginVMs = new List<AlternateVersionProductOriginViewModel>();
                if (product.OriginIds != null)
                {
                    foreach (var originId in product.OriginIds)
                    {
                        product.SelectedOriginVMs.Add(new AlternateVersionProductOriginViewModel
                        {
                            CountryId = originId
                        });
                    }
                }
            }

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                var version = _mapper.Map<AlternateVersion>(model);
                version.SerialNo = await _alternateVersionsBiz.GenerateSerialNo(model.QuotationOfferVersionId);
                var result = await _alternateVersionsBiz.AddAsync(version, user.Id);
                if (result)
                {
                    var quotationVM = _mapper.Map<QuotationViewModel>(await _quotationsBiz.GetInfoByIdAsync(quotationId));
                    await SaveUserActivity(user.Id, UserActivities.Quotations.AddAlternateVersion, quotationVM.OpportunityName);

                    return RedirectToAction(nameof(Offers), new { id = quotationId });
                }
            }

            var versionVM = _mapper.Map<QuotationOfferVersionViewModel>(await _quotationOfferVersionsBiz.GetByIdAsync(model.QuotationOfferVersionId));

            var offerVM = _mapper.Map<QuotationOfferViewModel>(await _quotationOffersBiz.GetByIdAsync(versionVM.OfferId));

            ViewBag.OfferVM = offerVM;
            ViewBag.VersionVM = versionVM;

            ViewData["CurrencyId"] = await _currenciesBiz.GetAllUnarchivedAsync();
            ViewData["ValidityId"] = await _offerValiditiesBiz.GetAllUnarchivedAsync();
            ViewData["DeliveryPlaceId"] = await _deliveryPlacesBiz.GetAllUnarchivedAsync();
            ViewData["OriginDocumentId"] = await _originDocumentsBiz.GetAllUnarchivedAsync();
            ViewData["CertificateId"] = await _offerCertificatesBiz.GetAllUnarchivedAsync();
            ViewData["GuaranteeTermId"] = await _guaranteeTermsBiz.GetAllUnarchivedAsync();
            ViewData["Origins"] = await _countriesBiz.GetAllUnarchivedAsync();
            ViewData["VATValueId"] = _mapper.Map<List<VATValueViewModel>>(await _vatValuesBiz.GetAllUnarchivedAsync());

            var quotation = await _quotationsBiz.GetByIdAsync(quotationId);
            List<QuotationOfferProductInfoViewModel> productList = new();
            var techSpecsProducts = _mapper.Map<List<TechnicalSpecificationProductViewModel>>(await _technicalSpecificationProductsBiz.GetByQuotationIdAsync(quotationId));
            productList = techSpecsProducts.Select(p => new QuotationOfferProductInfoViewModel
            {
                ProductVM = p,
                ProductId = p.Id,
                SupplierOfferProductVMs = _mapper.Map<List<SupplierOfferProductViewModel>>(quotation.Opportunity.SupplierOffers
                .SelectMany(o => o.Products
                .Where(op => op.TechnicalSpecificationProductId == p.Id))
                .ToList())
            }).OrderBy(o => o.ProductVM.Index).ToList();
            ViewData["ProductList"] = productList;

            return View(model);
        }

        //public async Task<IActionResult> EditOfferVersion(int offerVersionId)
        //{
        //    var version = await _quotationOfferVersionsBiz.GetByIdAsync(offerVersionId);
        //    var model = _mapper.Map<QuotationOfferVersionViewModel>(version);

        //    ViewData["OfferVM"] = _mapper.Map<QuotationOfferViewModel>(version.Offer);
        //    ViewData["CurrencyId"] = await _currenciesBiz.GetAllUnarchivedAsync();
        //    ViewData["ValidityId"] = await _offerValiditiesBiz.GetAllUnarchivedAsync();
        //    ViewData["DeliveryPlaceId"] = await _deliveryPlacesBiz.GetAllUnarchivedAsync();
        //    ViewData["OriginDocumentId"] = await _originDocumentsBiz.GetAllUnarchivedAsync();
        //    ViewData["CertificateId"] = await _offerCertificatesBiz.GetAllUnarchivedAsync();
        //    ViewData["GuaranteeTermId"] = await _guaranteeTermsBiz.GetAllUnarchivedAsync();
        //    ViewData["Origins"] = await _countriesBiz.GetAllUnarchivedAsync();
        //    ViewData["VATValueId"] = _mapper.Map<List<VATValueViewModel>>(await _vatValuesBiz.GetAllUnarchivedAsync());
        //    return View(model);
        //}

        //[HttpPost]
        //public async Task<IActionResult> EditOfferVersion(QuotationOfferVersionViewModel model, int quotationId)
        //{
        //    if (model.GuaranteeTermId != null)
        //    {
        //        if (model.DeliveryGuaranteeMonths != null && model.DeliveryGuaranteeMonths <= 0)
        //        {
        //            ModelState.AddModelError("VersionVMs[0].DeliveryGuaranteeMonths", _localizationService["CompleteDeliveryTerms"].Value);
        //        }

        //        if (model.CommissionGuaranteeMonths != null && model.CommissionGuaranteeMonths <= 0)
        //        {
        //            ModelState.AddModelError("VersionVMs[0].CommissionGuaranteeMonths", _localizationService["CompleteDeliveryTerms"].Value);
        //        }
        //    }
        //    else
        //    {
        //        model.DeliveryGuaranteeMonths = null;
        //        model.CommissionGuaranteeMonths = null;
        //    }

        //    if (model.DownPaymentPercentage + model.UponDeliveryPercentage + model.AfterInstallationPercentage != 100)
        //    {
        //        ModelState.AddModelError("VersionVMs[0].DownPaymentPercentage", _localizationService["CompletePaymentTerms"].Value);
        //    }

        //    int i = 0;
        //    foreach (var product in model.ProductVMs.Where(p => p.IsIncluded))
        //    {
        //        if (product.OriginIds == null)
        //        {
        //            ModelState.AddModelError($"ProductVMs[{i}].OriginIds", string.Format(_localizationService["RequiredField"].Value, _localizationService["SelectedOrigins"].Value));
        //        }
        //        i++;
        //    }
        //    model.CertificateVMs = new List<QuotationOfferCertificateViewModel>();
        //    foreach (var certificateId in model.CertificateIds)
        //    {
        //        model.CertificateVMs.Add(new QuotationOfferCertificateViewModel
        //        {
        //            CertificateId = certificateId
        //        });
        //    }

        //    bool isOneProductIncluded = false;
        //    i = 0;
        //    foreach (var product in model.ProductVMs)
        //    {
        //        if (product.IsIncluded)
        //        {
        //            isOneProductIncluded = true;
        //            if (product.UnitPrice == 0)
        //            {
        //                ModelState.AddModelError($"ProductVMs[{i}].UnitPrice", string.Format(_localizationService["RequiredField"].Value, _localizationService["UnitPrice"].Value));
        //            }
        //        }
        //        i++;
        //    }
        //    if (!isOneProductIncluded)
        //    {
        //        ModelState.AddModelError("", _localizationService["IncludeOneProduct"].Value);
        //    }

        //    foreach (var product in model.ProductVMs)
        //    {
        //        product.SelectedOriginVMs = new List<QuotationOfferProductOriginViewModel>();
        //        foreach (var originId in product.OriginIds)
        //        {
        //            product.SelectedOriginVMs.Add(new QuotationOfferProductOriginViewModel
        //            {
        //                CountryId = originId
        //            });
        //        }
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        List<QuotationOfferProductViewModel> deletedProducts = new List<QuotationOfferProductViewModel>();
        //        foreach (var product in model.ProductVMs)
        //        {
        //            if (!product.IsIncluded) deletedProducts.Add(product);
        //        }
        //        foreach (var product in deletedProducts)
        //        {
        //            model.ProductVMs.Remove(product);
        //        }

        //        var user = await _userManager.FindByNameAsync(User.Identity.Name);

        //        var version = _mapper.Map<QuotationOfferVersion>(model);
        //        model.CreatedDate = DateTime.UtcNow;

        //        var result = await _quotationOfferVersionsBiz.UpdateAsync(version, user.Id);
        //        if (result)
        //        {
        //            var quotationVM = _mapper.Map<QuotationViewModel>(await _quotationsBiz.GetInfoByIdAsync(quotationId));
        //            await SaveUserActivity(user.Id, UserActivities.Quotations.UpdateOfferVersion, quotationVM.OpportunityName);

        //            return RedirectToAction(nameof(Offers), new { id = quotationId });
        //        }
        //    }

        //    ViewData["OfferVM"] = _mapper.Map<QuotationOfferViewModel>(await _quotationOffersBiz.GetByIdAsync(model.OfferId));
        //    ViewData["CurrencyId"] = await _currenciesBiz.GetAllUnarchivedAsync();
        //    ViewData["ValidityId"] = await _offerValiditiesBiz.GetAllUnarchivedAsync();
        //    ViewData["DeliveryPlaceId"] = await _deliveryPlacesBiz.GetAllUnarchivedAsync();
        //    ViewData["OriginDocumentId"] = await _originDocumentsBiz.GetAllUnarchivedAsync();
        //    ViewData["CertificateId"] = await _offerCertificatesBiz.GetAllUnarchivedAsync();
        //    ViewData["GuaranteeTermId"] = await _guaranteeTermsBiz.GetAllUnarchivedAsync();
        //    ViewData["Origins"] = await _countriesBiz.GetAllUnarchivedAsync();
        //    ViewData["VATValueId"] = _mapper.Map<List<VATValueViewModel>>(await _vatValuesBiz.GetAllUnarchivedAsync());
        //    return View(model);
        //}

        public async Task<IActionResult> EditAlternateVersion(int alternateVersionId)
        {
            var alternateVersionVM = _mapper.Map<AlternateVersionViewModel>(await _alternateVersionsBiz.GetByIdAsync(alternateVersionId));

            var versionVM = _mapper.Map<QuotationOfferVersionViewModel>(await _quotationOfferVersionsBiz.GetByIdAsync(alternateVersionVM.QuotationOfferVersionId));

            var offerVM = _mapper.Map<QuotationOfferViewModel>(await _quotationOffersBiz.GetByIdAsync(versionVM.OfferId));

            ViewBag.OfferVM = offerVM;
            ViewBag.VersionVM = versionVM;

            ViewData["CurrencyId"] = await _currenciesBiz.GetAllUnarchivedAsync();
            ViewData["ValidityId"] = await _offerValiditiesBiz.GetAllUnarchivedAsync();
            ViewData["DeliveryPlaceId"] = await _deliveryPlacesBiz.GetAllUnarchivedAsync();
            ViewData["OriginDocumentId"] = await _originDocumentsBiz.GetAllUnarchivedAsync();
            ViewData["CertificateId"] = await _offerCertificatesBiz.GetAllUnarchivedAsync();
            ViewData["GuaranteeTermId"] = await _guaranteeTermsBiz.GetAllUnarchivedAsync();
            ViewData["Origins"] = await _countriesBiz.GetAllUnarchivedAsync();
            ViewData["VATValueId"] = _mapper.Map<List<VATValueViewModel>>(await _vatValuesBiz.GetAllUnarchivedAsync());

            var quotation = await _quotationsBiz.GetByIdAsync(offerVM.QuotationId);

            List<QuotationOfferProductInfoViewModel> productList = new();
            var techSpecsProducts = _mapper.Map<List<TechnicalSpecificationProductViewModel>>(await _technicalSpecificationProductsBiz.GetByQuotationIdAsync(offerVM.QuotationId));
            productList = techSpecsProducts.Select(p => new QuotationOfferProductInfoViewModel
            {
                ProductVM = p,
                ProductId = p.Id,
                SupplierOfferProductVMs = _mapper.Map<List<SupplierOfferProductViewModel>>(quotation.Opportunity.SupplierOffers
                .SelectMany(o => o.Products
                .Where(op => op.TechnicalSpecificationProductId == p.Id))
                .ToList())
            }).OrderBy(o => o.ProductVM.Index).ToList();
            ViewData["ProductList"] = productList;

            return View(alternateVersionVM);
        }

        [HttpPost]
        public async Task<IActionResult> EditAlternateVersion(AlternateVersionViewModel model, int quotationId)
        {
            if (model.GuaranteeTermId != null)
            {
                if (model.DeliveryGuaranteeMonths != null && model.DeliveryGuaranteeMonths <= 0)
                {
                    ModelState.AddModelError("DeliveryGuaranteeMonths", _localizationService["CompleteDeliveryTerms"].Value);
                }

                if (model.CommissionGuaranteeMonths != null && model.CommissionGuaranteeMonths <= 0)
                {
                    ModelState.AddModelError("CommissionGuaranteeMonths", _localizationService["CompleteDeliveryTerms"].Value);
                }
            }
            else
            {
                model.DeliveryGuaranteeMonths = null;
                model.CommissionGuaranteeMonths = null;
            }

            if (model.DownPaymentPercentage + model.UponDeliveryPercentage + model.AfterInstallationPercentage != 100)
            {
                ModelState.AddModelError("DownPaymentPercentage", _localizationService["CompletePaymentTerms"].Value);
            }

            int i = 0;
            bool isOneProductIncluded = false;
            foreach (var product in model.ProductVMs)
            {
                if (product.IsIncluded)
                {
                    isOneProductIncluded = true;
                    if (product.OriginIds == null)
                    {
                        ModelState.AddModelError($"VersionVMs[0].ProductVMs[{i}].OriginIds", string.Format(_localizationService["RequiredField"].Value, _localizationService["SelectedOrigins"].Value));
                    }
                    if (product.UnitPrice == 0)
                    {
                        ModelState.AddModelError($"VersionVMs[0].ProductVMs[{i}].UnitPrice", string.Format(_localizationService["RequiredField"].Value, _localizationService["UnitPrice"].Value));
                    }
                    if (product.OfferDescription == null)
                    {
                        ModelState.AddModelError($"VersionVMs[0].ProductVMs[{i}].OfferDescription", string.Format(_localizationService["RequiredField"].Value, _localizationService["OfferDescription"].Value));
                    }
                }
                else
                {
                    product.UnitPrice = 0;
                    product.OfferDescription = "";
                    product.OriginIds = null;
                }
                i++;
            }

            if (!isOneProductIncluded)
            {
                ModelState.AddModelError("", _localizationService["IncludeOneProduct"].Value);
            }

            model.CertificateVMs = new List<AlternateVersionCertificateViewModel>();
            foreach (var certificateId in model.CertificateIds)
            {
                model.CertificateVMs.Add(new AlternateVersionCertificateViewModel
                {
                    CertificateId = certificateId
                });
            }

            foreach (var product in model.ProductVMs)
            {
                product.SelectedOriginVMs = new List<AlternateVersionProductOriginViewModel>();
                if (product.OriginIds != null)
                {
                    foreach (var originId in product.OriginIds)
                    {
                        product.SelectedOriginVMs.Add(new AlternateVersionProductOriginViewModel
                        {
                            CountryId = originId
                        });
                    }
                }
            }

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                var version = _mapper.Map<AlternateVersion>(model);

                var result = await _alternateVersionsBiz.UpdateAsync(version, user.Id);
                if (result)
                {
                    var quotationVM = _mapper.Map<QuotationViewModel>(await _quotationsBiz.GetInfoByIdAsync(quotationId));
                    await SaveUserActivity(user.Id, UserActivities.Quotations.UpdateOfferVersion, quotationVM.OpportunityName);

                    return RedirectToAction(nameof(Offers), new { id = quotationId });
                }
            }

            var versionVM = _mapper.Map<QuotationOfferVersionViewModel>(await _quotationOfferVersionsBiz.GetByIdAsync(model.QuotationOfferVersionId));

            var offerVM = _mapper.Map<QuotationOfferViewModel>(await _quotationOffersBiz.GetByIdAsync(versionVM.OfferId));

            ViewBag.OfferVM = offerVM;
            ViewBag.VersionVM = versionVM;

            ViewData["CurrencyId"] = await _currenciesBiz.GetAllUnarchivedAsync();
            ViewData["ValidityId"] = await _offerValiditiesBiz.GetAllUnarchivedAsync();
            ViewData["DeliveryPlaceId"] = await _deliveryPlacesBiz.GetAllUnarchivedAsync();
            ViewData["OriginDocumentId"] = await _originDocumentsBiz.GetAllUnarchivedAsync();
            ViewData["CertificateId"] = await _offerCertificatesBiz.GetAllUnarchivedAsync();
            ViewData["GuaranteeTermId"] = await _guaranteeTermsBiz.GetAllUnarchivedAsync();
            ViewData["Origins"] = await _countriesBiz.GetAllUnarchivedAsync();
            ViewData["VATValueId"] = _mapper.Map<List<VATValueViewModel>>(await _vatValuesBiz.GetAllUnarchivedAsync());

            var quotation = await _quotationsBiz.GetByIdAsync(offerVM.QuotationId);

            List<QuotationOfferProductInfoViewModel> productList = new();
            var techSpecsProducts = _mapper.Map<List<TechnicalSpecificationProductViewModel>>(await _technicalSpecificationProductsBiz.GetByQuotationIdAsync(offerVM.QuotationId));
            productList = techSpecsProducts.Select(p => new QuotationOfferProductInfoViewModel
            {
                ProductVM = p,
                ProductId = p.Id,
                SupplierOfferProductVMs = _mapper.Map<List<SupplierOfferProductViewModel>>(quotation.Opportunity.SupplierOffers
                .SelectMany(o => o.Products
                .Where(op => op.TechnicalSpecificationProductId == p.Id))
                .ToList())
            }).OrderBy(o => o.ProductVM.Index).ToList();
            ViewData["ProductList"] = productList;

            return View(model);
        }

        public async Task<IActionResult> GetAlternateVersionData(int versionId)
        {
            var alternateVersionVM = _mapper.Map<List<AlternateVersionViewModel>>(await _alternateVersionsBiz.GetByVersionIdAsync(versionId));

            return PartialView("_AlternateVersionPartial", alternateVersionVM);
        }

        public async Task<IActionResult> ChangeSelectedOfferVersion(int id, int versionId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var result = await _quotationsBiz.ChangeSelectedVersion(id, versionId);

            var quotationVM = _mapper.Map<QuotationViewModel>(await _quotationsBiz.GetInfoByIdAsync(id));
            await SaveUserActivity(user.Id, UserActivities.Quotations.ChangeSelectedOfferVersion, quotationVM.OpportunityName);

            return Json(result);
        }

        public async Task<IActionResult> PrintOffer(int offerVersionId, bool isEmail)
        {
            var offerVersion = await _quotationOfferVersionsBiz.GetByIdAsync(offerVersionId);
            var model = _mapper.Map<QuotationOfferVersionViewModel>(offerVersion);
            ViewBag.IsEmail = isEmail;
            var opportunity = await _opportunitiesBiz.GetBasicDataByIdAsync(offerVersion.Offer.Quotation.OpportunityId);
            var opportunityVM = _mapper.Map<OpportunityViewModel>(opportunity);
            ViewData["OpportunityVM"] = opportunityVM;

            var department = await _departmentsBiz.GetByIdAsync(opportunity.DepartmentId);
            var departmentVM = _mapper.Map<DepartmentViewModel>(department);
            ViewData["DepartmentVM"] = departmentVM;

            var companyData = await _companyDataBiz.GetAsync();
            var companyDataVM = _mapper.Map<CompanyDataViewModel>(companyData);
            ViewData["CompanyDataVM"] = companyDataVM;

            ViewData["OfferVM"] = _mapper.Map<QuotationOfferViewModel>(offerVersion.Offer);

            return View(model);
        }

        public async Task<IActionResult> PrintTechnicalOffer(int offerVersionId, bool isEmail)
        {
            var offerVersion = await _quotationOfferVersionsBiz.GetByIdAsync(offerVersionId);
            var model = _mapper.Map<QuotationOfferVersionViewModel>(offerVersion);
            ViewBag.IsEmail = isEmail;
            var opportunity = await _opportunitiesBiz.GetBasicDataByIdAsync(offerVersion.Offer.Quotation.OpportunityId);
            var opportunityVM = _mapper.Map<OpportunityViewModel>(opportunity);
            ViewData["OpportunityVM"] = opportunityVM;

            var department = await _departmentsBiz.GetByIdAsync(opportunity.DepartmentId);
            var departmentVM = _mapper.Map<DepartmentViewModel>(department);
            ViewData["DepartmentVM"] = departmentVM;

            var companyData = await _companyDataBiz.GetAsync();
            var companyDataVM = _mapper.Map<CompanyDataViewModel>(companyData);
            ViewData["CompanyDataVM"] = companyDataVM;
            var offerId = department.Code + offerVersion.CreatedDate.ToString("yyMMdd") + "T" + offerVersion.Version;

            await SendToConvesation(offerVersion.Offer.QuotationId, offerId, isEmail);

            ViewData["OfferVM"] = _mapper.Map<QuotationOfferViewModel>(offerVersion.Offer);

            return View(model);
        }

        public async Task<IActionResult> PrintFinancialOffer(int offerVersionId, bool isEmail)
        {
            var offerVersion = await _quotationOfferVersionsBiz.GetByIdAsync(offerVersionId);
            var model = _mapper.Map<QuotationOfferVersionViewModel>(offerVersion);
            ViewBag.IsEmail = isEmail;
            var opportunity = await _opportunitiesBiz.GetBasicDataByIdAsync(offerVersion.Offer.Quotation.OpportunityId);
            var opportunityVM = _mapper.Map<OpportunityViewModel>(opportunity);
            ViewData["OpportunityVM"] = opportunityVM;
            var department = await _departmentsBiz.GetByIdAsync(opportunity.DepartmentId);
            var departmentVM = _mapper.Map<DepartmentViewModel>(department);
            ViewData["DepartmentVM"] = departmentVM;
            var companyData = await _companyDataBiz.GetAsync();
            var companyDataVM = _mapper.Map<CompanyDataViewModel>(companyData);
            ViewData["CompanyDataVM"] = companyDataVM;
            var offerId = department.Code + offerVersion.CreatedDate.ToString("yyMMdd") + "F" + offerVersion.Version;

            await SendToConvesation(offerVersion.Offer.QuotationId, offerId, isEmail);

            ViewData["OfferVM"] = _mapper.Map<QuotationOfferViewModel>(offerVersion.Offer);

            return View(model);
        }

        public async Task<IActionResult> AddCompetitorOffer(int id)
        {
            ViewData["DeliveryPlaceId"] = await _deliveryPlacesBiz.GetAllUnarchivedAsync();
            ViewData["CompetitorId"] = await _competitorsBiz.GetAllUnarchivedAsync();
            ViewData["ValidityId"] = await _offerValiditiesBiz.GetAllUnarchivedAsync();
            ViewData["CurrencyId"] = await _currenciesBiz.GetAllUnarchivedAsync();
            ViewData["CertificateId"] = await _offerCertificatesBiz.GetAllUnarchivedAsync();
            ViewData["OriginDocumentId"] = await _originDocumentsBiz.GetAllUnarchivedAsync();
            ViewData["GuaranteeTermId"] = await _guaranteeTermsBiz.GetAllUnarchivedAsync();
            ViewData["Origins"] = await _countriesBiz.GetAllUnarchivedAsync();

            var opportunity = await _opportunitiesBiz.GetByQuotationId(id);

            ViewData["ProductVMs"] = _mapper.Map<List<TechnicalSpecificationProductViewModel>>(opportunity.TechnicalSpecification.Products).OrderBy(o => o.Index).ToList();

            return View(new CompetitorOfferViewModel
            {
                QuotationId = id,
                IsTwoEnvelopes = (await _opportunityTypesBiz.GetByIdAsync(opportunity.TypeId)).EnumValue == (int)OpportunityTypeEnum.TwoEnvelopes
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddCompetitorOffer(CompetitorOfferViewModel model)
        {
            if (model.AfterInstallationPercentage + model.DownPaymentPercentage + model.UponDeliveryPercentage != 100)
            {
                ModelState.AddModelError("DownPaymentPercentage", _localizationService["CompletePaymentTerms"].Value);
            }

            bool isOneProductIncluded = false;
            int i = 0;
            foreach (var product in model.ProductVMs)
            {
                if (product.IsIncluded)
                {
                    isOneProductIncluded = true;
                    if (product.OriginIds == null)
                    {
                        ModelState.AddModelError($"ProductVMs[{i}].OriginIds", string.Format(_localizationService["RequiredField"].Value, _localizationService["SelectedOrigins"].Value));
                    }
                }

                if (product.IsIncluded && product.UnitPrice == null && !model.IsTwoEnvelopes)
                {
                    ModelState.AddModelError($"ProductVMs[{i}].UnitPrice", string.Format(_localizationService["ValueMustBeANumber"].Value, _localizationService["UnitPrice"].Value));
                }
                i++;
            }

            if (!isOneProductIncluded)
            {
                ModelState.AddModelError("", _localizationService["IncludeOneProduct"].Value);
            }

            if (ModelState.IsValid)
            {
                i = 0;
                foreach (var product in model.ProductVMs)
                {
                    if (product.IsIncluded)
                    {
                        product.OriginVMs = new List<CompetitorOfferProductOriginViewModel>();
                        foreach (var origin in product.OriginIds)
                        {
                            product.OriginVMs.Add(new CompetitorOfferProductOriginViewModel
                            {
                                CountryId = origin
                            });
                        }
                    }
                    i++;
                }

                model.CertificateVMs = new List<CompetitorOfferCertificateViewModel>();
                foreach (var certificate in model.CertificateIds)
                {
                    model.CertificateVMs.Add(new CompetitorOfferCertificateViewModel
                    {
                        CertificateId = certificate
                    });
                }

                var competitorOffer = _mapper.Map<CompetitorOffer>(model);

                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                competitorOffer.CreatedById = user.Id;

                competitorOffer.CreatedDate = DateTime.UtcNow;

                await _competitorOffersBiz.AddAsync(competitorOffer, user.Id);

                var quotationVM = _mapper.Map<QuotationViewModel>(await _quotationsBiz.GetInfoByIdAsync(model.QuotationId));
                await SaveUserActivity(user.Id, UserActivities.Quotations.AddCompetitorOffer, quotationVM.OpportunityName);

                var quotationCompetitors = await _quotationCompetitorsBiz.GetByQuotationId(model.QuotationId);
                if (!quotationCompetitors.Any(qc => qc.CompetitorId == model.CompetitorId))
                {
                    await _quotationCompetitorsBiz.AddAsync(new QuotationCompetitor
                    {
                        CompetitorId = model.CompetitorId,
                        QuotationId = model.QuotationId,
                        IsTechnicalAccepted = !model.IsTwoEnvelopes
                    });
                }

                return RedirectToAction(nameof(Details), new { id = model.QuotationId });
            }

            ViewData["DeliveryPlaceId"] = await _deliveryPlacesBiz.GetAllUnarchivedAsync();
            ViewData["CompetitorId"] = await _competitorsBiz.GetAllUnarchivedAsync();
            ViewData["ValidityId"] = await _offerValiditiesBiz.GetAllUnarchivedAsync();
            ViewData["CurrencyId"] = await _currenciesBiz.GetAllUnarchivedAsync();
            ViewData["CertificateId"] = await _offerCertificatesBiz.GetAllUnarchivedAsync();
            ViewData["OriginDocumentId"] = await _originDocumentsBiz.GetAllUnarchivedAsync();
            ViewData["GuaranteeTermId"] = await _guaranteeTermsBiz.GetAllUnarchivedAsync();
            ViewData["Origins"] = await _countriesBiz.GetAllUnarchivedAsync();

            var opportunity = await _opportunitiesBiz.GetByQuotationId(model.QuotationId);

            ViewData["ProductVMs"] = _mapper.Map<List<TechnicalSpecificationProductViewModel>>(opportunity.TechnicalSpecification.Products).OrderBy(o => o.Index).ToList();

            return View(model);
        }

        public async Task<IActionResult> EditCompetitorOffer(int offerId)
        {
            var offer = _mapper.Map<CompetitorOfferViewModel>(await _competitorOffersBiz.GetByIdAsync(offerId));

            offer.CertificateIds = offer.CertificateVMs != null ? offer.CertificateVMs.Select(x => x.CertificateId).ToList() : new List<int>();

            foreach (var product in offer.ProductVMs)
            {
                product.OriginIds = product.OriginVMs != null ? product.OriginVMs.Select(x => x.CountryId).ToList() : new List<int>();
            }

            ViewData["DeliveryPlaceId"] = await _deliveryPlacesBiz.GetAllUnarchivedAsync();
            ViewData["CompetitorId"] = await _competitorsBiz.GetAllUnarchivedAsync();
            ViewData["ValidityId"] = await _offerValiditiesBiz.GetAllUnarchivedAsync();
            ViewData["CurrencyId"] = await _currenciesBiz.GetAllUnarchivedAsync();
            ViewData["CertificateId"] = await _offerCertificatesBiz.GetAllUnarchivedAsync();
            ViewData["OriginDocumentId"] = await _originDocumentsBiz.GetAllUnarchivedAsync();
            ViewData["GuaranteeTermId"] = await _guaranteeTermsBiz.GetAllUnarchivedAsync();
            ViewData["Origins"] = await _countriesBiz.GetAllUnarchivedAsync();

            var opportunity = await _opportunitiesBiz.GetByQuotationId(offer.QuotationId);

            ViewData["ProductVMs"] = _mapper.Map<List<TechnicalSpecificationProductViewModel>>(opportunity.TechnicalSpecification.Products).OrderBy(o => o.Index).ToList();

            offer.IsTwoEnvelopes = (await _opportunityTypesBiz.GetByIdAsync(opportunity.TypeId)).EnumValue == (int)OpportunityTypeEnum.TwoEnvelopes;

            return View(offer);
        }

        [HttpPost]
        public async Task<IActionResult> EditCompetitorOffer(CompetitorOfferViewModel model)
        {
            if (model.AfterInstallationPercentage + model.DownPaymentPercentage + model.UponDeliveryPercentage != 100)
            {
                ModelState.AddModelError("DownPaymentPercentage", _localizationService["CompletePaymentTerms"].Value);
            }

            bool isOneProductIncluded = false;
            int i = 0;
            foreach (var product in model.ProductVMs)
            {
                if (product.IsIncluded)
                {
                    isOneProductIncluded = true;
                    if (product.OriginIds == null)
                    {
                        ModelState.AddModelError($"ProductVMs[{i}].OriginIds", string.Format(_localizationService["RequiredField"].Value, _localizationService["SelectedOrigins"].Value));
                    }
                }

                if (product.IsIncluded && product.UnitPrice == null && !model.IsTwoEnvelopes)
                {
                    ModelState.AddModelError($"ProductVMs[{i}].UnitPrice", string.Format(_localizationService["ValueMustBeANumber"].Value, _localizationService["UnitPrice"].Value));
                }
                i++;
            }

            if (!isOneProductIncluded)
            {
                ModelState.AddModelError("", _localizationService["IncludeOneProduct"].Value);
            }

            if (ModelState.IsValid)
            {
                foreach (var product in model.ProductVMs)
                {
                    if (product.IsIncluded)
                    {
                        product.OriginVMs = new List<CompetitorOfferProductOriginViewModel>();
                        foreach (var origin in product.OriginIds)
                        {
                            product.OriginVMs.Add(new CompetitorOfferProductOriginViewModel
                            {
                                CountryId = origin
                            });
                        }
                    }
                }

                model.CertificateVMs = new List<CompetitorOfferCertificateViewModel>();
                foreach (var certificate in model.CertificateIds)
                {
                    model.CertificateVMs.Add(new CompetitorOfferCertificateViewModel
                    {
                        CertificateId = certificate
                    });
                }

                var competitorOffer = _mapper.Map<CompetitorOffer>(model);

                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                await _competitorOffersBiz.UpdateAsync(competitorOffer, user.Id);

                var quotationCompetitors = await _quotationCompetitorsBiz.GetByQuotationId(model.QuotationId);
                if (!quotationCompetitors.Any(qc => qc.CompetitorId == model.CompetitorId))
                {
                    await _quotationCompetitorsBiz.AddAsync(new QuotationCompetitor
                    {
                        CompetitorId = model.CompetitorId,
                        QuotationId = model.QuotationId,
                        IsTechnicalAccepted = !model.IsTwoEnvelopes
                    });

                    var quotationVM = _mapper.Map<QuotationViewModel>(await _quotationsBiz.GetInfoByIdAsync(model.QuotationId));
                    await SaveUserActivity(user.Id, UserActivities.Quotations.UpdateCompetitorOffer, quotationVM.OpportunityName);
                }

                return RedirectToAction(nameof(Details), new { id = model.QuotationId });
            }

            ViewData["DeliveryPlaceId"] = await _deliveryPlacesBiz.GetAllUnarchivedAsync();
            ViewData["CompetitorId"] = await _competitorsBiz.GetAllUnarchivedAsync();
            ViewData["ValidityId"] = await _offerValiditiesBiz.GetAllUnarchivedAsync();
            ViewData["CurrencyId"] = await _currenciesBiz.GetAllUnarchivedAsync();
            ViewData["CertificateId"] = await _offerCertificatesBiz.GetAllUnarchivedAsync();
            ViewData["OriginDocumentId"] = await _originDocumentsBiz.GetAllUnarchivedAsync();
            ViewData["GuaranteeTermId"] = await _guaranteeTermsBiz.GetAllUnarchivedAsync();
            ViewData["Origins"] = await _countriesBiz.GetAllUnarchivedAsync();

            var opportunity = await _opportunitiesBiz.GetByQuotationId(model.QuotationId);

            ViewData["ProductVMs"] = _mapper.Map<List<TechnicalSpecificationProductViewModel>>(opportunity.TechnicalSpecification.Products).OrderBy(o => o.Index).ToList();

            return View(model);
        }

        public async Task<IActionResult> GetMassageSupplier(int id, int supplierId, DateTime? date)
        {
            var quotation = await _quotationsBiz.GetInfoByIdAsync(id);
            var massages = _mapper.Map<List<SupplierConversationViewModel>>(await _supplierConversationsBiz.GetSupplierMessages(supplierId, id, date));

            ViewData["IsTechnicalSessionDone"] = quotation.TechnicalSessionStatus != null;
            return PartialView("_SupplierConversationPartial", massages);
        }

        public async Task<IActionResult> CreateConversationSupplier(SupplierConversationViewModel model)
        {
            bool result = false;

            if (ModelState.IsValid)
            {
                var user = (await _userManager.FindByNameAsync(User.Identity.Name));
                if (model.ConversationFile != null)
                {
                    string uniqueFileName = UploadFile(model.ConversationFile, "Conversation");
                    var attachment = new Attachment { FilePath = uniqueFileName, Title = model.ConversationFile.FileName, UploadedById = user.Id };
                    var fileAttachment = await _AttachmentsBiz.AddAsync(attachment);
                    model.AttachmentId = attachment.Id;
                }
                model.CreatedById = user.Id;
                var convarsation = _mapper.Map<SupplierConversation>(model);
                result = await _supplierConversationsBiz.AddAsync(convarsation);
                if (result)
                {
                    var quotationVM = _mapper.Map<QuotationViewModel>(await _quotationsBiz.GetInfoByIdAsync(model.QuotationId));
                    await SaveUserActivity(user.Id, UserActivities.Quotations.AddSupplierConversation, quotationVM.OpportunityName);

                    return RedirectToAction(nameof(Index));
                }
            }
            return Json(result);
        }

        public async Task<IActionResult> ArchiveConversationSupplier(int id)
        {
            var result = await _supplierConversationsBiz.ArchiveAsync(id);

            return Json(result);
        }

        public async Task<IActionResult> GetMassageCustomer(int id, DateTime? date)
        {
            var quotation = await _quotationsBiz.GetInfoByIdAsync(id);
            var massages = _mapper.Map<List<CustomerConversationViewModel>>(await _CustomerConversationsBiz.GetCustomerMessages(id, date));
            ViewBag.CustomerLogoPath = quotation.Opportunity.Customer.LogoAttachment?.FilePath;

            ViewData["IsTechnicalSessionDone"] = quotation.TechnicalSessionStatus != null;
            return PartialView("_CustomerConversationPartial", massages);
        }

        public async Task<IActionResult> CreateConversationCustomer(CustomerConversationViewModel model)
        {
            bool result = false;

            if (ModelState.IsValid)
            {
                var user = (await _userManager.FindByNameAsync(User.Identity.Name));
                if (model.ConversationFile != null)
                {
                    string uniqueFileName = UploadFile(model.ConversationFile, "Conversation");
                    var attachment = new Attachment { FilePath = uniqueFileName, Title = model.ConversationFile.FileName, UploadedById = user.Id };
                    var fileAttachment = await _AttachmentsBiz.AddAsync(attachment);
                    model.AttachmentId = attachment.Id;
                }
                model.CreatedById = user.Id;
                var convarsation = _mapper.Map<CustomerConversation>(model);
                result = await _CustomerConversationsBiz.AddAsync(convarsation);
                if (result)
                {
                    var quotationVM = _mapper.Map<QuotationViewModel>(await _quotationsBiz.GetInfoByIdAsync(model.QuotationId));
                    await SaveUserActivity(user.Id, UserActivities.Quotations.AddCustomerConversation, quotationVM.OpportunityName);

                    return RedirectToAction(nameof(Index));
                }
            }
            return Json(result);
        }

        public async Task<IActionResult> ArchiveConversationCustomer(int id)
        {
            var result = await _CustomerConversationsBiz.ArchiveAsync(id);
            return Json(result);
        }

        public async Task<IActionResult> EditConversationSupplier(int id, int quotationId)
        {
            ViewBag.CompanyData = _mapper.Map<CompanyDataViewModel>(await _companyDataBiz.GetAsync());
            var massages = _mapper.Map<SupplierConversationViewModel>(await _supplierConversationsBiz.GetByIdAsync(id));
            return PartialView("_EditConversationSupplier", massages);
        }

        public async Task<IActionResult> EditConversationCustomer(int id, int quotationId)
        {
            ViewBag.CompanyData = _mapper.Map<CompanyDataViewModel>(await _companyDataBiz.GetAsync());
            var massagesCutomer = _mapper.Map<CustomerConversationViewModel>(await _CustomerConversationsBiz.GetByIdAsync(id));
            return PartialView("_EditConversationCustomer", massagesCutomer);
        }

        public async Task<IActionResult> UpdateConversationSupplier(SupplierConversationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = (await _userManager.FindByNameAsync(User.Identity.Name));
                if (model.ConversationFile != null)
                {
                    if (model.AttachmentId != null)
                    {
                        var attachment = await _AttachmentsBiz.GetByIdAsync((int)model.AttachmentId);
                        DeleteFile("Conversation", attachment.FilePath);
                        string uniqueFileName = UploadFile(model.ConversationFile, "Conversation");
                        attachment.FilePath = uniqueFileName;
                        attachment.Title = model.ConversationFile.FileName;
                        attachment.UploadedById = user.Id;
                    }
                    else
                    {
                        string uniqueFileName = UploadFile(model.ConversationFile, "Conversation");
                        var newAttachment = new Attachment { FilePath = uniqueFileName, Title = model.ConversationFile.FileName, UploadedById = user.Id };
                        await _AttachmentsBiz.AddAsync(newAttachment);
                        model.AttachmentId = newAttachment.Id;
                    }
                }

                model.CreatedById = user.Id;
                var convarsation = _mapper.Map<SupplierConversation>(model);
                bool result = await _supplierConversationsBiz.UpdateAsync(convarsation);
                if (result)
                {
                    var quotationVM = _mapper.Map<QuotationViewModel>(await _quotationsBiz.GetInfoByIdAsync(model.QuotationId));
                    await SaveUserActivity(user.Id, UserActivities.Quotations.UpdateSupplierConversation, quotationVM.OpportunityName);

                    return Json(new { result });
                }
                return BadRequest();
            }
            else
            {
                ViewBag.CompanyData = _mapper.Map<CompanyDataViewModel>(await _companyDataBiz.GetAsync());

                return PartialView("_EditConversationSupplier", model);
            }
        }

        public async Task<IActionResult> UpdateConversationCustomer(CustomerConversationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = (await _userManager.FindByNameAsync(User.Identity.Name));
                if (model.ConversationFile != null)
                {
                    if (model.AttachmentId != null)
                    {
                        var attachment = await _AttachmentsBiz.GetByIdAsync((int)model.AttachmentId);
                        DeleteFile("Conversation", attachment.FilePath);
                        string uniqueFileName = UploadFile(model.ConversationFile, "Conversation");
                        attachment.FilePath = uniqueFileName;
                        attachment.Title = model.ConversationFile.FileName;
                        attachment.UploadedById = user.Id;
                    }
                    else
                    {
                        string uniqueFileName = UploadFile(model.ConversationFile, "Conversation");
                        var newAttachment = new Attachment { FilePath = uniqueFileName, Title = model.ConversationFile.FileName, UploadedById = user.Id };
                        await _AttachmentsBiz.AddAsync(newAttachment);
                        model.AttachmentId = newAttachment.Id;
                    }
                }

                var convarsation = _mapper.Map<CustomerConversation>(model);
                bool result = await _CustomerConversationsBiz.UpdateAsync(convarsation);
                if (result)
                {
                    var quotationVM = _mapper.Map<QuotationViewModel>(await _quotationsBiz.GetInfoByIdAsync(model.QuotationId));
                    await SaveUserActivity(user.Id, UserActivities.Quotations.UpdateCustomerConversation, quotationVM.OpportunityName);

                    return Json(new { result });
                }

                return BadRequest();
            }
            else
            {
                ViewBag.CompanyData = _mapper.Map<CompanyDataViewModel>(await _companyDataBiz.GetAsync());

                return PartialView("_EditConversationCustomer", model);
            }
        }

        public async Task<IActionResult> FinancialSessionComparison(int id)
        {
            var quotaion = await _quotationsBiz.GetByIdAsync(id);
            var quotationVM = _mapper.Map<QuotationViewModel>(quotaion);
            var selectedVersion = quotationVM.OfferVMs.SelectMany(x => x.VersionVMs).ToList().FirstOrDefault(v => v.IsSelected);
            if (selectedVersion == null)
            {
                selectedVersion = quotationVM.OfferVMs.LastOrDefault()?.VersionVMs.LastOrDefault();
            }
            var CompetitorOffers = quotationVM.CompetitorOfferVMs.GroupBy(o => o.CompetitorName).ToList();
            var CompetitorOffersAr = quotationVM.CompetitorOfferVMs.GroupBy(o => o.CompetitorName).ToList();
            var MyCompany = _mapper.Map<CompanyDataViewModel>(await _companyDataBiz.GetAsync());
            List<string> CompanyName = new();
            List<decimal> TotalPrice = new();
            List<decimal> SubTotal = new();
            List<int> NumberOfProduct = new();

            CompanyName.Add(_localizationService.IsRightToLeft() ? MyCompany.NameAr : MyCompany.Name);
            TotalPrice.Add(selectedVersion.TotalPrice);
            SubTotal.Add(selectedVersion.SubTotal);
            NumberOfProduct.Add(selectedVersion.ProductVMs.Count);
            foreach (var competitorGroup in _localizationService.IsRightToLeft() ? CompetitorOffersAr : CompetitorOffers)
            {
                if (quotationVM.CompetitorVMs.Where(c => c.IsTechnicalAccepted).Any(qc => qc.CompetitorId == competitorGroup.FirstOrDefault().CompetitorId))
                {
                    CompanyName.Add(_localizationService.IsRightToLeft() ? competitorGroup.First().CompetitorNameAr : competitorGroup.First().CompetitorName);
                    TotalPrice.Add(competitorGroup.First().ProductVMs.Sum(p => p.TotalPrice));
                    SubTotal.Add(competitorGroup.First().ProductVMs.Sum(p => p.TotalPrice));
                    NumberOfProduct.Add(competitorGroup.First().ProductVMs.Count(x => x.IsIncluded));
                }
            }
            return Json(new { CompanyName, TotalPrice, SubTotal, NumberOfProduct });
        }

        public async Task<IActionResult> ConvertToOpportunity(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var quotation = await _quotationsBiz.GetInfoByIdAsync(id);

            var opportunity = await _opportunitiesBiz.GetBasicDataByIdAsync(quotation.OpportunityId);
            opportunity.IsConverted = false;
            opportunity.ConvertedById = null;
            opportunity.IsArchived = false;
            await _opportunitiesBiz.UpdateAsync(opportunity, user.Id);

            await _quotationsBiz.DeleteAsync(id, user.Id);

            var quotationVM = _mapper.Map<QuotationViewModel>(quotation);
            await SaveUserActivity(user.Id, UserActivities.Quotations.ConvertToOpportunity, quotationVM.OpportunityName);

            return Json(true);
        }

        public class TechnicalSessionRejectionModel
        {
            public IFormFile RejectionFile { get; set; }
            public DateTime RejectionDate { get; set; }
            public DateTime FinancialSessionDate { get; set; }
            public int RejectReasonId { get; set; }
            public List<int> AcceptedCompetitorIds { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> RejectTechnicalSession(int id, TechnicalSessionRejectionModel model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var quotation = await _quotationsBiz.GetInfoByIdAsync(id);

            quotation.TechnicalSessionStatus = QuotationTechnicalSessionStatus.Rejected;
            quotation.RejectLetterDate = model.RejectionDate;
            quotation.FinancialSessionDate = model.FinancialSessionDate;
            quotation.RejectLetterPath = UploadFile(model.RejectionFile, "Quotations");
            quotation.RejectReasonId = model.RejectReasonId;
            quotation.IsArchived = true;
            var result = await _quotationsBiz.UpdateAsync(quotation, user.Id);

            if (model.AcceptedCompetitorIds != null)
            {
                foreach (int quotationCompetitorId in model.AcceptedCompetitorIds)
                {
                    var quotaitonCompetitor = await _quotationCompetitorsBiz.GetByIdAsync(quotationCompetitorId);
                    quotaitonCompetitor.IsTechnicalAccepted = true;
                    await _quotationCompetitorsBiz.UpdateAsync(quotaitonCompetitor);
                }
            }

            if (result)
            {
                var quotationVM = _mapper.Map<QuotationViewModel>(quotation);
                await SaveUserActivity(user.Id, UserActivities.Quotations.RejectTechnicalSession, quotationVM.OpportunityName);

                var project = await _projectsBiz.GetByQuotationId(quotation.Id);
                if (project != null)
                {
                    project.Status = ApplicationEnums.ProjectStatus.StoppedTechnicalSession;
                    project.EndDate = DateTime.UtcNow;
                    await _projectsBiz.UpdateAsync(project);
                }
            }

            return Json(result);
        }

        public class TechnicalSessionAcceptanceModel
        {
            public IFormFile AcceptanceLetter { get; set; }
            public DateTime AcceptanceLetterDate { get; set; }
            public DateTime FinancialSessionDate { get; set; }
            public List<int> AcceptedCompetitorIds { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> AcceptTechnicalSession(int id, TechnicalSessionAcceptanceModel model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var quotation = await _quotationsBiz.GetByIdAsync(id);

            quotation.TechnicalSessionStatus = QuotationTechnicalSessionStatus.Accepted;
            quotation.AcceptanceLetterDate = model.AcceptanceLetterDate;
            quotation.FinancialSessionDate = model.FinancialSessionDate;
            quotation.AcceptanceLetterPath = UploadFile(model.AcceptanceLetter, "Quotations");
            quotation.StatusId = (await _quotationStatusesBiz.GetByEnumValue((int)QuotationStatusEnum.FinancialSession)).Id;
            var result = await _quotationsBiz.UpdateAsync(quotation, user.Id);

            if (model.AcceptedCompetitorIds != null)
            {
                foreach (int quotationCompetitorId in model.AcceptedCompetitorIds)
                {
                    var quotaitonCompetitor = await _quotationCompetitorsBiz.GetByIdAsync(quotationCompetitorId);
                    quotaitonCompetitor.IsTechnicalAccepted = true;
                    await _quotationCompetitorsBiz.UpdateAsync(quotaitonCompetitor);
                }
            }

            var quotationVM = _mapper.Map<QuotationViewModel>(quotation);
            await SaveUserActivity(user.Id, UserActivities.Quotations.AcceptTechnicalSession, quotationVM.OpportunityName);

            return Json(result);
        }

        public async Task<IActionResult> SendToConvesation(int QoutaionId, string offerId, bool IsPrint)
        {
            var user = (await _userManager.FindByNameAsync(User.Identity.Name));
            var Conversation = new CustomerConversationViewModel();
            Conversation.IsCustomer = false;
            Conversation.QuotationId = QoutaionId;
            Conversation.CreatedDate = DateTime.UtcNow;
            Conversation.CreatedById = user.Id;
            if (IsPrint)
            {
                Conversation.Message = string.Format(_localizationService["SendEmail"].Value, offerId);
            }
            else
            {
                Conversation.Message = string.Format(_localizationService["SendPrint"].Value, offerId);
            }
            var convarsation = _mapper.Map<CustomerConversation>(Conversation);
            bool result = await _CustomerConversationsBiz.AddAsync(convarsation);

            var quotationVM = _mapper.Map<QuotationViewModel>(await _quotationsBiz.GetInfoByIdAsync(QoutaionId));
            await SaveUserActivity(user.Id, UserActivities.Quotations.AddCustomerConversation, quotationVM.OpportunityName);

            return Json(result);
        }

        public async Task<IActionResult> FinancialDetails(int id, int? competitorId)
        {
            var offers = _mapper.Map<List<CompetitorOfferViewModel>>(await _competitorOffersBiz.GetByQuotationIdAsync(id));
            ViewData["Title"] = (await _quotationsBiz.GetNameByIdAsync(id)).Opportunity.Name;
            ViewData["QuotationId"] = id;

            var competitorVMs = _mapper.Map<List<QuotationCompetitorViewModel>>(await _quotationCompetitorsBiz.GetByQuotationId(id));
            ViewData["CompetitorVMs"] = competitorVMs.Where(c => c.IsTechnicalAccepted).ToList();

            ViewData["CompetitorId"] = competitorId;


            // start
            var quotation = await _quotationsBiz.GetByIdAsync(id);
            var quotationVM = _mapper.Map<QuotationViewModel>(quotation);
            List<IGrouping<string, CompetitorOfferViewModel>> competitorsGroup = new();

            if (_localizationService.IsRightToLeft())
            {
                competitorsGroup = quotationVM.CompetitorOfferVMs.GroupBy(o => o.CompetitorNameAr).ToList();
            }
            else
            {
                competitorsGroup = quotationVM.CompetitorOfferVMs.GroupBy(o => o.CompetitorName).ToList();
            }


            var companyData = _mapper.Map<CompanyDataViewModel>(await _companyDataBiz.GetAsync());

            var lastOfferVersion = quotationVM.OfferVMs.SelectMany(x => x.VersionVMs).FirstOrDefault(v => v.IsSelected);
            if (lastOfferVersion == null)
            {
                lastOfferVersion = quotationVM.OfferVMs.SelectMany(x => x.VersionVMs).LastOrDefault();
            }
            var quotationOffer = await _quotationOffersBiz.GetByQuotationId(id);

            List<QuotationFinancialDetailsViewModel> quotationGroups = new();
            try
            {
                quotationGroups.Add(new QuotationFinancialDetailsViewModel
                {
                    Id = lastOfferVersion.Id,
                    ImagePath = companyData.ImagePath,
                    Name = _localizationService.IsRightToLeft() ? companyData.NameAr : companyData.Name,
                    SubTotal = lastOfferVersion.SubTotal,
                    TotalPrice = lastOfferVersion.TotalPrice,
                    IsCompetitor = false,
                    OfferVersionVM = lastOfferVersion
                });

                foreach (var competitor in competitorsGroup)
                {
                    if (quotationVM.CompetitorVMs.Where(c => c.IsTechnicalAccepted).Any(qc => qc.CompetitorId == competitor.FirstOrDefault().CompetitorId))
                    {
                        quotationGroups.Add(new QuotationFinancialDetailsViewModel
                        {
                            Id = competitor.First().CompetitorId,
                            ImagePath = competitor.First().CompetitorImagePath,
                            Name = competitor.Key,
                            SubTotal = competitor.FirstOrDefault().TotalPrice,
                            TotalPrice = competitor.FirstOrDefault().TotalPrice,
                            IsCompetitor = true,
                            CompetitorOfferVMs = competitor.ToList()
                        });
                    }
                }
            }
            catch { }
            ViewData["Origins"] = await _countriesBiz.GetAllUnarchivedAsync();

            ViewData["QuotationGroups"] = quotationGroups;
            //ViewData["alternateVersions"] = _mapper.Map<List<QuotationOffersBiz>>(await _quotationOffersBiz.GetByQuotationId(Id));

            return View(offers);
        }

        public async Task<IActionResult> GetCompetitorOfferProducts(int quotationId, bool isCompetitor, int? competitorId = null)
        {

            //ViewData["alternateVersions"] = _mapper.Map<List<AlternateVersionViewModel>>(await _alternateVersionsBiz.GetByVersionIdAsync(offerId));

            if (isCompetitor)
            {
                var offer = _mapper.Map<List<CompetitorOfferViewModel>>(await _competitorOffersBiz.GetByQuotationIdAsync(quotationId));
                foreach (var products in offer)
                {
                    foreach (var product in products.ProductVMs)
                    {
                        product.OriginIds = product.OriginVMs != null ? product.OriginVMs.Select(x => x.CountryId).ToList() : new List<int>();
                    }
                }

                ViewData["Origins"] = await _countriesBiz.GetAllUnarchivedAsync();
                ViewData["QuotationId"] = offer.Select(x => x.QuotationId);
                return PartialView("_CompetitorOfferProductsPartial", offer.Where(x => x.CompetitorId == competitorId).ToList());

            }
            else
            {
                var Quotation = _mapper.Map<List<QuotationOfferViewModel>>(await _quotationOffersBiz.GetByQuotationId(quotationId));
                ViewData["Origins"] = await _countriesBiz.GetAllUnarchivedAsync();
                ViewData["QuotationId"] = Quotation.Select(x => x.QuotationId);
                return PartialView("_CompanyOfferProductsPartial", Quotation);

            }

        }

        public class FinancialDetailsModel
        {
            public int CompetitorOfferId { get; set; }
            public IList<CompetitorOfferProductViewModel> ProductVMs { get; set; }
        }

        public async Task<IActionResult> SaveFinancialDetails(FinancialDetailsModel model)
        {
            var user = (await _userManager.FindByNameAsync(User.Identity.Name));

            foreach (var product in model.ProductVMs)
            {
                product.Id = 0;
                if (product.IsIncluded)
                {
                    product.OriginVMs = new List<CompetitorOfferProductOriginViewModel>();
                    foreach (var originId in product.OriginIds)
                    {
                        product.OriginVMs.Add(new CompetitorOfferProductOriginViewModel
                        {
                            CountryId = originId
                        });
                    }
                }
            }

            var offerProducts = _mapper.Map<List<CompetitorOfferProduct>>(model.ProductVMs);
            await _competitorOffersBiz.UpdateOfferProducts(offerProducts);
            var competitorOfferVM = _mapper.Map<CompetitorViewModel>(await _competitorsBiz.GetByIdAsync(model.CompetitorOfferId));
            await SaveUserActivity(user.Id, UserActivities.Quotations.SaveFinancialDetails, competitorOfferVM.Name);

            return Json(true);
        }

        public async Task<IActionResult> NegotiationResults(int id)
        {
            var quotationVM = _mapper.Map<QuotationViewModel>(await _quotationsBiz.GetByIdAsync(id));

            var lastOfferVersion = quotationVM.OfferVMs.SelectMany(x => x.VersionVMs).FirstOrDefault(v => v.IsSelected);
            if (lastOfferVersion == null)
            {
                lastOfferVersion = quotationVM.OfferVMs.SelectMany(x => x.VersionVMs).LastOrDefault();
            }
            ViewData["LastOfferVersion"] = lastOfferVersion;
            NegotiationResultsViewModel model = new();
            //{
            //    Id = id,
            //    NegotiationResultsVM = quotationVM.NegotiationResultsVM,
            //    OtherNegotiationResultVMs = quotationVM.OtherNegotiationResultVMs,
            //    OldOfferValue = lastOfferVersion.SubTotal,
            //    OldDownPaymentPercentage = lastOfferVersion.DownPaymentPercentage,
            //    OldUponDeliveryPercentage = lastOfferVersion.UponDeliveryPercentage,
            //    OldAfterInstallationPercentage = lastOfferVersion.AfterInstallationPercentage,
            //    OldDeliveryFromDays = lastOfferVersion.DeliveryFromDays,
            //    OldDeliveryToDays = lastOfferVersion.DeliveryToDays
            //};


            ViewData["Title"] = quotationVM.OpportunityName;
            return View(model);
        }

        public async Task<IActionResult> NegotiationResultPartial(int id, int offerId, bool isAlternate)
        {
            var quotationVM = _mapper.Map<QuotationViewModel>(await _quotationsBiz.GetByIdAsync(id));

            var lastOfferVersion = quotationVM.OfferVMs.SelectMany(x => x.VersionVMs).FirstOrDefault(v => v.IsSelected);
            if (lastOfferVersion == null)
            {
                lastOfferVersion = quotationVM.OfferVMs.SelectMany(x => x.VersionVMs).LastOrDefault();
            }
            ViewData["LastOfferVersion"] = lastOfferVersion;

            NegotiationResultsViewModel model = new();
            if (!isAlternate)
            {
                model.Id = id;
                model.NegotiationResultsVM = quotationVM.NegotiationResultsVM;
                model.OtherNegotiationResultVMs = quotationVM.OtherNegotiationResultVMs;
                model.OldOfferValue = lastOfferVersion.SubTotal;
                model.OldDownPaymentPercentage = lastOfferVersion.DownPaymentPercentage;
                model.OldUponDeliveryPercentage = lastOfferVersion.UponDeliveryPercentage;
                model.OldAfterInstallationPercentage = lastOfferVersion.AfterInstallationPercentage;
                model.OldDeliveryFromDays = lastOfferVersion.DeliveryFromDays;
                model.OldDeliveryToDays = lastOfferVersion.DeliveryToDays;
            }
            else
            {
                model.Id = id;
                model.NegotiationResultsVM = quotationVM.NegotiationResultsVM;
                model.OtherNegotiationResultVMs = quotationVM.OtherNegotiationResultVMs;
                model.OldOfferValue = lastOfferVersion.AlternateVMs.FirstOrDefault(x => x.Id == offerId).SubTotal;
                model.OldDownPaymentPercentage = lastOfferVersion.AlternateVMs.FirstOrDefault(x => x.Id == offerId).DownPaymentPercentage;
                model.OldUponDeliveryPercentage = lastOfferVersion.AlternateVMs.FirstOrDefault(x => x.Id == offerId).UponDeliveryPercentage;
                model.OldAfterInstallationPercentage = lastOfferVersion.AlternateVMs.FirstOrDefault(x => x.Id == offerId).AfterInstallationPercentage;
                model.OldDeliveryFromDays = lastOfferVersion.AlternateVMs.FirstOrDefault(x => x.Id == offerId).DeliveryFromDays;
                model.OldDeliveryToDays = lastOfferVersion.AlternateVMs.FirstOrDefault(x => x.Id == offerId).DeliveryToDays;

            }

            ViewData["Title"] = quotationVM.OpportunityName;
            return PartialView("_NegotiationResultsPartial", model);
        }

        [HttpPost]
        public async Task<IActionResult> NegotiationResults(NegotiationResultsViewModel model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (model.NegotiationResultsVM != null)
            {
                if (model.NegotiationResultsVM.DownPaymentPercentage + model.NegotiationResultsVM.UponDeliveryPercentage +
                    model.NegotiationResultsVM.AfterInstallationPercentage != 100)
                {
                    ModelState.AddModelError("NegotiationResultsVM.DownPaymentPercentage", _localizationService["CompletePaymentTerms"].Value);
                }
            }
            if (ModelState.IsValid)
            {
                var quotationVM = _mapper.Map<QuotationViewModel>(await _quotationsBiz.GetInfoByIdAsync(model.Id));

                var negotiationResult = await _negotiationResultsBiz.GetByQuotationIdAsync(model.Id);
                if (negotiationResult != null)
                {
                    negotiationResult.OfferValue = model.NegotiationResultsVM.OfferValue;
                    negotiationResult.DownPaymentPercentage = model.NegotiationResultsVM.DownPaymentPercentage;
                    negotiationResult.UponDeliveryPercentage = model.NegotiationResultsVM.UponDeliveryPercentage;
                    negotiationResult.AfterInstallationPercentage = model.NegotiationResultsVM.AfterInstallationPercentage;
                    negotiationResult.DeliveryFromDays = model.NegotiationResultsVM.DeliveryFromDays;
                    negotiationResult.DeliveryToDays = model.NegotiationResultsVM.DeliveryToDays;

                    await _negotiationResultsBiz.UpdateAsync(negotiationResult);

                    await SaveUserActivity(user.Id, UserActivities.Quotations.UpdateNegotiationResults, quotationVM.OpportunityName);
                }
                else
                {
                    negotiationResult = new NegotiationResult
                    {
                        QuotationId = model.Id,
                        OfferValue = model.NegotiationResultsVM.OfferValue,
                        DownPaymentPercentage = model.NegotiationResultsVM.DownPaymentPercentage,
                        UponDeliveryPercentage = model.NegotiationResultsVM.UponDeliveryPercentage,
                        AfterInstallationPercentage = model.NegotiationResultsVM.AfterInstallationPercentage,
                        DeliveryFromDays = model.NegotiationResultsVM.DeliveryFromDays,
                        DeliveryToDays = model.NegotiationResultsVM.DeliveryToDays
                    };

                    await _negotiationResultsBiz.AddAsync(negotiationResult);

                    await SaveUserActivity(user.Id, UserActivities.Quotations.AddNegotiationResults, quotationVM.OpportunityName);
                }

                await _otherNegotiationResultsBiz.DeleteByQuotationIdAsync(model.Id);
                if (model.OtherNegotiationResultVMs != null)
                {
                    foreach (var otherResult in model.OtherNegotiationResultVMs)
                    {
                        await _otherNegotiationResultsBiz.AddAsync(new OtherNegotiationResult
                        {
                            QuotationId = model.Id,
                            Title = otherResult.Title,
                            ValueBefore = otherResult.ValueBefore,
                            ValueAfter = otherResult.ValueAfter
                        });
                    }
                }

                return RedirectToAction(nameof(Details), new { id = model.Id });
            }

            ViewData["Title"] = model.Title;
            return View(model);
        }

        [HttpPost]
        public IActionResult AddOtherNegotiationResult(NegotiationResultsViewModel model)
        {
            if (model.OtherNegotiationResultVMs == null) model.OtherNegotiationResultVMs = new List<OtherNegotiationResultViewModel>();
            model.OtherNegotiationResultVMs.Add(new OtherNegotiationResultViewModel());

            return PartialView("_NegotiationResultsPartial", model);
        }

        [HttpPost]
        public IActionResult DeleteOtherNegotiationResult(int index, NegotiationResultsViewModel model)
        {
            model.OtherNegotiationResultVMs.RemoveAt(index);

            return PartialView("_NegotiationResultsPartial", model);
        }

        public async Task<IActionResult> GetMassageNegotiationSupplier(int id, int supplierId, DateTime? date)
        {
            var negotiations = _mapper.Map<List<SupplierNegotiationViewModel>>(await _supplierNegotiationBiz.GetSupplierNegotiation(supplierId, id, date));
            return PartialView("_SupplierNegotiationPartial", negotiations);
        }

        public async Task<IActionResult> CreateNegotiationSupplier(SupplierNegotiationViewModel model)
        {
            bool result = false;

            if (ModelState.IsValid)
            {
                var user = (await _userManager.FindByNameAsync(User.Identity.Name));
                if (model.ConversationFile != null)
                {
                    string uniqueFileName = UploadFile(model.ConversationFile, "Negotiation");
                    var attachment = new Attachment { FilePath = uniqueFileName, Title = model.ConversationFile.FileName, UploadedById = user.Id };
                    var fileAttachment = await _AttachmentsBiz.AddAsync(attachment);
                    model.AttachmentId = attachment.Id;
                }
                model.CreatedById = user.Id;
                var convarsation = _mapper.Map<SupplierNegotiation>(model);
                result = await _supplierNegotiationBiz.AddAsync(convarsation);
                if (result)
                {
                    var quotationVM = _mapper.Map<QuotationViewModel>(await _quotationsBiz.GetInfoByIdAsync(model.QuotationId));
                    await SaveUserActivity(user.Id, UserActivities.Quotations.AddSupplierNegotiation, quotationVM.OpportunityName);

                    return RedirectToAction(nameof(Index));
                }
            }
            return Json(result);
        }

        public async Task<IActionResult> EditNegotiationSupplier(int id, int quotationId)
        {
            ViewBag.CompanyData = _mapper.Map<CompanyDataViewModel>(await _companyDataBiz.GetAsync());
            var massages = _mapper.Map<SupplierNegotiationViewModel>(await _supplierNegotiationBiz.GetByIdAsync(id));
            return PartialView("_EditNegotiationSupplier", massages);
        }

        public async Task<IActionResult> UpdateNegotiationSupplier(SupplierNegotiationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = (await _userManager.FindByNameAsync(User.Identity.Name));
                if (model.ConversationFile != null)
                {
                    if (model.AttachmentId != null)
                    {
                        var attachment = await _AttachmentsBiz.GetByIdAsync((int)model.AttachmentId);
                        DeleteFile("Conversation", attachment.FilePath);
                        string uniqueFileName = UploadFile(model.ConversationFile, "Negotiation");
                        attachment.FilePath = uniqueFileName;
                        attachment.Title = model.ConversationFile.FileName;
                        attachment.UploadedById = user.Id;
                    }
                    else
                    {
                        string uniqueFileName = UploadFile(model.ConversationFile, "Negotiation");
                        var newAttachment = new Attachment { FilePath = uniqueFileName, Title = model.ConversationFile.FileName, UploadedById = user.Id };
                        await _AttachmentsBiz.AddAsync(newAttachment);
                        model.AttachmentId = newAttachment.Id;
                    }
                }

                model.CreatedById = user.Id;
                var convarsation = _mapper.Map<SupplierNegotiation>(model);
                bool result = await _supplierNegotiationBiz.UpdateAsync(convarsation);
                if (result)
                {
                    var quotationVM = _mapper.Map<QuotationViewModel>(await _quotationsBiz.GetInfoByIdAsync(model.QuotationId));
                    await SaveUserActivity(user.Id, UserActivities.Quotations.UpdateSupplierNegotiation, quotationVM.OpportunityName);

                    return Json(new { result });
                }
                return BadRequest();
            }
            else
            {
                ViewBag.CompanyData = _mapper.Map<CompanyDataViewModel>(await _companyDataBiz.GetAsync());

                return PartialView("_EditNegotiationSupplier", model);
            }
        }

        public async Task<IActionResult> ArchiveNegotiationSupplier(int id)
        {
            var result = await _supplierNegotiationBiz.ArchiveAsync(id);
            return Json(result);
        }

        public async Task<IActionResult> GetNegotiationCustomer(int id, DateTime? date)
        {
            var quotation = await _quotationsBiz.GetInfoByIdAsync(id);
            ViewBag.CustomerLogoPath = quotation.Opportunity.Customer.LogoAttachment?.FilePath;

            var massages = _mapper.Map<List<CustomerNegotiationViewModel>>(await _customerNegotiationBiz.GetCustomerNegotiation(id, date));
            return PartialView("_CustomerNegotiationPartial", massages);
        }

        public async Task<IActionResult> CreateNegotationCustomer(CustomerNegotiationViewModel model)
        {
            bool result = false;

            if (ModelState.IsValid)
            {
                var user = (await _userManager.FindByNameAsync(User.Identity.Name));
                if (model.ConversationFile != null)
                {
                    string uniqueFileName = UploadFile(model.ConversationFile, "Negotiation");
                    var attachment = new Attachment { FilePath = uniqueFileName, Title = model.ConversationFile.FileName, UploadedById = user.Id };
                    var fileAttachment = await _AttachmentsBiz.AddAsync(attachment);
                    model.AttachmentId = attachment.Id;
                }
                model.CreatedById = user.Id;
                var convarsation = _mapper.Map<CustomerNegotiation>(model);
                result = await _customerNegotiationBiz.AddAsync(convarsation);
                if (result)
                {
                    var quotationVM = _mapper.Map<QuotationViewModel>(await _quotationsBiz.GetInfoByIdAsync(model.QuotationId));
                    await SaveUserActivity(user.Id, UserActivities.Quotations.AddCustomerNegotiation, quotationVM.OpportunityName);

                    return RedirectToAction(nameof(Index));
                }
            }
            return Json(result);
        }

        public async Task<IActionResult> EditNegotiationCustomer(int id, int quotationId)
        {
            ViewBag.CompanyData = _mapper.Map<CompanyDataViewModel>(await _companyDataBiz.GetAsync());
            var massagesCutomer = _mapper.Map<CustomerNegotiationViewModel>(await _customerNegotiationBiz.GetByIdAsync(id));
            return PartialView("_EditNegotiationCustomer", massagesCutomer);
        }

        public async Task<IActionResult> UpdateNegotiationCustomer(CustomerNegotiationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = (await _userManager.FindByNameAsync(User.Identity.Name));
                if (model.ConversationFile != null)
                {
                    if (model.AttachmentId != null)
                    {
                        var attachment = await _AttachmentsBiz.GetByIdAsync((int)model.AttachmentId);
                        DeleteFile("Conversation", attachment.FilePath);
                        string uniqueFileName = UploadFile(model.ConversationFile, "Negotiation");
                        attachment.FilePath = uniqueFileName;
                        attachment.Title = model.ConversationFile.FileName;
                        attachment.UploadedById = user.Id;
                    }
                    else
                    {
                        string uniqueFileName = UploadFile(model.ConversationFile, "Negotiation");
                        var newAttachment = new Attachment { FilePath = uniqueFileName, Title = model.ConversationFile.FileName, UploadedById = user.Id };
                        await _AttachmentsBiz.AddAsync(newAttachment);
                        model.AttachmentId = newAttachment.Id;
                    }
                }

                var convarsation = _mapper.Map<CustomerNegotiation>(model);
                bool result = await _customerNegotiationBiz.UpdateAsync(convarsation);
                if (result)
                {
                    var quotationVM = _mapper.Map<QuotationViewModel>(await _quotationsBiz.GetInfoByIdAsync(model.QuotationId));
                    await SaveUserActivity(user.Id, UserActivities.Quotations.UpdateCustomerNegotiation, quotationVM.OpportunityName);

                    return Json(new { result });
                }

                return BadRequest();
            }
            else
            {
                ViewBag.CompanyData = _mapper.Map<CompanyDataViewModel>(await _companyDataBiz.GetAsync());

                return PartialView("_EditConversationCustomer", model);
            }
        }

        public async Task<IActionResult> ArchiveNegotiationCustomer(int id)
        {
            var result = await _customerNegotiationBiz.ArchiveAsync(id);
            return Json(result);
        }

        public class FinancialSessionRejectionModel
        {
            public IFormFile RejectionFile { get; set; }
            public DateTime RejectionDate { get; set; }
            public int RejectReasonId { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> RejectFinancialSession(int id, FinancialSessionRejectionModel model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var quotation = await _quotationsBiz.GetByIdAsync(id);

            quotation.FinancialSessionStatus = QuotationFinancialSessionStatus.Rejected;
            quotation.RejectLetterDate = model.RejectionDate;
            quotation.RejectLetterPath = UploadFile(model.RejectionFile, "Quotations");
            quotation.RejectReasonId = model.RejectReasonId;
            quotation.IsArchived = true;
            var result = await _quotationsBiz.UpdateAsync(quotation, user.Id);
            if (result)
            {
                var quotationVM = _mapper.Map<QuotationViewModel>(quotation);
                await SaveUserActivity(user.Id, UserActivities.Quotations.RejectFinancialSession, quotationVM.OpportunityName);

                var project = await _projectsBiz.GetByQuotationId(quotation.Id);
                if (project != null)
                {
                    project.Status = ProjectStatus.StoppedFinanialSession;
                    project.EndDate = DateTime.UtcNow;
                    await _projectsBiz.UpdateAsync(project);
                }
            }
            return Json(result);
        }

        public FinalLG ProcessFinalLG(LetterOfGuaranteeViewModel model, FinalLG finalLG, int departmentId, string userId)
        {
            if (model.AttachmentFile != null)
            {
                finalLG.AttachmentTitle = model.AttachmentFile.FileName;
                finalLG.AttachmentPath = UploadFile(model.AttachmentFile, "Quotations");
            }

            if (model.ReceiptAttachmentFile != null)
            {
                finalLG.ReceiptAttachmentTitle = model.ReceiptAttachmentFile.FileName;
                finalLG.ReceiptAttachmentPath = UploadFile(model.ReceiptAttachmentFile, "Quotations");
            }

            if (model.IsRequested)
            {
                FinalLGRequest finalLGRequest = new();

                if (model.RequestVM == null)
                {
                    finalLGRequest.RequestedById = userId;
                    finalLGRequest.RequestedDate = DateTime.UtcNow;
                    finalLGRequest.Status = RequestStatusEnum.Pending;
                }
                else
                {
                    finalLGRequest = _mapper.Map<FinalLGRequest>(model.RequestVM);

                    if (model.IsSent && !model.RequestVM.IsSent)
                    {
                        finalLGRequest.IsSent = true;
                        finalLGRequest.SentById = userId;
                        finalLGRequest.SentDate = DateTime.UtcNow;
                    }

                    if (model.IsApproved && model.RequestVM.Status != RequestStatusEnum.Approved)
                    {
                        finalLGRequest.Status = RequestStatusEnum.Approved;
                        finalLGRequest.StatusById = userId;
                        finalLGRequest.StatusDate = DateTime.UtcNow;
                    }
                    else if (model.IsRejected && model.RequestVM.Status != RequestStatusEnum.Rejected)
                    {
                        finalLGRequest.Status = RequestStatusEnum.Rejected;
                        finalLGRequest.StatusById = userId;
                        finalLGRequest.StatusDate = DateTime.UtcNow;
                    }

                    if (model.IsPrinted && !model.RequestVM.IsPrinted)
                    {
                        finalLGRequest.IsPrinted = true;
                        finalLGRequest.PrintedById = userId;
                        finalLGRequest.PrintedDate = DateTime.UtcNow;
                    }
                }

                finalLGRequest.DepartmentId = departmentId;
                finalLG.Request = finalLGRequest;
            }

            return finalLG;
        }

        public PerformanceLG ProcessPerformanceLG(LetterOfGuaranteeViewModel model, PerformanceLG performanceLG, int departmentId, string userId)
        {
            if (model.AttachmentFile != null)
            {
                performanceLG.AttachmentTitle = model.AttachmentFile.FileName;
                performanceLG.AttachmentPath = UploadFile(model.AttachmentFile, "Quotations");
            }

            if (model.ReceiptAttachmentFile != null)
            {
                performanceLG.ReceiptAttachmentTitle = model.ReceiptAttachmentFile.FileName;
                performanceLG.ReceiptAttachmentPath = UploadFile(model.ReceiptAttachmentFile, "Quotations");
            }

            if (model.AdvanceAttachmentFile != null)
            {
                performanceLG.AdvanceAttachmentTitle = model.AdvanceAttachmentFile.FileName;
                performanceLG.AdvanceAttachmentPath = UploadFile(model.AdvanceAttachmentFile, "Quotations");
            }

            if (model.IsRequested)
            {
                PerformanceLGRequest performanceLGRequest = new();

                if (model.RequestVM == null)
                {
                    performanceLGRequest.RequestedById = userId;
                    performanceLGRequest.RequestedDate = DateTime.UtcNow;
                    performanceLGRequest.Status = RequestStatusEnum.Pending;
                }
                else
                {
                    performanceLGRequest = _mapper.Map<PerformanceLGRequest>(model.RequestVM);

                    if (model.IsSent && !model.RequestVM.IsSent)
                    {
                        performanceLGRequest.IsSent = true;
                        performanceLGRequest.SentById = userId;
                        performanceLGRequest.SentDate = DateTime.UtcNow;
                    }

                    if (model.IsApproved && model.RequestVM.Status != RequestStatusEnum.Approved)
                    {
                        performanceLGRequest.Status = RequestStatusEnum.Approved;
                        performanceLGRequest.StatusById = userId;
                        performanceLGRequest.StatusDate = DateTime.UtcNow;
                    }
                    else if (model.IsRejected && model.RequestVM.Status != RequestStatusEnum.Rejected)
                    {
                        performanceLGRequest.Status = RequestStatusEnum.Rejected;
                        performanceLGRequest.StatusById = userId;
                        performanceLGRequest.StatusDate = DateTime.UtcNow;
                    }

                    if (model.IsPrinted && !model.RequestVM.IsPrinted)
                    {
                        performanceLGRequest.IsPrinted = true;
                        performanceLGRequest.PrintedById = userId;
                        performanceLGRequest.PrintedDate = DateTime.UtcNow;
                    }
                }

                performanceLGRequest.DepartmentId = departmentId;
                performanceLG.Request = performanceLGRequest;
            }

            return performanceLG;
        }

        public async Task<IActionResult> AcceptFinancialSession(int id, FinancialSessionAcceptanceModel model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var quotation = await _quotationsBiz.GetByIdAsync(id);

            quotation.FinancialSessionStatus = QuotationFinancialSessionStatus.Accepted;
            quotation.AwardingLetterDate = model.AwardingLetterDate;
            quotation.AwardingLetterPath = UploadFile(model.AwardingLetter, "Quotations");
            quotation.StatusId = (await _quotationStatusesBiz.GetByEnumValue((int)QuotationStatusEnum.Approved)).Id;
            quotation.LGType = (LetterOfGuaranteeType)model.LGType;
            switch (quotation.LGType)
            {
                case LetterOfGuaranteeType.FinalLG:
                    quotation.FinalLG = ProcessFinalLG(model.FinalLGVM, new FinalLG(), quotation.Opportunity.DepartmentId, user.Id);
                    break;
                case LetterOfGuaranteeType.PerformanceAndFinalLG:
                    quotation.FinalLG = ProcessFinalLG(model.FinalLGVM, new FinalLG(), quotation.Opportunity.DepartmentId, user.Id);

                    quotation.PerformanceLG = ProcessPerformanceLG(model.PerformanceLGVM, new PerformanceLG(), quotation.Opportunity.DepartmentId, user.Id);
                    break;
            }
            var result = await _quotationsBiz.UpdateAsync(quotation, user.Id);

            var quotationVM = _mapper.Map<QuotationViewModel>(quotation);
            await SaveUserActivity(user.Id, UserActivities.Quotations.AcceptFinancialSession, quotationVM.OpportunityName);

            return Json(result);
        }

        public class LetterOfGuaranteesData
        {
            public LetterOfGuaranteeViewModel PerformanceLGVM { get; set; }
            public LetterOfGuaranteeViewModel FinalLGVM { get; set; }
        }

        public async Task<IActionResult> SaveLetterOfGuarantees(int id, LetterOfGuaranteesData model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var quotation = await _quotationsBiz.GetInfoByIdAsync(id);

            switch (quotation.LGType)
            {
                case LetterOfGuaranteeType.FinalLG:
                    var finalLG = _mapper.Map<FinalLG>(model.FinalLGVM);
                    finalLG = ProcessFinalLG(model.FinalLGVM, finalLG, quotation.Opportunity.DepartmentId, user.Id);
                    await _finalLGsBiz.UpdateAsync(finalLG);
                    break;
                case LetterOfGuaranteeType.PerformanceAndFinalLG:
                    finalLG = _mapper.Map<FinalLG>(model.FinalLGVM);
                    finalLG = ProcessFinalLG(model.FinalLGVM, finalLG, quotation.Opportunity.DepartmentId, user.Id);
                    await _finalLGsBiz.UpdateAsync(finalLG);

                    var performanceLG = _mapper.Map<PerformanceLG>(model.PerformanceLGVM);
                    performanceLG = ProcessPerformanceLG(model.PerformanceLGVM, performanceLG, quotation.Opportunity.DepartmentId, user.Id);
                    await _performanceLGsBiz.UpdateAsync(performanceLG);
                    break;
            }

            var quotationVM = _mapper.Map<QuotationViewModel>(quotation);
            await SaveUserActivity(user.Id, UserActivities.Quotations.SaveLGs, quotationVM.OpportunityName);

            return Json(true);
        }

        public async Task<IActionResult> GetLetterOfGuarantees(int id)
        {
            var quotationVM = _mapper.Map<QuotationViewModel>(await _quotationsBiz.GetWithLGByIdAsync(id));

            var departmentBanks = await _departmentBanksBiz.GetByDepartmentIdAsync((int)quotationVM.OpportunityVM.DepartmentId);
            ViewData["DepartmentBankId"] = departmentBanks.Select(db => db.Branch.Bank).ToList();

            ViewData["BankId"] = await _banksBiz.GetAllUnarchivedAsync();

            ViewData["AssignedToId"] = await _userManager.GetAllUsersAsync();

            ViewData["PerformancePeriods"] = new List<Period>
            {
                new Period
                {
                    Text = _localizationService["OnePeriod"].Value,
                    Value = 1
                },
                new Period
                {
                     Text = _localizationService["TwoPeriods"].Value,
                    Value = 2
                }
            };

            ViewData["FinalPeriods"] = new List<Period>
            {
                new Period
                {
                     Text = _localizationService["TwoPeriods"].Value,
                    Value = 2
                },
                new Period
                {
                    Text = _localizationService["FourPeriods"].Value,
                    Value = 4
                },
                new Period
                {
                    Text = _localizationService["SixPeriods"].Value,
                    Value = 6
                },
            };

            return PartialView("_LetterOfGuaranteesPartial", quotationVM);
        }

        public async Task<IActionResult> GetBankDetails(int bankId)
        {
            var bank = _mapper.Map<BankViewModel>(await _banksBiz.GetByIdAsync(bankId));

            return Json(bank, new JsonSerializerOptions
            {
                PropertyNamingPolicy = null,
                ReferenceHandler = ReferenceHandler.Preserve
            });
        }

        public async Task<IActionResult> GetBankBranches(int bankId)
        {
            var branches = await _banksBiz.GetBranchesAsync(bankId);

            return PartialView("_BankBranchOptionsPartial", branches);
        }

        public IActionResult CalculateExpiryDate(DateTime? issueDate, int periodNo)
        {
            if (issueDate == null)
            {
                issueDate = DateTime.UtcNow;
            }
            var expiryDate = issueDate.Value.AddMonths(3 * periodNo).AddDays(-1);
            return Json(new
            {
                issueDate = issueDate.Value.ToString("yyyy-MM-dd"),
                expiryDate = expiryDate.ToString("yyyy-MM-dd")
            });
        }

        [Authorize(Permissions.Quotations.PrintLG)]
        public async Task<IActionResult> PrintLGRequest(int requestId, bool isFinal)
        {
            if (isFinal)
            {
                var request = _mapper.Map<LetterOfGuaranteeRequestViewModel>(await _finalLGRequestsBiz.GetByIdAsync(requestId));

                ViewData["IsFinal"] = true;
                return View(request);
            }
            else
            {
                var request = _mapper.Map<LetterOfGuaranteeRequestViewModel>(await _performanceLGRequestsBiz.GetByIdAsync(requestId));

                ViewData["IsFinal"] = false;
                return View(request);
            }
        }

        public async Task<IActionResult> Comparison(int id, List<int> companyIds)
        {
            var productVMs = _mapper.Map<List<TechnicalSpecificationProductViewModel>>(await _technicalSpecificationProductsBiz.GetByQuotationIdAsync(id));
            var quotationOfferVMs = _mapper.Map<List<QuotationOfferViewModel>>(await _quotationOffersBiz.GetByQuotationId(id));
            var competitorOfferVMs = _mapper.Map<List<CompetitorOfferViewModel>>(await _competitorOffersBiz.GetByQuotationIdAsync(id));

            List<QuotationComparisonViewModel> comparisonVM = new();
            foreach (int companyId in companyIds)
            {
                if (companyId == 0)
                {
                    var companyData = _mapper.Map<CompanyDataViewModel>(await _companyDataBiz.GetAsync());
                    var lastOfferVersion = quotationOfferVMs.SelectMany(x => x.VersionVMs).FirstOrDefault(v => v.IsSelected);
                    if (lastOfferVersion == null)
                    {
                        lastOfferVersion = quotationOfferVMs.SelectMany(x => x.VersionVMs).LastOrDefault();
                    }

                    comparisonVM.Add(new QuotationComparisonViewModel
                    {
                        CompanyName = _localizationService.IsRightToLeft() ? companyData.NameAr : companyData.Name,
                        ImagePath = companyData.ImagePath,
                        ComparisonProductVMs = lastOfferVersion.ProductVMs.Select(p => new QuotationComparisonProductViewModel
                        {
                            Id = p.ProductVM.TechnicalSpecificationProductId,
                            Name = _localizationService.IsRightToLeft() ? p.ProductNameAr : p.ProductName,
                            TotalPrice = p.Total
                        }).ToList(),
                        ComparisonOfferVM = new QuotationComparisonOfferViewModel
                        {
                            AfterInstallationPercentage = lastOfferVersion.AfterInstallationPercentage,
                            CertificateNames = _localizationService.IsRightToLeft() ? lastOfferVersion.CertificateNamesStrAr : lastOfferVersion.CertificateNamesStr,
                            CurrencyName = _localizationService.IsRightToLeft() ? lastOfferVersion.CurrencyNameAr : lastOfferVersion.CurrencyName,
                            DeliveryFromDays = lastOfferVersion.DeliveryFromDays,
                            DeliveryPlaceName = _localizationService.IsRightToLeft() ? lastOfferVersion.DeliveryPlaceNameAr : lastOfferVersion.DeliveryPlaceName,
                            DeliveryToDays = lastOfferVersion.DeliveryToDays,
                            DownPaymentPercentage = lastOfferVersion.DownPaymentPercentage,
                            ExchangeRate = lastOfferVersion.Factor,
                            GuaranteeName = _localizationService.IsRightToLeft() ? lastOfferVersion.GuaranteeTermNameAr : lastOfferVersion.GuaranteeTermName,
                            IsVATIncluded = true,
                            Notes = lastOfferVersion.Notes,
                            OriginDocumentName = _localizationService.IsRightToLeft() ? lastOfferVersion.OriginDocumentNameAr : lastOfferVersion.OriginDocumentName,
                            SupplierName = _localizationService.IsRightToLeft() ? lastOfferVersion.SupplierNameAr : lastOfferVersion.SupplierName,
                            UponDeliveryPercentage = lastOfferVersion.UponDeliveryPercentage,
                            ValidityName = _localizationService.IsRightToLeft() ? lastOfferVersion.ValidityNameAr : lastOfferVersion.ValidityName
                        }
                    });
                }
                else
                {
                    var competitorOffer = competitorOfferVMs.FirstOrDefault(o => o.CompetitorId == companyId);
                    comparisonVM.Add(new QuotationComparisonViewModel
                    {
                        CompanyName = _localizationService.IsRightToLeft() ? competitorOffer.CompetitorNameAr : competitorOffer.CompetitorName,
                        Id = competitorOffer.CompetitorId,
                        ImagePath = competitorOffer.CompetitorImagePath,
                        ComparisonProductVMs = competitorOffer.ProductVMs.Select(p => new QuotationComparisonProductViewModel
                        {
                            Id = p.ProductId,
                            Name = _localizationService.IsRightToLeft() ? p.ProductVM.ProductNameAr : p.ProductVM.ProductName,
                            TotalPrice = p.TotalPrice
                        }).ToList(),
                        ComparisonOfferVM = new QuotationComparisonOfferViewModel
                        {
                            AfterInstallationPercentage = competitorOffer.AfterInstallationPercentage,
                            CertificateNames = _localizationService.IsRightToLeft() ? competitorOffer.CertificateNamesStrAr : competitorOffer.CertificateNamesStr,
                            CurrencyName = _localizationService.IsRightToLeft() ? competitorOffer.CurrencyNameAr : competitorOffer.CurrencyName,
                            DeliveryFromDays = competitorOffer.DeliveryFromDays,
                            DeliveryPlaceName = _localizationService.IsRightToLeft() ? competitorOffer.DeliveryPlaceNameAr : competitorOffer.DeliveryPlaceName,
                            DeliveryToDays = competitorOffer.DeliveryToDays,
                            DownPaymentPercentage = competitorOffer.DownPaymentPercentage,
                            ExchangeRate = competitorOffer.ExchangeRate,
                            GuaranteeName = _localizationService.IsRightToLeft() ? competitorOffer.GuaranteeTermNameAr : competitorOffer.GuaranteeTermName,
                            IsVATIncluded = true,
                            Notes = competitorOffer.Notes,
                            OriginDocumentName = _localizationService.IsRightToLeft() ? competitorOffer.OriginDocumentNameAr : competitorOffer.OriginDocumentName,
                            SupplierName = competitorOffer.Supplier,
                            UponDeliveryPercentage = competitorOffer.UponDeliveryPercentage,
                            ValidityName = _localizationService.IsRightToLeft() ? competitorOffer.ValidityNameAr : competitorOffer.ValidityName
                        }
                    });
                }
            }

            ViewData["ProductVMs"] = productVMs;

            return View(comparisonVM);
        }

        public async Task<IActionResult> ConvertToPurchaseOrder(int id)
        {
            var quotation = await _quotationsBiz.GetInfoByIdAsync(id);
            if (quotation is null)
            {
                return NotFound();
            }

            var selectedVersion = await _quotationOfferVersionsBiz.GetSelectedQuotationVersion(id);

            var acceptedSupplierOffers = await _supplierOffersBiz.GetAcceptedOffers(quotation.OpportunityId);

            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            PurchaseOrder purchaseOrder = new()
            {
                CreatedById = user.Id,
                LastUpdatedById = user.Id,
                CreatedDate = DateTime.UtcNow,
                QuotationId = id,
                Status = PurchaseOrderStatus.New,
                SelectedVersionId = selectedVersion.Id,
                AcceptedOffers = acceptedSupplierOffers.Select(o => new PurchaseOrderSupplierOffer
                {
                    SupplierOfferId = o.Id
                }).ToList()
            };

            await _quotationsBiz.ArchiveAsync(id, user.Id);

            var result = await _purchaseOrdersBiz.AddAsync(purchaseOrder, user.Id);

            if (result)
            {
                var quotationVM = _mapper.Map<QuotationViewModel>(quotation);
                await SaveUserActivity(user.Id, UserActivities.Quotations.ConvertToPurchaseOrder, quotationVM.OpportunityName);

                var project = await _projectsBiz.GetByQuotationId(quotation.Id);
                if (project != null)
                {
                    project.Status = ProjectStatus.PerchasingOrder;
                    await _projectsBiz.UpdateAsync(project);
                }
            }
            return RedirectToAction("Index", "PurchasingOrders");
        }

        public IActionResult ReloadRejectedTSViewComponent()
        {
            return ViewComponent("RejectedTS");
        }

        public IActionResult ReloadRejectedFSViewComponent()
        {
            return ViewComponent("RejectedFS");
        }

        public class DeleteProduct
        {
            public int ProductId { get; set; }
            public bool IsAlternate { get; set; }
            public bool isIncluded { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProductAlternate(List<DeleteProduct> model)
        {
            foreach (var item in model)
            {
                await _quotationsBiz.DeleteProductAlternate(item.ProductId, item.IsAlternate, item.isIncluded);
            }
            return Json(true);
        }

    }
}

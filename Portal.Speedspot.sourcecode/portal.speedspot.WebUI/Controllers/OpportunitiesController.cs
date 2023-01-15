using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using portal.speedspot.BizLayer.BizMethods;
using portal.speedspot.BizLayer.IdentityModule;
using portal.speedspot.Models.Concretes;
using portal.speedspot.Models.Constants;
using portal.speedspot.Models.ViewModels;
using portal.speedspot.WebUI.Controllers.Infrastructure;
using portal.speedspot.WebUI.Helpers;
using portal.speedspot.WebUI.Infrastracture;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static portal.speedspot.Models.Constants.ApplicationEnums;

namespace portal.speedspot.WebUI.Controllers
{
    public class OpportunitiesController : BaseController
    {
        private readonly ILogger _logger;
        private readonly OpportunitiesBiz _opportunitiesBiz;
        private readonly OpportunityTypesBiz _opportunityTypesBiz;
        private readonly CustomersBiz _customersBiz;
        private readonly CountriesBiz _countriesBiz;
        private readonly DepartmentsBiz _departmentsBiz;
        private readonly OpportunityStatusesBiz _opportunityStatusesBiz;
        private readonly ProjectsBiz _projectsBiz;
        private readonly BanksBiz _banksBiz;
        private readonly TechnicalSpecificationAttachmentsBiz _technicalSpecificationAttachmentsBiz;
        private readonly TechnicalSpecificationProductsBiz _technicalSpecificationProductsBiz;
        private readonly AttachmentsBiz _attachmentsBiz;
        private readonly ProductsBiz _productsBiz;
        private readonly SuppliersBiz _suppliersBiz;
        private readonly RequestOffersBiz _requestOffersBiz;
        private readonly CurrenciesBiz _currenciesBiz;
        private readonly DeliveryTypesBiz _deliveryTypesBiz;
        private readonly SupplierOffersBiz _supplierOffersBiz;
        private readonly PaymentTypesBiz _paymentTypesBiz;
        private readonly BidBondsBiz _bidBondsBiz;
        private readonly BookTendersBiz _bookTendersBiz;
        //private readonly ToDoTasksBiz _toDoTasksBiz;
        private readonly EmployeeTypesBiz _employeeTypesBiz;
        private readonly QuotationStatusesBiz _quotationStatusesBiz;
        private readonly QuotationsBiz _quotationsBiz;
        private readonly FavouriteTypesBiz _favouriteTypesBiz;
        private readonly FavouritesBiz _favouritesBiz;
        private readonly UserDepartmentsBiz _userDepartmentsBiz;
        private readonly ProductCategoriesBiz _productCategoriesBiz;
        private readonly BidBondRequestsBiz _bidBondRequestsBiz;
        private readonly BookTenderRequestsBiz _bookTenderRequestsBiz;
        private readonly ProductOriginsBiz _productOriginsBiz;
        private readonly SupplierOfferAttachmentsBiz _supplierOfferAttachmentsBiz;
        private readonly EmailSender _mailer;
        private readonly IHubNotificationHelper _hubNotificationHelper;

        public OpportunitiesController(
            ILogger<OpportunitiesController> logger,
            OpportunitiesBiz opportunitiesBiz,
            IMapper mapper,
            ProjectsBiz projectsBiz,
            OpportunityTypesBiz opportunityTypesBiz,
            CustomersBiz customersBiz,
            CountriesBiz countriesBiz,
            ApplicationUserManager userManager,
            DepartmentsBiz departmentsBiz,
            OpportunityStatusesBiz opportunityStatusesBiz,
            BanksBiz banksBiz,
            TechnicalSpecificationAttachmentsBiz technicalSpecificationAttachmentsBiz,
            TechnicalSpecificationProductsBiz technicalSpecificationProductsBiz,
            AttachmentsBiz attachmentsBiz,
            ProductsBiz productsBiz,
            SuppliersBiz suppliersBiz,
            RequestOffersBiz requestOffersBiz,
            CurrenciesBiz currenciesBiz,
            DeliveryTypesBiz deliveryTypesBiz,
            SupplierOffersBiz supplierOffersBiz,
            PaymentTypesBiz paymentTypesBiz,
            BidBondsBiz bidBondsBiz,
            BookTendersBiz bookTendersBiz,
            EmployeeTypesBiz employeeTypesBiz,
            QuotationStatusesBiz quotationStatusesBiz,
            QuotationsBiz quotationsBiz,
            FavouriteTypesBiz favouriteTypesBiz,
            FavouritesBiz favouritesBiz,
            UserDepartmentsBiz userDepartmentsBiz,
            ProductCategoriesBiz productCategoriesBiz,
            BidBondRequestsBiz bidBondRequestsBiz,
            BookTenderRequestsBiz bookTenderRequestsBiz,
            ProductOriginsBiz productOriginsBiz,
            SupplierOfferAttachmentsBiz supplierOfferAttachmentsBiz,
            IWebHostEnvironment hostEnvironment,
            LocalizationService localizationService,
            EmailSender emailSender,
            IHubNotificationHelper hubNotificationHelper,
            UserActivitiesBiz userActivitiesBiz
            ) : base(mapper, hostEnvironment, userManager, localizationService, userActivitiesBiz)
        {
            _logger = logger;
            _opportunitiesBiz = opportunitiesBiz;
            _opportunityTypesBiz = opportunityTypesBiz;
            _projectsBiz = projectsBiz;
            _customersBiz = customersBiz;
            _countriesBiz = countriesBiz;
            _departmentsBiz = departmentsBiz;
            _opportunityStatusesBiz = opportunityStatusesBiz;
            _banksBiz = banksBiz;
            _technicalSpecificationAttachmentsBiz = technicalSpecificationAttachmentsBiz;
            _technicalSpecificationProductsBiz = technicalSpecificationProductsBiz;
            _attachmentsBiz = attachmentsBiz;
            _productsBiz = productsBiz;
            _hubNotificationHelper = hubNotificationHelper;
            _suppliersBiz = suppliersBiz;
            _requestOffersBiz = requestOffersBiz;
            _currenciesBiz = currenciesBiz;
            _deliveryTypesBiz = deliveryTypesBiz;
            _supplierOffersBiz = supplierOffersBiz;
            _paymentTypesBiz = paymentTypesBiz;
            _bidBondsBiz = bidBondsBiz;
            _bookTendersBiz = bookTendersBiz;
            _employeeTypesBiz = employeeTypesBiz;
            _quotationsBiz = quotationsBiz;
            _quotationStatusesBiz = quotationStatusesBiz;
            _favouriteTypesBiz = favouriteTypesBiz;
            _favouritesBiz = favouritesBiz;
            _userDepartmentsBiz = userDepartmentsBiz;
            _productCategoriesBiz = productCategoriesBiz;
            _bidBondRequestsBiz = bidBondRequestsBiz;
            _bookTenderRequestsBiz = bookTenderRequestsBiz;
            _productOriginsBiz = productOriginsBiz;
            _supplierOfferAttachmentsBiz = supplierOfferAttachmentsBiz;

            _mailer = emailSender;
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

        public async Task<IActionResult> GetSupplierDetails(int supplierId)
        {
            var supplier = _mapper.Map<SupplierViewModel>(await _suppliersBiz.GetByIdAsync(supplierId));
            return PartialView("_SupplierDetailsPartial", supplier);
        }

        public async Task<IActionResult> GetProductDetails(int productId, int index)
        {
            var product = _mapper.Map<TechnicalSpecificationProductViewModel>(await _technicalSpecificationProductsBiz.GetByIdAsync(productId));
            return PartialView("_ProductDetailsPartial", new TechnicalSpecificationProductDetailsViewModel { Index = index, VM = product });
        }

        public async Task<IActionResult> CheckOpportunityType(int typeId)
        {
            var type = await _opportunityTypesBiz.GetByIdAsync(typeId);
            var enumValue = (OpportunityTypeEnum)type.EnumValue;
            if (enumValue == OpportunityTypeEnum.DirectOffer || enumValue == OpportunityTypeEnum.Contract)
            {
                return Json(false);
            }
            return Json(true);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTechnicalSpecsAttachment(int attachmentId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var attachment = await _attachmentsBiz.GetByIdAsync(attachmentId);

            await _attachmentsBiz.DeleteAsync(attachmentId, user.Id);

            await SaveUserActivity(user.Id, UserActivities.Opportunities.DeleteTechSpecsAttachment, attachment.Title);

            return Json(true);
        }

        [HttpGet]
        public async Task<IActionResult> GetTechnicalSpecsAttachments(int id)
        {
            var attachments = _mapper.Map<List<TechnicalSpecificationAttachmentViewModel>>(await _technicalSpecificationAttachmentsBiz.GetByTechnicalSpecificationIdAsync(id));

            return PartialView("_TechnicalSpecificatonAttachmentsPartial", attachments);
        }

        public class TechSpecsProductsViewModel
        {
            public List<TechnicalSpecificationProductViewModel> ProductVMs { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> AddTechnicalSpecsProduct(TechSpecsProductsViewModel addProductVM)
        {
            if (addProductVM == null || addProductVM.ProductVMs == null)
            {
                addProductVM.ProductVMs = new List<TechnicalSpecificationProductViewModel>();
            }
            addProductVM.ProductVMs.Add(new TechnicalSpecificationProductViewModel
            {
                ProductId = null
            });

            foreach (var product in addProductVM.ProductVMs)
            {
                if (product.ProductId != null)
                {
                    var dbProduct = await _productsBiz.GetByIdAsync((int)product.ProductId);
                    product.ProductName = dbProduct.Name;
                    product.ProductNameAr = dbProduct.NameAr;
                }
            }

            ViewData["ProductId"] = _mapper.Map<List<ProductViewModel>>(await _productsBiz.GetAllUnarchivedAsync());
            ViewData["ProductVMs"] = addProductVM.ProductVMs.Where(p => p.ProductId != null);
            ViewData["ProductOriginId"] = (await _productOriginsBiz.GetAllUnarchivedAsync()).ToList();

            return PartialView("_TechnicalSpecificatonProductsPartial", addProductVM.ProductVMs);
        }

        [HttpPost]
        public async Task<IActionResult> UploadTechnicalSpecsProductFile(int? id, IFormFile file)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            string uniqueFileName = UploadFile(file, "Opportunities");

            var attachment = new Attachment
            {
                FilePath = uniqueFileName,
                Title = file.FileName,
                UploadedById = user.Id
            };

            if (id != null)
            {
                attachment.Id = (int)id;
                await _attachmentsBiz.UpdateAsync(attachment, user.Id);
            }
            else
            {
                await _attachmentsBiz.AddAsync(attachment, user.Id);
            }

            await SaveUserActivity(user.Id, UserActivities.Opportunities.UploadTechSpecsAttachment, attachment.Title);

            return Json(new
            {
                FilePath = uniqueFileName,
                AttachmentId = attachment.Id
            });
        }

        [HttpPost]
        public async Task<IActionResult> RefreshTechnicalSpecsProduct(TechSpecsProductsViewModel ProductsVM)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (ProductsVM == null || ProductsVM.ProductVMs == null)
            {
                ProductsVM.ProductVMs = new List<TechnicalSpecificationProductViewModel>();
            }
            foreach (var product in ProductsVM.ProductVMs)
            {
                if (product.ProductId != null)
                {
                    var dbProduct = await _productsBiz.GetByIdAsync((int)product.ProductId);
                    product.ProductName = dbProduct.Name;
                    product.ProductNameAr = dbProduct.NameAr;
                    product.ProductVM = _mapper.Map<ProductViewModel>(dbProduct);
                    product.RequestedOriginVMs = new List<TechnicalSpecificationProductOriginViewModel>();
                    if (product.RequestedOriginIdsStr != null)
                    {
                        foreach (string id in product.RequestedOriginIdsStr.Split(','))
                        {
                            product.RequestedOriginVMs.Add(new TechnicalSpecificationProductOriginViewModel
                            {
                                CountryId = int.Parse(id)
                            });
                        }
                    }
                }
            }

            ViewData["ProductVMs"] = ProductsVM.ProductVMs.Where(p => p.ProductId != null);
            ViewData["CountryId"] = await _countriesBiz.GetAllAsync();
            //ViewData["ProductOriginId"] = (await _productOriginsBiz.GetAllUnarchivedAsync()).ToList();

            return PartialView("_TechnicalSpecificatonProductsPartial", ProductsVM.ProductVMs);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTechnicalSpecsProduct(int technicalSpecsProductId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var product = await _technicalSpecificationProductsBiz.GetByIdAsync(technicalSpecsProductId);
            await _technicalSpecificationProductsBiz.DeleteAsync(technicalSpecsProductId, user.Id);

            await SaveUserActivity(user.Id, UserActivities.Opportunities.DeleteTechSpecsProduct, product.Description);

            return Json(true);
        }

        public class AddRequestOfferProductViewModel
        {
            public int TechnicalSpecificationId { get; set; }
            public List<RequestOfferProductViewModel> ProductVMs { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> AddRequestOfferProduct(AddRequestOfferProductViewModel addProductVM)
        {
            if (addProductVM == null || addProductVM.ProductVMs == null)
            {
                addProductVM.ProductVMs = new List<RequestOfferProductViewModel>();
            }
            addProductVM.ProductVMs.Add(new RequestOfferProductViewModel
            {
                ProductId = null,
                ProductVM = new TechnicalSpecificationProductViewModel()
            });

            ViewData["ProductVMs"] = _mapper.Map<List<TechnicalSpecificationProductViewModel>>(await _technicalSpecificationProductsBiz.GetByTechnicalSpecificationId(addProductVM.TechnicalSpecificationId)).ToList();
            return PartialView("_RequestOfferProductsPartial", addProductVM.ProductVMs);
        }

        [HttpGet]
        public async Task<IActionResult> ClearRequestOfferProducts(int technicalSpecificationId)
        {
            List<RequestOfferProductViewModel> productVMs = new();

            ViewData["ProductVMs"] = _mapper.Map<List<TechnicalSpecificationProductViewModel>>(await _technicalSpecificationProductsBiz.GetByTechnicalSpecificationId(technicalSpecificationId)).ToList();
            return PartialView("_RequestOfferProductsPartial", productVMs);
        }

        [HttpPost]
        public async Task<IActionResult> UploadOfferFile(IFormFile file)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            string uniqueFileName = UploadFile(file, "Opportunities");

            var attachment = new Attachment
            {
                FilePath = uniqueFileName,
                Title = file.FileName,
                UploadedById = user.Id
            };
            await _attachmentsBiz.AddAsync(attachment, user.Id);

            await SaveUserActivity(user.Id, UserActivities.Opportunities.UploadOfferFile, file.FileName);

            return Json(new
            {
                FilePath = uniqueFileName,
                AttachmentId = attachment.Id
            });
        }

        public class RequestOffersViewModel
        {
            public List<RequestOfferViewModel> RequestOfferVMs { get; set; }
            public int TechnicalSpecificationId { get; set; }
            public int OpportunityId { get; set; }
            //public List<int> ProductIds { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> AddRequestOffers(RequestOffersViewModel requestOffers)
        {
            if (requestOffers != null && requestOffers.RequestOfferVMs != null)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var techSpecsProducts = await _technicalSpecificationProductsBiz.GetByTechnicalSpecificationId(requestOffers.TechnicalSpecificationId);
                List<int> techSpecsProductIds = techSpecsProducts.OrderBy(o => o.Id).Select(x => x.ProductId).ToList();
                foreach (var requestOffer in requestOffers.RequestOfferVMs)
                {
                    requestOffer.OpportunityId = requestOffers.OpportunityId;
                    SupplierProductsViewModel supplierProducts = _mapper.Map<SupplierProductsViewModel>(await _suppliersBiz.GetByIdWithProductsAsync(requestOffer.SupplierId, techSpecsProductIds));
                    requestOffer.ProductVMs = new List<RequestOfferProductViewModel>();
                    foreach (var supplierProduct in supplierProducts.ProductVMs)
                    {
                        var techSpecsProduct = _mapper.Map<TechnicalSpecificationProductViewModel>(techSpecsProducts.FirstOrDefault(p => p.ProductId == supplierProduct.ProductId));
                        requestOffer.ProductVMs.Add(new RequestOfferProductViewModel
                        {
                            ProductId = techSpecsProduct.Id
                        });
                    }
                    requestOffer.ProductVMs = requestOffer.ProductVMs.OrderBy(o => o.ProductId).ToList();
                    await _requestOffersBiz.AddAsync(_mapper.Map<RequestOffer>(requestOffer), user.Id);

                    var supplier = _mapper.Map<SupplierViewModel>(await _suppliersBiz.GetByIdAsync(requestOffer.SupplierId));
                    await SaveUserActivity(user.Id, UserActivities.Opportunities.AddRequestOffer, supplier.Name);
                }
            }

            var dbRequestOffers = _mapper.Map<List<RequestOfferViewModel>>(await _requestOffersBiz.GetByOpportunityId(requestOffers.OpportunityId));

            return PartialView("_RequestOffersPartial", dbRequestOffers);
        }

        public async Task<IActionResult> GetTechSpecsProductDetails(int id)
        {
            var tsProduct = _mapper.Map<TechnicalSpecificationProductViewModel>(await _technicalSpecificationProductsBiz.GetByIdAsync(id));

            return PartialView("_TechnicalSpecificationProductDetails", tsProduct);
        }

        [HttpPost]
        public async Task<IActionResult> AddDbRequestOffer(RequestOfferViewModel model)
        {
            if (ModelState.IsValid)
            {
                //////////// Email Suppliers /////////////////////////
                //if (model.IsSendEmail && !model.IsEmailSent)
                //{
                //    var supplier = await _suppliersBiz.GetByIdAsync(model.SupplierId);
                //    try
                //    {
                //        var mailResult = _mailer.Send("New inquiry", "We request an offer for attached product specifications", supplier.Email, supplier.Name,
                //            _webHostEnvironment.WebRootPath + "/Images/Opportunities/" + model.RequestOfferAttachmentPath);
                //        if (mailResult)
                //        {
                //            model.IsEmailSent = true;
                //        }
                //    }
                //    catch { }
                //}
                //////////////////////////////////////////////////////
                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                var requestOffer = _mapper.Map<RequestOffer>(model);
                var result = await _requestOffersBiz.AddAsync(requestOffer, user.Id);

                var supplier = _mapper.Map<SupplierViewModel>(await _suppliersBiz.GetByIdAsync(requestOffer.SupplierId));
                await SaveUserActivity(user.Id, UserActivities.Opportunities.AddRequestOffer, supplier.Name);

                return Json(result);
            }

            return Json(false);
        }

        public async Task<IActionResult> GetRequestOffers(int id)
        {
            var requestOffers = _mapper.Map<List<RequestOfferViewModel>>(await _requestOffersBiz.GetByOpportunityId(id));

            return PartialView("_DetailsRequestOffersPartial", requestOffers);
        }

        public async Task<IActionResult> RequestOfferEmailSent(int id, int offerId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var offer = _mapper.Map<RequestOfferViewModel>(await _requestOffersBiz.OfferEmailSent(offerId, user.Id));
            await SaveUserActivity(user.Id, UserActivities.Opportunities.RequestOfferEmailSent, offer.SupplierName);

            var dbRequestOffers = _mapper.Map<List<RequestOfferViewModel>>(await _requestOffersBiz.GetByOpportunityId(id));

            return PartialView("_RequestOffersPartial", dbRequestOffers);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRequestOffer(int offerId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            await _requestOffersBiz.DeleteAsync(offerId, user.Id);

            var offer = _mapper.Map<RequestOfferViewModel>(await _requestOffersBiz.GetByIdAsync(offerId));
            await SaveUserActivity(user.Id, UserActivities.Opportunities.DeleteRequestOffer, offer.SupplierName);

            return Json(true);
        }

        public async Task<IActionResult> GetCurrencyDetails(int currencyId, decimal price, decimal rate)
        {
            var currency = await _currenciesBiz.GetByIdAsync(currencyId);
            var calculatedCurrency = new CalculatedCurrencyViewModel
            {
                ExchangeRate = rate != 0 ? rate : currency != null ? currency.ExchangeRate : 0,
                Price = price
            };
            return PartialView("_CurrencyDetailsPartial", calculatedCurrency);
        }

        public async Task<IActionResult> GetCurrencyData(int currencyId)
        {
            Currency currency = await _currenciesBiz.GetByIdAsync(currencyId);

            return Json(currency, new JsonSerializerOptions
            {
                PropertyNamingPolicy = null
            });
        }

        public async Task<IActionResult> GetSupplierProducts(int id, int supplierId, string currencySymbol)
        {
            List<SupplierOfferProductViewModel> productVMs = new List<SupplierOfferProductViewModel>();
            var requestOfferVM = _mapper.Map<RequestOfferViewModel>(await _requestOffersBiz.GetRequestOfferAsync(id, supplierId));
            foreach (var productVM in requestOfferVM.ProductVMs)
            {
                productVMs.Add(new SupplierOfferProductViewModel
                {
                    TechnicalSpecificationProductId = (int)productVM.ProductId,
                    ProductVM = productVM.ProductVM
                });
            }

            ViewData["CurrencySymbol"] = currencySymbol;
            return PartialView("_SupplierOffers_SupplierProductsPartial", productVMs.OrderBy(o => o.ProductVM.Index).ToList());
        }

        [HttpPost]
        public async Task<IActionResult> AddSupplierOffer(SupplierOfferViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                if (model.Files != null && model.Files.Count > 0)
                {
                    model.AttachmentVMs = new List<SupplierOfferAttachmentViewModel>();
                    foreach (IFormFile file in model.Files)
                    {
                        string uploadedFileName = UploadFile(file, "Opportunities");
                        Attachment attachment = new()
                        {
                            FilePath = uploadedFileName,
                            Title = file.FileName,
                            UploadedById = user.Id
                        };
                        await _attachmentsBiz.AddAsync(attachment, user.Id);
                        model.AttachmentVMs.Add(new SupplierOfferAttachmentViewModel
                        {
                            AttachmentId = attachment.Id
                        });
                    }
                }

                foreach (SupplierOfferProductViewModel product in model.ProductVMs)
                {
                    if (product.File != null)
                    {
                        string uploadedFileName = UploadFile(product.File, "Opportunities");
                        Attachment attachment = new()
                        {
                            FilePath = uploadedFileName,
                            Title = product.File.FileName,
                            UploadedById = user.Id
                        };
                        await _attachmentsBiz.AddAsync(attachment, user.Id);
                        product.AttachmentId = attachment.Id;
                    }
                }

                SupplierOffer supplierOffer = _mapper.Map<SupplierOffer>(model);

                var supplier = _mapper.Map<SupplierViewModel>(await _suppliersBiz.GetByIdAsync(model.SupplierId));
                await SaveUserActivity(user.Id, UserActivities.Opportunities.AddSupplierOffer, supplier.Name);

                await _supplierOffersBiz.AddAsync(supplierOffer, user.Id);
            }

            var supplierOffers = _mapper.Map<List<SupplierOfferViewModel>>(await _supplierOffersBiz.GetByOpportunityId(model.OpportunityId));

            return PartialView("_SupplierOffersPartial", supplierOffers.OrderBy(o => o.LocalTotalPriceWCIF).ToList());
        }

        [HttpGet]
        public async Task<IActionResult> EditSupplierOffer(int supplierOfferId)
        {
            var supplierOffer = _mapper.Map<SupplierOfferViewModel>(await _supplierOffersBiz.GetByIdAsync(supplierOfferId));
            var RequestOfferVMs = await _requestOffersBiz.GetByOpportunityId(supplierOffer.OpportunityId);
            var supplierOfferVMs = _mapper.Map<List<SupplierOfferViewModel>>(await _supplierOffersBiz.GetByOpportunityId(supplierOffer.OpportunityId));

            ViewData["SupplierOffer_SupplierId"] = _mapper.Map<List<SupplierViewModel>>(RequestOfferVMs.Where(o => o.IsEmailSent).Select(x => x.Supplier).ToList());
            ViewData["SupplierOffers"] = supplierOfferVMs;
            ViewData["CurrencyId"] = (await _currenciesBiz.GetAllUnarchivedAsync()).ToList();
            ViewData["DeliveryTypeId"] = (await _deliveryTypesBiz.GetAllUnarchivedAsync()).ToList();
            ViewData["PaymentTypeId"] = (await _paymentTypesBiz.GetAllUnarchivedAsync()).ToList();

            return PartialView("_SupplierOffers_SupplierOfferEditFormPartial", supplierOffer);
        }

        public async Task<IActionResult> DeleteOfferAttachment(int offerAttachmentId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var offerAttachment = await _supplierOfferAttachmentsBiz.GetByIdAsync(offerAttachmentId);
            await _supplierOfferAttachmentsBiz.DeleteAsync(offerAttachmentId, user.Id);
            bool result = await _attachmentsBiz.DeleteAsync(offerAttachment.AttachmentId, user.Id);

            await SaveUserActivity(user.Id, UserActivities.Opportunities.DeleteOfferAttachment, offerAttachment.Attachment.Title);

            return Json(result);
        }

        public async Task<IActionResult> UpdateSupplierOffer(SupplierOfferViewModel model)
        {
            ModelState.Remove("Files");
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                if (model.Files != null && model.Files.Count > 0)
                {
                    model.AttachmentVMs = new List<SupplierOfferAttachmentViewModel>();
                    foreach (IFormFile file in model.Files)
                    {
                        string uploadedFileName = UploadFile(file, "Opportunities");
                        Attachment attachment = new Attachment
                        {
                            FilePath = uploadedFileName,
                            Title = file.FileName,
                            UploadedById = user.Id
                        };
                        await _attachmentsBiz.AddAsync(attachment, user.Id);
                        model.AttachmentVMs.Add(new SupplierOfferAttachmentViewModel
                        {
                            AttachmentId = attachment.Id
                        });
                    }
                }

                foreach (SupplierOfferProductViewModel product in model.ProductVMs)
                {
                    if (product.File != null)
                    {
                        string uploadedFileName = UploadFile(product.File, "Opportunities");
                        Attachment attachment = new Attachment
                        {
                            FilePath = uploadedFileName,
                            Title = product.File.FileName,
                            UploadedById = user.Id
                        };
                        if (product.AttachmentId != null)
                        {
                            attachment.Id = (int)product.AttachmentId;
                            await _attachmentsBiz.UpdateAsync(attachment, user.Id);
                        }
                        else
                        {
                            await _attachmentsBiz.AddAsync(attachment, user.Id);
                        }
                    }
                }

                SupplierOffer supplierOffer = _mapper.Map<SupplierOffer>(model);

                await _supplierOffersBiz.UpdateAsync(supplierOffer, user.Id);

                var supplier = _mapper.Map<SupplierViewModel>(await _suppliersBiz.GetByIdAsync(model.SupplierId));
                await SaveUserActivity(user.Id, UserActivities.Opportunities.UpdateSupplierOffer, supplier.Name);
            }

            var supplierOffers = _mapper.Map<List<SupplierOfferViewModel>>(await _supplierOffersBiz.GetByOpportunityId(model.OpportunityId));

            return PartialView("_SupplierOffersPartial", supplierOffers.OrderBy(o => o.LocalTotalPriceWCIF).ToList());
        }

        public async Task<IActionResult> ClearSupplierOfferForm(int id)
        {
            var supplierOfferVMs = _mapper.Map<List<SupplierOfferViewModel>>(await _supplierOffersBiz.GetByOpportunityId(id));
            var RequestOfferVMs = await _requestOffersBiz.GetByOpportunityId(id);

            ViewData["SupplierOffer_SupplierId"] = _mapper.Map<List<SupplierViewModel>>(RequestOfferVMs.Where(o => o.IsEmailSent).Select(x => x.Supplier).ToList());
            ViewData["SupplierOffers"] = supplierOfferVMs;
            ViewData["CurrencyId"] = (await _currenciesBiz.GetAllUnarchivedAsync()).ToList();
            ViewData["DeliveryTypeId"] = (await _deliveryTypesBiz.GetAllUnarchivedAsync()).ToList();
            ViewData["PaymentTypeId"] = (await _paymentTypesBiz.GetAllUnarchivedAsync()).ToList();

            return PartialView("_SupplierOffers_SupplierOfferFormPartial");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSupplierOffer(int id, int supplierOfferId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var supplierOffer = _mapper.Map<SupplierOfferViewModel>(await _supplierOffersBiz.GetByIdAsync(supplierOfferId));

            await _supplierOffersBiz.DeleteAsync(supplierOfferId, user.Id);

            var supplierOffers = _mapper.Map<List<SupplierOfferViewModel>>(await _supplierOffersBiz.GetByOpportunityId(id));

            await SaveUserActivity(user.Id, UserActivities.Opportunities.DeleteSupplierOffer, supplierOffer.SupplierName);

            return PartialView("_SupplierOffersPartial", supplierOffers.OrderBy(o => o.LocalTotalPriceWCIF).ToList());
        }

        public async Task<IActionResult> GetSupplierOffers(int id)
        {
            var supplierOffers = _mapper.Map<List<SupplierOfferViewModel>>(await _supplierOffersBiz.GetByOpportunityId(id));

            return PartialView("_DetailsSupplierOffers", supplierOffers);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAttachment(int attachmentId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            await _attachmentsBiz.DeleteAsync(attachmentId, user.Id);

            var attachment = await _attachmentsBiz.GetByIdAsync(attachmentId);
            await SaveUserActivity(user.Id, UserActivities.Opportunities.DeleteAttachment, attachment.Title);

            return Json(true);
        }

        public async Task<IActionResult> GetOpportunityFiles(int id)
        {
            var opportunityVM = _mapper.Map<OpportunityViewModel>(await _opportunitiesBiz.GetByIdAsync(id));

            var attachments = new List<OpportunityAttachmentViewModel>();
            if (opportunityVM.BidBondVM.BidBondAttachmentId != null)
            {
                attachments.Add(new OpportunityAttachmentViewModel
                {
                    AttachmentId = (int)opportunityVM.BidBondVM.BidBondAttachmentId,
                    AttachmentPath = opportunityVM.BidBondVM.BidBondAttachmentPath,
                    AttachmentTitle = opportunityVM.BidBondVM.BidBondAttachmentTitle,
                    IsDeletable = false
                });
            };
            if (opportunityVM.BookTenderVM.ReceiptAttachmentId != null)
            {
                attachments.Add(new OpportunityAttachmentViewModel
                {
                    AttachmentId = (int)opportunityVM.BookTenderVM.ReceiptAttachmentId,
                    AttachmentPath = opportunityVM.BookTenderVM.ReceiptAttachmentPath,
                    AttachmentTitle = opportunityVM.BookTenderVM.ReceiptAttachmentTitle,
                    IsDeletable = false
                });
            }
            if (opportunityVM.TechnicalSpecificationVM.AttachmentFiles != null)
            {
                foreach (var attachment in opportunityVM.TechnicalSpecificationVM.AttachmentFiles)
                {
                    if (attachment.AttachmentId != null)
                        attachments.Add(new OpportunityAttachmentViewModel
                        {
                            AttachmentId = (int)attachment.AttachmentId,
                            AttachmentPath = attachment.AttachmentPath,
                            AttachmentTitle = attachment.Title,
                            IsDeletable = true
                        });
                }
            }
            //foreach (var requestOffer in opportunityVM.RequestOfferVMs)
            //{
            //    attachments.Add(new OpportunityAttachmentViewModel
            //    {
            //        AttachmentId = requestOffer.RequestOfferAttachmentId,
            //        AttachmentPath = requestOffer.RequestOfferAttachmentPath,
            //        AttachmentTitle = requestOffer.RequestOfferAttachmentTitle,
            //        IsDeletable = false
            //    });
            //}
            //foreach (var supplierOffer in opportunityVM.SupplierOfferVMs)
            //{
            //    attachments.Add(new OpportunityAttachmentViewModel
            //    {
            //        AttachmentId = supplierOffer.OfferAttachmentId,
            //        AttachmentPath = supplierOffer.OfferAttachmentPath,
            //        AttachmentTitle = supplierOffer.OfferAttachmentTitle,
            //        IsDeletable = false
            //    });
            //}

            return PartialView("_DetailsFiles", attachments);
        }

        public async Task<IActionResult> GetTechSpecsAttachments(int id)
        {
            var opportunity = await _opportunitiesBiz.GetByIdAsync(id);
            var attachments = _mapper.Map<List<TechnicalSpecificationAttachmentViewModel>>(await _technicalSpecificationAttachmentsBiz.GetByTechnicalSpecificationIdAsync(opportunity.TechnicalSpecification.Id));

            return PartialView("_DetailsTechnicalSpecificationAttachmentsPartial", attachments);
        }

        public async Task<IActionResult> GetBidBondDetails(int id)
        {
            return PartialView("_DetailsBidBondPartial", await _bidBondsBiz.GetByOpportunityId(id));
        }

        public async Task<IActionResult> GetBookTenderDetails(int id)
        {
            return PartialView("_DetailsBookTenderPartial", await _bookTendersBiz.GetByOpportunityId(id));
        }

        public async Task<IActionResult> RefreshChangeableActions(int id)
        {
            var model = _mapper.Map<OpportunityViewModel>(await _opportunitiesBiz.GetByIdAsync(id));
            return PartialView("_DetailsChangeableActionsPartial", model);
        }

        public async Task<IActionResult> RefreshRequestOfferAvailableSuppliers(int id)
        {
            //var requestOffers = await _requestOffersBiz.GetByOpportunityId(id);
            var supplierOfferVMs = _mapper.Map<List<SupplierOfferViewModel>>(await _supplierOffersBiz.GetByOpportunityId(id));
            var supplierVMs = _mapper.Map<List<SupplierViewModel>>((await _requestOffersBiz.GetByOpportunityId(id)).Where(o => o.IsEmailSent).Select(x => x.Supplier).ToList());

            ViewData["SupplierOffer_SupplierId"] = supplierVMs;
            ViewData["SupplierOffers"] = supplierOfferVMs;
            return PartialView("_SupplierOffers_AvailableSuppliersPartial");
        }

        public async Task<IActionResult> RefreshEditRequestOfferAvailableSuppliers(int id)
        {
            //var requestOffers = await _requestOffersBiz.GetByOpportunityId(id);
            var supplierOfferVMs = _mapper.Map<List<SupplierOfferViewModel>>(await _supplierOffersBiz.GetByOpportunityId(id));
            var supplierVMs = _mapper.Map<List<SupplierViewModel>>((await _requestOffersBiz.GetByOpportunityId(id)).Where(o => o.IsEmailSent).Select(x => x.Supplier).ToList());

            ViewData["SupplierOffer_SupplierId"] = supplierVMs;
            ViewData["SupplierOffers"] = supplierOfferVMs;
            return PartialView("_SupplierOffers_AvailableSuppliersEditPartial");
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

        public async Task<IActionResult> ToggleFavourite(int? itemId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var favourityType = await _favouriteTypesBiz.GetByEnumValue((int)FavouriteTypeEnum.Opportunities);
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

        public async Task<IActionResult> GetCustomerDetails(int customerId)
        {
            CustomerViewModel customerVM = _mapper.Map<CustomerViewModel>(await _customersBiz.GetByIdAsync(customerId));
            int? DepartmentId = null;
            if (customerVM.DepartmentVMs != null && customerVM.DepartmentVMs.Count > 0)
            {
                DepartmentId = customerVM.DepartmentVMs.FirstOrDefault().DepartmentId;
            }
            return Json(new
            {
                customerVM.AddressVM?.CountryId,
                customerVM.AddressVM?.BillingAddress,
                DepartmentId
            },
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = null
            });
        }

        public class ProcessDataViewModel
        {
            public bool IsValid { get; set; }
            public Opportunity Opportunity { get; set; }
            public OpportunityViewModel ViewModel { get; set; }
        }

        public async Task<ProcessDataViewModel> ProcessData(OpportunityViewModel model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            bool isValid = true;

            //Process request Offers
            var deletedRequestOffers = new List<RequestOfferViewModel>();
            var requestOfferVMs = model.RequestOfferVMs;
            for (int i = 0; i < requestOfferVMs.Count; i++)
            {
                var offer = requestOfferVMs[i];
                if (offer.SupplierId == 0 || offer.ProductVMs == null || offer.ProductVMs.Count == 0)
                {
                    deletedRequestOffers.Add(offer);
                }
            }
            foreach (var offer in deletedRequestOffers)
            {
                model.RequestOfferVMs.Remove(offer);
            }

            //Process Supplier Offers
            var deletedSupplierOffers = new List<SupplierOfferViewModel>();
            var supplierOfferVMs = model.SupplierOfferVMs;
            for (int i = 0; i < supplierOfferVMs.Count; i++)
            {
                var offer = supplierOfferVMs[i];
                if (offer.SupplierId == 0 || offer.ProductVMs == null || offer.ProductVMs.Count == 0)
                {
                    deletedSupplierOffers.Add(offer);
                }
            }
            foreach (var offer in deletedSupplierOffers)
            {
                model.SupplierOfferVMs.Remove(offer);
            }

            if (!model.IsBidBond)
            {
                model.BidBondVM = null;
            }
            if (!model.IsBookTender)
            {
                model.BookTenderVM = null;
            }

            var opportunity = _mapper.Map<Opportunity>(model);

            //Process Status
            opportunity.StatusId = (await _opportunityStatusesBiz.GetByEnumValue((int)OpportunityStatusEnum.SalesInformation)).Id;

            //Process Type
            OpportunityTypeEnum typeEnum = (OpportunityTypeEnum)(await _opportunityTypesBiz.GetByIdAsync(opportunity.TypeId)).EnumValue;
            switch (typeEnum)
            {
                case OpportunityTypeEnum.DirectOffer:
                case OpportunityTypeEnum.Contract:
                    opportunity.BidBond = null;
                    opportunity.BookTender = null;
                    opportunity.IsLimitedTenderType = null;
                    opportunity.TenderNumber = null;
                    break;
            }

            //Process Bidbond
            if (opportunity.BidBond != null)
            {
                if (model.BidBondVM.BidBondFile != null)
                {
                    string uniqueFileName = UploadFile(model.BidBondVM.BidBondFile, "Opportunities");
                    if (opportunity.BidBond.BidBondAttachmentId != null)
                    {
                        opportunity.BidBond.BidBondAttachment = new Attachment
                        {
                            FilePath = uniqueFileName,
                            Title = model.BidBondVM.BidBondFile.FileName,
                            Id = (int)opportunity.BidBond.BidBondAttachmentId,
                            UploadedById = user.Id
                        };
                    }
                    else
                    {
                        opportunity.BidBond.BidBondAttachment = new Attachment
                        {
                            FilePath = uniqueFileName,
                            Title = model.BidBondVM.BidBondFile.FileName,
                            UploadedById = user.Id
                        };
                    }
                }
                if (opportunity.BidBond.CashType)
                {
                    opportunity.BidBond.BidBondOff_ReceiptNo = null;
                }
                else
                {
                    opportunity.BidBond.BidBondOn_BankId = null;
                    opportunity.BidBond.BidBondOn_IsCredit = null;
                    opportunity.BidBond.BidBondOn_BankBranch = null;
                    opportunity.BidBond.BidBondOn_BidBondNo = null;
                    opportunity.BidBond.BidBondOn_ExpiryDate = null;
                    opportunity.BidBond.BidBondOn_Fees = null;
                    opportunity.BidBond.BidBondOn_HoldAmount = null;
                    opportunity.BidBond.BidBondOn_Percentage = null;
                }

                if (model.BidBondVM.IsRequested)
                {
                    BidBondRequest request = new();

                    if (model.BidBondVM.RequestVM == null)
                    {
                        request.RequestedById = user.Id;
                        request.RequestedDate = DateTime.UtcNow;
                        request.Status = RequestStatusEnum.Pending;
                    }
                    else
                    {
                        request = _mapper.Map<BidBondRequest>(model.BidBondVM.RequestVM);
                        //request.Id = (int)model.BidBondVM.RequestVM.Id;

                        if (model.BidBondVM.IsSent && !model.BidBondVM.RequestVM.IsSent)
                        {
                            request.IsSent = true;
                            request.SentById = user.Id;
                            request.SentDate = DateTime.UtcNow;
                        }

                        if (model.BidBondVM.IsApproved && model.BidBondVM.RequestVM.Status != RequestStatusEnum.Approved)
                        {
                            request.Status = RequestStatusEnum.Approved;
                            request.StatusById = user.Id;
                            request.StatusDate = DateTime.UtcNow;
                        }
                        else if (model.BidBondVM.IsRejected && model.BidBondVM.RequestVM.Status != RequestStatusEnum.Rejected)
                        {
                            request.Status = RequestStatusEnum.Rejected;
                            request.StatusById = user.Id;
                            request.StatusDate = DateTime.UtcNow;
                        }

                        if (model.BidBondVM.IsPrinted && !model.BidBondVM.RequestVM.IsPrinted)
                        {
                            request.IsPrinted = true;
                            request.PrintedById = user.Id;
                            request.PrintedDate = DateTime.UtcNow;
                        }
                    }

                    request.DepartmentId = opportunity.DepartmentId;
                    opportunity.BidBond.Request = request;
                }
            }

            //Process Book Tender
            if (opportunity.BookTender != null)
            {
                if (model.BookTenderVM.ReceiptAttachmentFile != null)
                {
                    string uniqueFileName = UploadFile(model.BookTenderVM.ReceiptAttachmentFile, "Opportunities");
                    if (opportunity.BookTender.ReceiptAttachmentId != null)
                    {
                        opportunity.BookTender.ReceiptAttachment = new Attachment
                        {
                            FilePath = uniqueFileName,
                            Title = model.BookTenderVM.ReceiptAttachmentFile.FileName,
                            Id = (int)opportunity.BookTender.ReceiptAttachmentId,
                            UploadedById = user.Id
                        };
                    }
                    else
                    {
                        opportunity.BookTender.ReceiptAttachment = new Attachment
                        {
                            FilePath = uniqueFileName,
                            Title = model.BookTenderVM.ReceiptAttachmentFile.FileName,
                            UploadedById = user.Id
                        };
                    }
                }

                if (model.BookTenderVM.IsRequested)
                {
                    BookTenderRequest request = new();

                    if (model.BookTenderVM.RequestVM == null)
                    {
                        request.RequestedById = user.Id;
                        request.RequestedDate = DateTime.UtcNow;
                        request.Status = RequestStatusEnum.Pending;
                    }
                    else
                    {
                        request = _mapper.Map<BookTenderRequest>(model.BookTenderVM.RequestVM);

                        if (model.BookTenderVM.IsSent && !model.BookTenderVM.RequestVM.IsSent)
                        {
                            request.IsSent = true;
                            request.SentById = user.Id;
                            request.SentDate = DateTime.UtcNow;
                        }

                        if (model.BookTenderVM.IsApproved && model.BookTenderVM.RequestVM.Status != RequestStatusEnum.Approved)
                        {
                            request.Status = RequestStatusEnum.Approved;
                            request.StatusById = user.Id;
                            request.StatusDate = DateTime.UtcNow;
                        }
                        else if (model.BookTenderVM.IsRejected && model.BookTenderVM.RequestVM.Status != RequestStatusEnum.Rejected)
                        {
                            request.Status = RequestStatusEnum.Rejected;
                            request.StatusById = user.Id;
                            request.StatusDate = DateTime.UtcNow;
                        }

                        if (model.BookTenderVM.IsPrinted && !model.BookTenderVM.RequestVM.IsPrinted)
                        {
                            request.IsPrinted = true;
                            request.PrintedById = user.Id;
                            request.PrintedDate = DateTime.UtcNow;
                        }
                    }

                    request.DepartmentId = opportunity.DepartmentId;
                    opportunity.BookTender.Request = request;
                }
            }

            //Process Technical Specifications
            if (opportunity.TechnicalSpecification != null)
            {
                if (model.TechnicalSpecificationVM.Files != null && model.TechnicalSpecificationVM.Files.Count > 0)
                {
                    if (opportunity.TechnicalSpecification.Attachments == null)
                        opportunity.TechnicalSpecification.Attachments = new List<TechnicalSpecificationAttachment>();

                    if (model.TechnicalSpecificationVM.AttachmentFiles == null)
                        model.TechnicalSpecificationVM.AttachmentFiles = new List<TechnicalSpecificationAttachmentViewModel>();

                    foreach (var file in model.TechnicalSpecificationVM.Files)
                    {
                        string uniqueFileName = UploadFile(file, "Opportunities");

                        var attachment = new Attachment
                        {
                            FilePath = uniqueFileName,
                            Title = file.FileName,
                            UploadedById = user.Id
                        };
                        await _attachmentsBiz.AddAsync(attachment);

                        opportunity.TechnicalSpecification.Attachments.Add(new TechnicalSpecificationAttachment
                        {
                            AttachmentId = attachment.Id,
                            Attachment = attachment
                        });

                        model.TechnicalSpecificationVM.AttachmentFiles.Add(new TechnicalSpecificationAttachmentViewModel
                        {
                            AttachmentPath = uniqueFileName,
                            Title = file.FileName,
                            TechnicalSpecificationId = opportunity.TechnicalSpecification.Id,
                            AttachmentId = attachment.Id
                        });
                    }
                }

                if (model.TechnicalSpecificationVM.ProductVMs != null && model.TechnicalSpecificationVM.ProductVMs.Count > 0)
                {
                    int i = 0;
                    foreach (var productVM in model.TechnicalSpecificationVM.ProductVMs)
                    {
                        if (productVM.ProductId != null)
                        {
                            if (productVM.RequestedOriginIdsStr == null || productVM.RequestedOriginIdsStr == "")
                            {
                                isValid = false;
                                ModelState.AddModelError("TechnicalSpecificationVM.ProductVMs[" + i + "].RequestedOriginIds", string.Format(_localizationService["RequiredField"].Value, _localizationService["OriginCountry"].Value));
                            }

                            if (productVM.Quantity == null)
                            {
                                isValid = false;
                                ModelState.AddModelError("TechnicalSpecificationVM.ProductVMs[" + i + "].Quantity", string.Format(_localizationService["RequiredField"].Value, _localizationService["Quantity"].Value));
                            }

                            if (productVM.Description == null || productVM.Description == "")
                            {
                                isValid = false;
                                ModelState.AddModelError("TechnicalSpecificationVM.ProductVMs[" + i + "].Description", string.Format(_localizationService["RequiredField"].Value, _localizationService["Description"].Value));
                            }

                            var product = await _productsBiz.GetByIdAsync((int)productVM.ProductId);
                            productVM.ProductName = product.Name;
                            productVM.ProductNameAr = product.NameAr;
                            productVM.ProductVM = _mapper.Map<ProductViewModel>(product);

                            if (productVM.AttachmentFile != null)
                            {
                                string uniqueFileName = UploadFile(productVM.AttachmentFile, "Opportunities");

                                var attachment = new Attachment
                                {
                                    FilePath = uniqueFileName,
                                    Title = productVM.AttachmentFile.FileName,
                                    UploadedById = user.Id
                                };

                                if (productVM.AttachmentId != null)
                                {
                                    attachment.Id = (int)productVM.AttachmentId;
                                    await _attachmentsBiz.UpdateAsync(attachment);
                                }
                                else
                                {
                                    await _attachmentsBiz.AddAsync(attachment);
                                    productVM.AttachmentId = attachment.Id;
                                    productVM.AttachmentPath = attachment.FilePath;
                                    productVM.AttachmentTitle = attachment.Title;
                                }

                                opportunity.TechnicalSpecification.Products[i].AttachmentId = attachment.Id;
                            }

                            //if (productVM.AttachmentId == null && (productVM.AttachmentPath == null || productVM.AttachmentPath == ""))
                            //{
                            //    isValid = false;
                            //    ModelState.AddModelError("TechnicalSpecificationVM.ProductVMs[" + i + "].AttachmentId", string.Format(_localizationService["RequiredField"].Value, _localizationService["Attachment"].Value));
                            //}
                        }
                        else
                        {
                            isValid = false;
                            ModelState.AddModelError("TechnicalSpecificationVM.ProductVMs[" + i + "].ProductId", string.Format(_localizationService["RequiredField"].Value, _localizationService["ProductId"].Value));
                        }

                        i++;
                    }
                }

                if (opportunity.TechnicalSpecification.Products.Count > 0)
                {
                    opportunity.StatusId = (await _opportunityStatusesBiz.GetByEnumValue((int)OpportunityStatusEnum.TechnicalSpecifications)).Id;
                }

                if (opportunity.SupplierOffers.Count > 0)
                {
                    opportunity.StatusId = (await _opportunityStatusesBiz.GetByEnumValue((int)OpportunityStatusEnum.SupplierOffers)).Id;
                }
            }

            return new ProcessDataViewModel
            {
                IsValid = isValid,
                Opportunity = opportunity,
                ViewModel = model
            };
        }

        [Authorize(Permissions.Opportunities.View)]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userDepartments = await _userDepartmentsBiz.GetByUserAsync(user.Id);

            var favourityType = await _favouriteTypesBiz.GetByEnumValue((int)FavouriteTypeEnum.Opportunities);
            var isFavourite = await _favouritesBiz.IsFavourite(favourityType.Id, user.Id, null);
            ViewData["IsFavourite"] = isFavourite;

            ViewData["OpportunityTypes"] = await _opportunityTypesBiz.GetAllAsync();
            ViewData["OpportunityStatuses"] = await _opportunityStatusesBiz.GetAllAsync();

            if (await _userManager.IsSuperAdmin(user))
                ViewData["Departments"] = await _departmentsBiz.GetAllUnarchivedAsync();
            else
                ViewData["Departments"] = await _departmentsBiz.GetAllUnarchivedByUserAsync(user.DepartmentId, userDepartments.Select(x => x.DepartmentId).ToList());

            return View();
        }
        [Authorize(Permissions.Opportunities.View)]
        public async Task<ActionResult> GetOpportunities()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userDepartments = await _userDepartmentsBiz.GetByUserAsync(user.Id);

            List<OpportunityViewModel> opportunityVMs;

            if (await _userManager.IsSuperAdmin(user))
                opportunityVMs = _mapper.Map<List<OpportunityViewModel>>(await _opportunitiesBiz.GetAllUnarchivedAsync());
            else
                opportunityVMs = _mapper.Map<List<OpportunityViewModel>>(await _opportunitiesBiz.GetAllUnarchivedByUserAsync(user.Id, user.DepartmentId,
                    userDepartments.Select(x => x.DepartmentId).ToList()));
            ViewData["BudgetTotal"] = opportunityVMs.Sum(o => o.Budget);

            var favourityType = await _favouriteTypesBiz.GetByEnumValue((int)FavouriteTypeEnum.Opportunities);
            foreach (var opportunityVM in opportunityVMs)
            {
                opportunityVM.IsFavourite = await _favouritesBiz.IsFavourite(favourityType.Id, user.Id, opportunityVM.Id);
            }
            return PartialView("_ShowOpportunitiesPartial", opportunityVMs);
        }

        [Authorize(Permissions.Opportunities.Create)]
        public async Task<IActionResult> Create(int typeId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userDepartments = await _userDepartmentsBiz.GetByUserAsync(user.Id);
            bool userIsSuperAdmin = await _userManager.IsSuperAdmin(user);
            if (userIsSuperAdmin)
                ViewData["DepartmentId"] = await _departmentsBiz.GetAllUnarchivedAsync();
            else
                ViewData["DepartmentId"] = await _departmentsBiz.GetAllUnarchivedByUserAsync(user.DepartmentId,
                    userDepartments.Select(x => x.DepartmentId).ToList());
            var opportunityTypes = await _opportunityTypesBiz.GetAllAsync();
            string defaultTypeName, defaultTypeNameAr;
            int defaultTypeId;
            var selectedType = opportunityTypes.FirstOrDefault(t => t.Id == typeId);
            if (selectedType != null)
            {
                defaultTypeId = selectedType.Id;
                defaultTypeName = selectedType.Name;
                defaultTypeNameAr = selectedType.NameAr;
            }
            else
            {
                defaultTypeId = opportunityTypes.First().Id;
                defaultTypeName = opportunityTypes.First().Name;
                defaultTypeNameAr = opportunityTypes.First().NameAr;
            }

            ViewData["TypeId"] = opportunityTypes;
            ViewData["DefaultTypeId"] = defaultTypeId;
            ViewData["DefaultTypeName"] = defaultTypeName;
            ViewData["DefaultTypeNameAr"] = defaultTypeNameAr;

            if (await _userManager.IsSuperAdmin(user))
                ViewData["CustomerId"] = await _customersBiz.GetAllUnarchivedAsync();
            else
                ViewData["CustomerId"] = await _customersBiz.GetAllUnarchivedByUserAsync(user.Id, user.DepartmentId, userDepartments.Select(x => x.DepartmentId).ToList());

            ViewData["CountryId"] = await _countriesBiz.GetAllUnarchivedAsync();
            ViewData["SalesAgentId"] = _mapper.Map<List<UserViewModel>>(await _userManager.GetSalesAgentsAsync());

            if (await _userManager.IsSuperAdmin(user))
                ViewData["DepartmentId"] = await _departmentsBiz.GetAllUnarchivedAsync();
            else
                ViewData["DepartmentId"] = await _departmentsBiz.GetAllUnarchivedByUserAsync(user.DepartmentId, userDepartments.Select(x => x.DepartmentId).ToList());

            ViewData["BankId"] = await _banksBiz.GetAllUnarchivedAsync();
            ViewData["AssignedToId"] = _userManager.Users
                .Select(u => _mapper.Map<UserViewModel>(u))
                .ToList();

            ViewData["Periods"] = new List<Period>
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

            ViewData["ProductId"] = _mapper.Map<List<ProductViewModel>>(await _productsBiz.GetAllUnarchivedAsync());
            ViewData["SupplierId"] = _mapper.Map<List<SupplierViewModel>>(await _suppliersBiz.GetAllUnarchivedAsync());
            ViewData["ProductVMs"] = new List<TechnicalSpecificationProductViewModel>();
            ViewData["SupplierOffer_SupplierId"] = new List<SupplierViewModel>();
            ViewData["SupplierOffers"] = new List<SupplierOfferViewModel>();
            ViewData["CurrencyId"] = (await _currenciesBiz.GetAllUnarchivedAsync()).ToList();
            ViewData["DeliveryTypeId"] = (await _deliveryTypesBiz.GetAllUnarchivedAsync()).ToList();
            ViewData["PaymentTypeId"] = (await _paymentTypesBiz.GetAllUnarchivedAsync()).ToList();

            return View(new OpportunityViewModel()
            {
                TypeId = defaultTypeId
            });
        }

        [HttpPost]
        [Authorize(Permissions.Opportunities.Create)]
        public async Task<IActionResult> Create(OpportunityViewModel model, string actionType = "Add")
        {
            if (model.TechnicalSpecificationVM != null && model.TechnicalSpecificationVM.ProductVMs != null)
            {
                foreach (var productVM in model.TechnicalSpecificationVM.ProductVMs)
                {
                    if (productVM.RequestedOriginIdsStr != null)
                    {
                        productVM.RequestedOriginVMs = new List<TechnicalSpecificationProductOriginViewModel>();
                        foreach (string countryId in productVM.RequestedOriginIdsStr.Split(','))
                        {
                            productVM.RequestedOriginVMs.Add(new TechnicalSpecificationProductOriginViewModel
                            {
                                CountryId = int.Parse(countryId),
                                TechnicalSpecificationProductId = productVM.Id
                            });
                        }
                    }
                }
            }

            ProcessDataViewModel processResult = new()
            {
                ViewModel = model
            };

            if (model.BookTenderVM.ReceiptNo != "" && model.BookTenderVM.ReceiptAttachmentId != null)
            {
                ModelState.Remove("BookTenderVM.ReceiptAttachmentFile");
            }

            if (model.BidBondVM.BidBondOff_ReceiptNo != "" && model.BidBondVM.BidBondAttachmentId != null)
            {
                ModelState.Remove("BidBondVM.BidBondFile");
            }

            if (!model.IsBidBond)
            {
                ModelState.Remove("BidBondVM.Amount");
            }

            if (!model.IsBookTender)
            {
                ModelState.Remove("BookTenderVM.Amount");
            }

            if (ModelState.IsValid)
            {
                processResult = await ProcessData(model);

                if (processResult.IsValid)
                {
                    string userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
                    processResult.Opportunity.CreatedById = userId;
                    processResult.Opportunity.SerialNo = await _opportunitiesBiz.GetNewSerialNo(null, processResult.Opportunity.DueDate.Year, processResult.Opportunity.DepartmentId);
                    var result = await _opportunitiesBiz.AddAsync(processResult.Opportunity, userId);
                    List<string> sendToUserIds = new List<string>();
                    var manger = await _userManager.FindByNameAsync(User.Identity.Name);
                    sendToUserIds.Add(model.SalesAgentId);
                    if (model.ProjectManagerId != null)
                    {
                        sendToUserIds.Add(model.ProjectManagerId);
                    }
                    if (model.GuestId != null)
                    {
                        sendToUserIds.Add(model.GuestId);
                    }
                    foreach (var item in await _userManager.GetManagersAsync())
                    {
                        if (item.DepartmentId == model.DepartmentId || await _userDepartmentsBiz.IsInDepartmentAsync(item.Id, (int)model.DepartmentId))
                        {
                            sendToUserIds.Add(item.Id);
                        }
                    }
                    // Add items using Add method   

                    if (result)
                    {
                        NotificationViewModel notificationViewModel = new NotificationViewModel
                        {
                            CreatedById = userId,
                            CreatedDate = DateTime.UtcNow,
                            Type = NotificationTypeEnum.Opportunity_New,
                            Route = "/",
                            Details = model.Name,
                            NotificationUserVMs = sendToUserIds.Select(x => new NotificationUserViewModel
                            {
                                SendToId = x,
                                IsRead = false
                            }).ToList()
                        };

                        await _hubNotificationHelper.SendNotification(notificationViewModel);

                        // create project with opportunities
                        var department = await _departmentsBiz.GetByIdAsync((int)model.DepartmentId);
                        ProjectViewModel project = new()
                        {
                            Code = model.DueDate.HasValue ? model.DueDate.Value.ToString("yy") + department.Code + processResult.Opportunity.SerialNo.ToString() : "",
                            Name = model.Name,
                            DepartmentId = (int)model.DepartmentId,
                            CustomerId = model.CustomerId,
                            OpportunityId = processResult.Opportunity.Id,
                            Status = ProjectStatus.Opportunity,
                            CreatedDate = DateTime.UtcNow,
                        };
                        var newProject = _mapper.Map<Project>(project);
                        await _projectsBiz.AddAsync(newProject);

                        await SaveUserActivity(userId, UserActivities.Opportunities.Create, model.Name);

                        switch (actionType)
                        {
                            case "Add":
                                return RedirectToAction(nameof(Index));
                            case "Save":
                                return RedirectToAction(nameof(Details), new { id = processResult.Opportunity.Id });
                        }
                    }
                }
            }

            var opportunityTypes = await _opportunityTypesBiz.GetAllAsync();
            var selectedType = opportunityTypes.FirstOrDefault(t => t.Id == model.TypeId);
            int defaultTypeId = selectedType.Id;
            string defaultTypeName = selectedType.Name;
            string defaultTypeNameAr = selectedType.NameAr;

            ViewData["TypeId"] = opportunityTypes;
            ViewData["DefaultTypeId"] = defaultTypeId;
            ViewData["DefaultTypeName"] = defaultTypeName;
            ViewData["DefaultTypeNameAr"] = defaultTypeNameAr;

            ViewData["CustomerId"] = await _customersBiz.GetAllAsync();
            ViewData["CountryId"] = await _countriesBiz.GetAllAsync();
            ViewData["SalesAgentId"] = _mapper.Map<List<UserViewModel>>(await _userManager.GetSalesAgentsAsync());
            ViewData["DepartmentId"] = await _departmentsBiz.GetAllAsync();
            ViewData["BankId"] = await _banksBiz.GetAllUnarchivedAsync();
            ViewData["AssignedToId"] = _userManager.Users
                .Select(u => _mapper.Map<UserViewModel>(u))
                .ToList();
            ViewData["Periods"] = new List<Period>
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

            ViewData["ProductId"] = _mapper.Map<List<ProductViewModel>>(await _productsBiz.GetAllUnarchivedAsync());
            ViewData["SupplierId"] = _mapper.Map<List<SupplierViewModel>>(await _suppliersBiz.GetAllUnarchivedAsync());
            if (model.TechnicalSpecificationVM.Id != 0)
                ViewData["ProductVMs"] = await _technicalSpecificationProductsBiz.GetByTechnicalSpecificationId(model.TechnicalSpecificationVM.Id);
            else
                ViewData["ProductVMs"] = new List<TechnicalSpecificationProductViewModel>();
            ViewData["SupplierOffer_SupplierId"] = new List<SupplierViewModel>();
            ViewData["SupplierOffers"] = new List<SupplierOfferViewModel>();
            ViewData["CurrencyId"] = (await _currenciesBiz.GetAllUnarchivedAsync()).ToList();
            ViewData["DeliveryTypeId"] = (await _deliveryTypesBiz.GetAllUnarchivedAsync()).ToList();
            ViewData["PaymentTypeId"] = (await _paymentTypesBiz.GetAllUnarchivedAsync()).ToList();

            return View(processResult.ViewModel);
        }

        [Authorize(Permissions.Opportunities.Details)]
        public async Task<IActionResult> Details(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var favourityType = await _favouriteTypesBiz.GetByEnumValue((int)FavouriteTypeEnum.Opportunities);
            var opportunity = await _opportunitiesBiz.GetByIdAsync(id);
            var model = _mapper.Map<OpportunityViewModel>(opportunity);
            model.IsFavourite = await _favouritesBiz.IsFavourite(favourityType.Id, user.Id, model.Id);

            ViewData["SupplierId"] = _mapper.Map<List<SupplierViewModel>>((await _suppliersBiz.GetAllUnarchivedAsync()).ToList());
            if (model.TechnicalSpecificationVM.Id != 0)
                ViewData["ProductVMs"] = model.TechnicalSpecificationVM.ProductVMs;
            else
                ViewData["ProductVMs"] = new List<TechnicalSpecificationProductViewModel>();
            ViewData["SupplierOffer_SupplierId"] = _mapper.Map<List<SupplierViewModel>>(opportunity.RequestOffers.Where(o => o.IsEmailSent).Select(x => x.Supplier).ToList());
            ViewData["SupplierOffers"] = _mapper.Map<List<SupplierOfferViewModel>>(opportunity.SupplierOffers);
            ViewData["CurrencyId"] = (await _currenciesBiz.GetAllUnarchivedAsync()).ToList();
            ViewData["DeliveryTypeId"] = (await _deliveryTypesBiz.GetAllUnarchivedAsync()).ToList();
            ViewData["PaymentTypeId"] = (await _paymentTypesBiz.GetAllUnarchivedAsync()).ToList();

            return View(model);
        }

        [Authorize(Permissions.Opportunities.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var opportunity = await _opportunitiesBiz.GetByIdAsync(id);
            var model = _mapper.Map<OpportunityViewModel>(opportunity);

            var opportunityTypes = await _opportunityTypesBiz.GetAllAsync();
            var selectedType = opportunityTypes.FirstOrDefault(t => t.Id == model.TypeId);
            int defaultTypeId = selectedType.Id;
            string defaultTypeName = selectedType.Name;
            string defaultTypeNameAr = selectedType.NameAr;

            ViewData["TypeId"] = opportunityTypes;
            ViewData["DefaultTypeId"] = defaultTypeId;
            ViewData["DefaultTypeName"] = defaultTypeName;
            ViewData["DefaultTypeNameAr"] = defaultTypeNameAr;

            ViewData["CustomerId"] = await _customersBiz.GetAllAsync();
            ViewData["CountryId"] = await _countriesBiz.GetAllAsync();
            ViewData["SalesAgentId"] = _mapper.Map<List<UserViewModel>>(await _userManager.GetSalesAgentsAsync());
            ViewData["DepartmentId"] = await _departmentsBiz.GetAllAsync();
            ViewData["BankId"] = await _banksBiz.GetAllUnarchivedAsync();
            ViewData["AssignedToId"] = _userManager.Users
                .Select(u => _mapper.Map<UserViewModel>(u))
                .ToList();
            ViewData["Periods"] = new List<Period>
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

            ViewData["ProductId"] = _mapper.Map<List<ProductViewModel>>(await _productsBiz.GetAllUnarchivedAsync());
            ViewData["SupplierId"] = _mapper.Map<List<SupplierViewModel>>(await _suppliersBiz.GetAllUnarchivedAsync());
            ViewData["ProductVMs"] = model.TechnicalSpecificationVM.ProductVMs.Where(p => p.ProductId != null).ToList();
            ViewData["SupplierOffer_SupplierId"] = _mapper.Map<List<SupplierViewModel>>(opportunity.RequestOffers.Where(o => o.IsEmailSent).Select(x => x.Supplier).ToList());
            ViewData["SupplierOffers"] = _mapper.Map<List<SupplierOfferViewModel>>(opportunity.SupplierOffers);
            ViewData["CurrencyId"] = (await _currenciesBiz.GetAllUnarchivedAsync()).ToList();
            ViewData["DeliveryTypeId"] = (await _deliveryTypesBiz.GetAllUnarchivedAsync()).ToList();
            ViewData["PaymentTypeId"] = (await _paymentTypesBiz.GetAllUnarchivedAsync()).ToList();

            return View(model);
        }

        [HttpPost]
        [Authorize(Permissions.Opportunities.Edit)]
        public async Task<IActionResult> Edit(OpportunityViewModel model)
        {
            if (model.TechnicalSpecificationVM != null && model.TechnicalSpecificationVM.ProductVMs != null)
            {
                foreach (var productVM in model.TechnicalSpecificationVM.ProductVMs)
                {
                    if (productVM.RequestedOriginIdsStr != null)
                    {
                        productVM.RequestedOriginVMs = new List<TechnicalSpecificationProductOriginViewModel>();
                        foreach (string countryId in productVM.RequestedOriginIdsStr.Split(','))
                        {
                            productVM.RequestedOriginVMs.Add(new TechnicalSpecificationProductOriginViewModel
                            {
                                CountryId = int.Parse(countryId),
                                TechnicalSpecificationProductId = productVM.Id
                            });
                        }
                    }
                }
            }

            ProcessDataViewModel processResult = new()
            {
                ViewModel = model
            };

            if (model.BookTenderVM.ReceiptNo != "" && model.BookTenderVM.ReceiptAttachmentId != null)
            {
                ModelState.Remove("BookTenderVM.ReceiptAttachmentFile");
            }

            if (model.BidBondVM.BidBondOff_ReceiptNo != "" && model.BidBondVM.BidBondAttachmentId != null)
            {
                ModelState.Remove("BidBondVM.BidBondFile");
            }

            if (ModelState.IsValid)
            {
                //////////// Email Suppliers /////////////////////////
                //if (model.RequestOfferVMs != null)
                //{
                //    foreach (var offer in model.RequestOfferVMs)
                //    {
                //        if (offer.IsSendEmail && !offer.IsEmailSent)
                //        {
                //            var supplier = await _suppliersBiz.GetByIdAsync(offer.SupplierId);
                //            try
                //            {
                //                var mailResult = _mailer.Send("New inquiry", "We request an offer for attached product specifications", supplier.Email, supplier.Name,
                //                    _webHostEnvironment.WebRootPath + "/Images/Opportunities/" + offer.RequestOfferAttachmentPath);
                //                if (mailResult)
                //                    offer.IsEmailSent = true;
                //            }
                //            catch { }
                //        }
                //    }
                //}
                //////////////////////////////////////////////////////

                processResult = await ProcessData(model);

                if (processResult.IsValid)
                {
                    string userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
                    processResult.Opportunity.SerialNo = await _opportunitiesBiz.CheckSerialNo(processResult.Opportunity.Id, processResult.Opportunity.DueDate.Year, processResult.Opportunity.DepartmentId, processResult.Opportunity.SerialNo);
                    var result = await _opportunitiesBiz.UpdateAsync(processResult.Opportunity, userId);

                    await SaveUserActivity(userId, UserActivities.Opportunities.Update, model.Name);

                    if (result)
                    {
                        return RedirectToAction(nameof(Details), new
                        {
                            id = processResult.Opportunity.Id
                        });
                    }
                }
            }

            Opportunity opportunity;
            if (processResult.Opportunity != null)
            {
                opportunity = processResult.Opportunity;
            }
            else
            {
                opportunity = await _opportunitiesBiz.GetByIdAsync(model.Id);
            }

            var opportunityTypes = await _opportunityTypesBiz.GetAllAsync();
            var selectedType = opportunityTypes.FirstOrDefault(t => t.Id == model.TypeId);
            int defaultTypeId = selectedType.Id;
            string defaultTypeName = selectedType.Name;
            string defaultTypeNameAr = selectedType.NameAr;

            ViewData["TypeId"] = opportunityTypes;
            ViewData["DefaultTypeId"] = defaultTypeId;
            ViewData["DefaultTypeName"] = defaultTypeName;
            ViewData["DefaultTypeNameAr"] = defaultTypeNameAr;

            ViewData["CustomerId"] = await _customersBiz.GetAllAsync();
            ViewData["CountryId"] = await _countriesBiz.GetAllAsync();
            ViewData["SalesAgentId"] = _mapper.Map<List<UserViewModel>>(await _userManager.GetSalesAgentsAsync());
            ViewData["DepartmentId"] = await _departmentsBiz.GetAllAsync();
            ViewData["BankId"] = await _banksBiz.GetAllUnarchivedAsync();
            ViewData["AssignedToId"] = _userManager.Users
                .Select(u => _mapper.Map<UserViewModel>(u))
                .ToList();
            ViewData["Periods"] = new List<Period>
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

            ViewData["ProductId"] = _mapper.Map<List<ProductViewModel>>(await _productsBiz.GetAllUnarchivedAsync());
            ViewData["SupplierId"] = _mapper.Map<List<SupplierViewModel>>(await _suppliersBiz.GetAllUnarchivedAsync());
            ViewData["ProductVMs"] = _mapper.Map<List<TechnicalSpecificationProductViewModel>>(await _technicalSpecificationProductsBiz.GetByTechnicalSpecificationId(model.TechnicalSpecificationVM.Id)).ToList();
            ViewData["SupplierOffer_SupplierId"] = _mapper.Map<List<SupplierViewModel>>(opportunity.RequestOffers.Where(o => o.IsEmailSent).Select(x => x.Supplier).ToList());
            ViewData["SupplierOffers"] = _mapper.Map<List<SupplierOfferViewModel>>(opportunity.SupplierOffers);
            ViewData["CurrencyId"] = (await _currenciesBiz.GetAllUnarchivedAsync()).ToList();
            ViewData["DeliveryTypeId"] = (await _deliveryTypesBiz.GetAllUnarchivedAsync()).ToList();
            ViewData["PaymentTypeId"] = (await _paymentTypesBiz.GetAllUnarchivedAsync()).ToList();
            return View(model);
        }

        [HttpGet]
        [Authorize(Permissions.Opportunities.TechnicalApprove)]
        public async Task<IActionResult> TechnicalApprove(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var result = await _opportunitiesBiz.TechnicalApproveAsync(id, user.Id);
            if (!result)
            {
                ModelState.AddModelError("", "Something wrong happend, please contact the administrator");
            }

            var opportunity = _mapper.Map<OpportunityViewModel>(await _opportunitiesBiz.GetBasicDataByIdAsync(id));
            await SaveUserActivity(user.Id, UserActivities.Opportunities.TechnicalApprove, opportunity.Name);

            return RedirectToAction(nameof(Details), new
            {
                id
            });
        }

        [Authorize(Permissions.Opportunities.ConvertToQuotation)]
        public async Task<IActionResult> ConvertToQuotation(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var opportunity = await _opportunitiesBiz.GetBasicDataByIdAsync(id);

            var quotation = new Quotation
            {
                AdminId = opportunity.CreatedById,
                CreatedById = user.Id,
                CreatedDate = DateTime.UtcNow,
                OpportunityId = id,
                StatusId = (await _quotationStatusesBiz.GetByEnumValue((int)QuotationStatusEnum.New)).Id,
                DueDate = opportunity.DueDate
            };

            var result = await _quotationsBiz.AddAsync(quotation, user.Id);
            if (result)
            {
                opportunity.IsConverted = true;
                opportunity.ConvertedById = user.Id;
                await _opportunitiesBiz.UpdateAsync(opportunity, user.Id);

                await SaveUserActivity(user.Id, UserActivities.Opportunities.ConvertToQuotation, opportunity.Name);

                return RedirectToAction(nameof(Details), "Quotations", new
                {
                    id = quotation.Id
                });
            }

            return RedirectToAction(nameof(Details), new
            {
                id
            });
        }

        [Authorize(Permissions.Opportunities.Delete)]
        public async Task<IActionResult> Archive(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            var result = await _opportunitiesBiz.ArchiveAsync(id, userId);

            var opportunity = _mapper.Map<OpportunityViewModel>(await _opportunitiesBiz.GetBasicDataByIdAsync(id));
            await SaveUserActivity(userId, UserActivities.Opportunities.Archive, opportunity.Name);

            return Json(result);
        }

        [Authorize(Permissions.Opportunities.Delete)]
        public async Task<IActionResult> Unarchive(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            var result = await _opportunitiesBiz.UnarchiveAsync(id, userId);

            var opportunity = _mapper.Map<OpportunityViewModel>(await _opportunitiesBiz.GetBasicDataByIdAsync(id));
            await SaveUserActivity(userId, UserActivities.Opportunities.Unarchive, opportunity.Name);

            return Json(result);
        }

        public async Task<IActionResult> GetProductCategories()
        {
            var categories = _mapper.Map<List<ProductCategoryViewModel>>(await _productCategoriesBiz.GetCategoriesWithChilds()).Where(c => c.ParentId == null).ToList();

            return PartialView("_CategoriesSelectPartial", categories);
        }

        public async Task<IActionResult> GetProductsByCategoryId(int id)
        {
            var products = _mapper.Map<List<ProductViewModel>>(await _productsBiz.GetByCategoryId(id));

            return PartialView("_ProductsSelectItemsPartial", products);
        }

        [Authorize(Permissions.Opportunities.PrintBidBond)]
        public async Task<IActionResult> PrintBidbondRequest(int requestId)
        {
            var request = _mapper.Map<BidBondRequestViewModel>(await _bidBondRequestsBiz.GetByIdAsync(requestId));

            return View(request);
        }

        [Authorize(Permissions.Opportunities.PrintBookTender)]
        public async Task<IActionResult> PrintBookTenderRequest(int requestId)
        {
            var request = _mapper.Map<BookTenderRequestViewModel>(await _bookTenderRequestsBiz.GetByIdAsync(requestId));

            return View(request);
        }

        public async Task<IActionResult> ChangeOfferAcceptance(int id, int offerId)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            await _supplierOffersBiz.ChangeAcceptance(offerId, userId);

            var supplierOffers = _mapper.Map<List<SupplierOfferViewModel>>(await _supplierOffersBiz.GetByOpportunityId(id));

            var offer = _mapper.Map<SupplierOfferViewModel>(await _supplierOffersBiz.GetByIdAsync(offerId));
            await SaveUserActivity(userId, UserActivities.Opportunities.ChangeOfferAcceptance, offer.SupplierName);

            return PartialView("_SupplierOffersPartial", supplierOffers.OrderBy(o => o.LocalTotalPriceWCIF).ToList());
        }

        public IActionResult ReloadViewComponent()
        {
            return ViewComponent("OpportunitiesArchive", new { isDone = true });
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CreateCustomerViewModel model)
        {
            //var Customers = _mapper.Map<List<CustomerViewModel>>(await _customersBiz.GetAllAsync());
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var result = false;
            if (ModelState.IsValid)
            {
                model.DepartmentVMs = new List<CustomerDepartmentViewModel>();
                foreach (int departmentId in model.DepartmentIds)
                {
                    model.DepartmentVMs.Add(new CustomerDepartmentViewModel
                    {
                        DepartmentId = departmentId
                    });
                }
                var customer = _mapper.Map<Customer>(model);
                customer.CreatedById = user.Id;
                result = await _customersBiz.AddAsync(customer, user.Id);
                if (result)
                {
                    await SaveUserActivity(user.Id, UserActivities.Opportunities.CreateCustomer, model.Name);

                    ViewData["CustomerId"] = await _customersBiz.GetAllAsync();
                    ViewData["SelectedCustomerId"] = customer.Id;
                    return PartialView("_ShowCustomerPartial");
                }
            }
            return Json(result);

        }

        [HttpPost]
        public async Task<IActionResult> CreateSupplier(CreateSupplierViewModel model)
        {
            //var Customers = _mapper.Map<List<CustomerViewModel>>(await _customersBiz.GetAllAsync());
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var result = false;
            if (ModelState.IsValid)
            {
                model.ProductVMs = new List<SupplierProductViewModel>();
                foreach (int SupplierProductId in model.SupplierProductIds)
                {
                    model.ProductVMs.Add(new SupplierProductViewModel
                    {
                        ProductId = SupplierProductId
                    });
                }


                model.DepartmentVMs = new List<SupplierDepartmentViewModel>();
                foreach (int departmentId in model.DepartmentIds)
                {
                    model.DepartmentVMs.Add(new SupplierDepartmentViewModel
                    {
                        DepartmentId = departmentId
                    });
                }

                var supplier = _mapper.Map<Supplier>(model);
                result = await _suppliersBiz.AddAsync(supplier, user.Id);
                if (result)
                {
                    await SaveUserActivity(user.Id, UserActivities.Opportunities.CreateSupplier, model.Name);

                    ViewData["SupplierId"] = await _customersBiz.GetAllAsync();
                    ViewData["SelectedSupplierId"] = supplier.Id;
                    return Json(result);
                }
            }
            return Json(result);

        }
    }
}

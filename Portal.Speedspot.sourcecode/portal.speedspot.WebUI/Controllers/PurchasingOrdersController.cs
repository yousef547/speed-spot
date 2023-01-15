using AutoMapper;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

using portal.speedspot.BizLayer.BizMethods;
using portal.speedspot.BizLayer.IdentityModule;
using portal.speedspot.Models.Concretes;
using portal.speedspot.Models.Constants;
using portal.speedspot.Models.ViewModels;
using portal.speedspot.WebUI.Controllers.Infrastructure;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using static portal.speedspot.Models.Constants.ApplicationEnums;

namespace portal.speedspot.WebUI.Controllers
{
    public class PurchasingOrdersController : BaseController
    {
        private readonly PurchaseOrdersBiz _purchaseOrdersBiz;
        private readonly SupplierOffersBiz _supplierOffersBiz;
        private readonly NegotiationResultsBiz _negotiationResultsBiz;
        private readonly QuotationOfferVersionsBiz _quotationOfferVersionsBiz;
        private readonly BanksBiz _banksBiz;
        private readonly CurrenciesBiz _CurrenciesBiz;
        private readonly PaymentTypesBiz _paymentTypesBiz;
        private readonly SupplierPoBiz _SupplierPoBiz;
        private readonly CustomerPoBiz _CustomerPoBiz;
        private readonly AttachmentsBiz _AttachmentsBiz;
        private readonly FundDetailsBiz _FundDetailsBiz;
        public PurchasingOrdersController(
            PurchaseOrdersBiz purchaseOrdersBiz,
            SupplierOffersBiz supplierOffersBiz,
            NegotiationResultsBiz negotiationResultsBiz,
            QuotationOfferVersionsBiz quotationOfferVersionsBiz,
            CurrenciesBiz currenciesBiz,
            PaymentTypesBiz paymentTypesBiz,
            BanksBiz banksBiz,
            ApplicationUserManager userManager,
            SupplierPoBiz supplierPoBiz,
            CustomerPoBiz customerPoBiz,
            AttachmentsBiz attachmentsBiz,
            FundDetailsBiz fundDetailsBiz,
            IMapper mapper,
            IWebHostEnvironment hostEnvironment
            ) : base(mapper, hostEnvironment, userManager)
        {
            _purchaseOrdersBiz = purchaseOrdersBiz;
            _negotiationResultsBiz = negotiationResultsBiz;
            _supplierOffersBiz = supplierOffersBiz;
            _quotationOfferVersionsBiz = quotationOfferVersionsBiz;
            _banksBiz = banksBiz;
            _CurrenciesBiz = currenciesBiz;
            _paymentTypesBiz = paymentTypesBiz;
            _SupplierPoBiz = supplierPoBiz;
            _CustomerPoBiz = customerPoBiz;
            _AttachmentsBiz = attachmentsBiz;
            _FundDetailsBiz = fundDetailsBiz;
        }


        private async void AddSuplierPo(SupplierPOViewModel model)
        {
            model.SupplierPoOffers = new List<SupplierPoOfferViewModel>();

            foreach (var item in model.SupplierPoOffers)
            {
                model.SupplierPoOffers.Add(new SupplierPoOfferViewModel
                {
                    SupplierOfferId = item.SupplierOfferId,
                });
            }

            var supplierPo = _mapper.Map<SupplierPo>(model);
            var addSupplier = await _SupplierPoBiz.AddAsync(supplierPo);


        }

        public IActionResult CalculateEndDate(DateTime startDate, int deliveryPeriod)
        {
            var endDate = startDate.AddDays(deliveryPeriod);
            return Json(new
            {
                endDate,
                endDateStr = endDate.ToString("dd/MM/yyyy"),
            });
        }
        public async Task<IActionResult> Index()
        {
            var purchaseOrders = _mapper.Map<List<PurchaseOrderViewModel>>(await _purchaseOrdersBiz.GetAllUnarchivedAsync());
            return View(purchaseOrders);
        }
        public async Task<IActionResult> Create(int id, FundType type)
        {
            var purchas = await _purchaseOrdersBiz.GetByIdAsync(id);
            var model = _mapper.Map<PurchaseOrderViewModel>(purchas);
            var offerAccept = _mapper.Map<List<SupplierOfferViewModel>>(await _supplierOffersBiz.GetByOpportunityId(model.OpportunityId));
            model.OfferAccept = offerAccept;
            var negotiation = _mapper.Map<NegotiationResultViewModel>(await _negotiationResultsBiz.GetByQuotationIdAsync(model.QuotationId));
            if (negotiation == null)
            {
                var quotationOfferVersion = _mapper.Map<QuotationOfferVersionViewModel>(await _quotationOfferVersionsBiz.GetSelectedQuotationVersion(model.QuotationId));
                ViewData["resultNegotiation"] = quotationOfferVersion.DeliveryToDays;
            }
            else
            {
                ViewData["resultNegotiation"] = negotiation.DeliveryToDays;
            }
            ViewData["DeliveryToDays"] = model.SelectedVersionVM.DeliveryToDays;
            ViewData["products"] = model.SelectedVersionVM.ProductVMs;
            ViewData["quotationNo"] = model.QuotationNo;
            ViewData["supplierPos"] = _mapper.Map<List<SupplierPOViewModel>>(await _SupplierPoBiz.GetAllSupplierByPurchasingOrder(id));
            ViewBag.Currencies = _mapper.Map<List<CurrencyViewModel>>(await _CurrenciesBiz.GetAllUnarchivedAsync());
            ViewBag.Banks = _mapper.Map<List<BankViewModel>>(await _banksBiz.GetAllUnarchivedAsync());
            ViewData["EndDate"] = DateTime.UtcNow.AddDays((int)ViewData["resultNegotiation"]);
            ViewBag.typeFound = type;
            ViewBag.PaymentTypes = await _paymentTypesBiz.GetAllUnarchivedAsync();

            return View(model);
        }
        public class PurchaseOrderModel
        {
            public CustomerPOViewModel CustomerPO { get; set; }
            public List<SupplierPOViewModel> SupplierPO { get; set; }

            public FundDetailsViewModel FundDetails { get; set; }

        }

        [HttpPost]
        public async Task<IActionResult> Create(PurchaseOrderViewModel model)
        {
            var user = (await _userManager.FindByNameAsync(User.Identity.Name));
            var updatepurchase = false;
            if (ModelState.IsValid)
            {
                var deletedSupplierPayments = new List<FundDetailsPaymentViewModel>();
                if (model.FundDetailsVM != null)
                {
                    for (var i = 0; i < model.FundDetailsVM.AllSupplierPayments.Count(); i++)
                    {
                        var payment = model.FundDetailsVM.AllSupplierPayments[i];
                        if (payment.SupplierPoId == null || payment.Amount == 0 || payment.PaymentTypeId == null)
                        {
                            deletedSupplierPayments.Add(payment);
                        }
                    }
                }
                foreach (var payment in deletedSupplierPayments)
                {
                    model.FundDetailsVM.AllSupplierPayments.Remove(payment);
                }

                //Process Supplier Offers

                foreach (var item in model.SupplierPOVMs)
                {
                    item.Type = SupplierPoType.Tender;
                    if (item.SupplierPoOffers != null)
                    {
                        foreach (var offer in item.SupplierPoOffers)
                        {
                            item.SupplierPoOffers = new List<SupplierPoOfferViewModel>();
                            item.SupplierPoOffers.Add(new SupplierPoOfferViewModel
                            {
                                SupplierOfferId = offer.SupplierOfferId,
                            });
                        }
                    }
                }

                model.CustomerPOVM.PoAttachmentPath = new List<CustomerPoAttachmentViewModel>();
                if (model.CustomerPOVM.AllActtachment != null)
                {
                    foreach (var item in model.CustomerPOVM.AllActtachment.ToList())
                    {
                        string uniqueFileName = UploadFile(item, "CustomerPo");
                        var attachment = new Attachment { FilePath = uniqueFileName, Title = item.FileName, UploadedById = user.Id };
                        var attach = await _AttachmentsBiz.AddAsync(attachment);
                        model.CustomerPOVM.PoAttachmentPath.Add(new CustomerPoAttachmentViewModel
                        {
                            AttachmentId = attachment.Id,
                        });
                    }
                }
                model.CustomerPOVM.PurchaseOrderId = model.Id;
                var GeneratCode = await _CustomerPoBiz.GenerateNewCode(model.DepartmentId);
                model.CustomerPOVM.Code = GeneratCode;
                model.Status = PurchaseOrderStatus.Completed;

                if (model.FundDetailsVM != null)
                {
                    model.FundDetailsVM.PurchaseOrderId = model.CustomerPOVM.PurchaseOrderId;


                    if (model.FundDetailsVM.CollectionFormFile != null)
                    {

                        string uniqueFileNameCollection = UploadFile(model.FundDetailsVM.CollectionFormFile, "fundDetails");
                        var attachmentCollection = new Attachment { FilePath = uniqueFileNameCollection, Title = model.FundDetailsVM.CollectionFormFile.FileName, UploadedById = user.Id };
                        var attachCollection = await _AttachmentsBiz.AddAsync(attachmentCollection);
                        model.FundDetailsVM.CollectionFileId = attachmentCollection.Id;
                    }

                    if (model.FundDetailsVM.ContractFormFile != null)
                    {
                        string uniqueFileNameContract = UploadFile(model.FundDetailsVM.ContractFormFile, "fundDetails");
                        var attachmentContract = new Attachment { FilePath = uniqueFileNameContract, Title = model.FundDetailsVM.ContractFormFile.FileName, UploadedById = user.Id };
                        var attachContract = await _AttachmentsBiz.AddAsync(attachmentContract);
                        model.FundDetailsVM.ContractFileId = attachmentContract.Id;
                    }
                }
                model.LastUpdatedById = user.Id;
                updatepurchase = await _purchaseOrdersBiz.UpdateAsync(_mapper.Map<PurchaseOrder>(model));

            }
            ViewData["index"] = model.CustomerPOVM.PurchaseOrderId;
            ViewData["supplierPos"] = _mapper.Map<List<SupplierPOViewModel>>(await _SupplierPoBiz.GetAllSupplierByPurchasingOrder(model.CustomerPOVM.PurchaseOrderId));
            ViewBag.Banks = _mapper.Map<List<BankViewModel>>(await _banksBiz.GetAllUnarchivedAsync());
            ViewBag.Currencies = _mapper.Map<List<CurrencyViewModel>>(await _CurrenciesBiz.GetAllUnarchivedAsync());
            ViewData["products"] = _mapper.Map<PurchaseOrderViewModel>(await _purchaseOrdersBiz.GetByIdAsync(model.Id)).SelectedVersionVM.ProductVMs;
            ViewBag.PaymentTypes = await _paymentTypesBiz.GetAllUnarchivedAsync();
            if (updatepurchase)
            {
                return RedirectToAction(nameof(ProjectDetails), new { id = model.Id });
            }
            return RedirectToAction(nameof(Create), new { id = model.Id, type = model.Type });

        }

        public async Task<IActionResult> ProjectDetails(int id)
        {
            var PurchasingOrder = _mapper.Map<PurchaseOrderViewModel>(await _purchaseOrdersBiz.GetByIdAsync(id));
            ViewData["products"] = PurchasingOrder.SelectedVersionVM.ProductVMs;

            return View(PurchasingOrder);
        }
        public IActionResult Projectindex()
        {
            return View();
        }
        public IActionResult Customerindex()
        {
            return View();
        }
        public IActionResult Supplierindex()
        {
            return View();
        }

        public class SupplierPaymentModel
        {
            public IList<FundDetailsPaymentViewModel> SupplierPayment { get; set; }
            public SupplierPaymentModel()
            {
                if (SupplierPayment == null) SupplierPayment = new List<FundDetailsPaymentViewModel>();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddSupplierPayment(SupplierPaymentModel model, int id, bool add)
        {
            if (add)
            {
                model.SupplierPayment.Add(new FundDetailsPaymentViewModel());
            }
            ViewBag.PaymentTypes = await _paymentTypesBiz.GetAllUnarchivedAsync();
            ViewData["supplierPos"] = _mapper.Map<List<SupplierPOViewModel>>(await _SupplierPoBiz.GetAllSupplierByPurchasingOrder(id));


            return PartialView("_AddSupplierPaymentPartial", model.SupplierPayment);

        }

        public async Task<IActionResult> DeleteSupplierPayment(SupplierPaymentModel model, int index, int id)
        {
            ViewData["supplierPos"] = _mapper.Map<List<SupplierPOViewModel>>(await _SupplierPoBiz.GetAllSupplierByPurchasingOrder(id));
            model.SupplierPayment.RemoveAt(index);
            ViewBag.PaymentTypes = await _paymentTypesBiz.GetAllUnarchivedAsync();
            return PartialView("_AddSupplierPaymentPartial", model.SupplierPayment);
        }

        public async Task<IActionResult> GetSupplierOffers(int id, int supplierId)
        {
            var offers = _mapper.Map<List<SupplierOfferViewModel>>(await _supplierOffersBiz.GetSupplierOffersAsync(id, supplierId));
            //return Json(offers);
            return PartialView("_TableSupplierOffersPartial", offers);
        }


        public async Task<IActionResult> AddFundDetails(FundDetailsViewModel model, List<FundDetailsPaymentViewModel> detailsPayment)
        {
            var user = (await _userManager.FindByNameAsync(User.Identity.Name));
            if (detailsPayment.Count == 0)
            {
                ModelState.AddModelError("", _localizationService["RequiredField"].Value);
            }
            if (ModelState.IsValid)
            {
                model.AllSupplierPayments = new List<FundDetailsPaymentViewModel>();
                foreach (FundDetailsPaymentViewModel item in detailsPayment)
                {
                    model.AllSupplierPayments.Add(new FundDetailsPaymentViewModel
                    {
                        Amount = item.Amount,
                        SupplierPoId = item.SupplierPoId,
                        PaymentTypeId = item.PaymentTypeId
                    });
                }

                string uniqueFileNameCollection = UploadFile(model.CollectionFormFile, "fundDetails");
                var attachmentCollection = new Attachment { FilePath = uniqueFileNameCollection, Title = model.CollectionFormFile.FileName, UploadedById = user.Id };
                var attachCollection = await _AttachmentsBiz.AddAsync(attachmentCollection);
                model.CollectionFileId = attachmentCollection.Id;

                string uniqueFileNameContract = UploadFile(model.ContractFormFile, "fundDetails");
                var attachmentContract = new Attachment { FilePath = uniqueFileNameCollection, Title = model.ContractFormFile.FileName, UploadedById = user.Id };
                var attachContract = await _AttachmentsBiz.AddAsync(attachmentContract);
                model.ContractFileId = attachmentContract.Id;

                var fundDetails = _mapper.Map<FundDetails>(model);
                await _FundDetailsBiz.AddAsync(fundDetails);
            }
            ViewBag.Banks = _mapper.Map<List<BankViewModel>>(await _banksBiz.GetAllUnarchivedAsync());
            ViewBag.Currencies = _mapper.Map<List<CurrencyViewModel>>(await _CurrenciesBiz.GetAllUnarchivedAsync());
            ViewBag.PaymentTypes = await _paymentTypesBiz.GetAllUnarchivedAsync();
            ViewData["supplierPos"] = _mapper.Map<List<SupplierPOViewModel>>(await _SupplierPoBiz.GetAllSupplierByPurchasingOrder(model.PurchaseOrderId));
            return PartialView("_FundDetailsPartial", model);

        }



        public async Task<IActionResult> GetDeatilsSupplierPo(int id)
        {
            var supplierPo = _mapper.Map<SupplierPOViewModel>(await _SupplierPoBiz.GetDetailsSupplierPo(id));
            return Json(supplierPo);
        }

    }
}

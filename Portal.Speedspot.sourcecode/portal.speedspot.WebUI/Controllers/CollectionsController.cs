using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using portal.speedspot.BizLayer.BizMethods;
using portal.speedspot.Models.ViewModels;
using portal.speedspot.WebUI.Controllers.Infrastructure;

namespace portal.speedspot.WebUI.Controllers
{
    public class CollectionsController : BaseController
    {
        private readonly PurchaseOrdersBiz _purchaseOrdersBiz;
        private readonly CurrenciesBiz _currenciesBiz;
        private readonly PaymentTermsBiz _paymentTermsBiz;
        private readonly PerformaInvoicesBiz _performaInvoicesBiz;
        private readonly VATValuesBiz _vATValuesBiz;
        private readonly QuotationOfferVersionsBiz _quotationOfferVersionsBiz;

        public CollectionsController(
            IMapper mapper,
            PurchaseOrdersBiz purchaseOrdersBiz,
            CurrenciesBiz currenciesBiz,
            PaymentTermsBiz paymentTermsBiz,
            PerformaInvoicesBiz performaInvoicesBiz,
            VATValuesBiz vATValuesBiz,
            QuotationOfferVersionsBiz quotationOfferVersionsBiz)
            : base(mapper)
        {
            _purchaseOrdersBiz = purchaseOrdersBiz;
            _currenciesBiz = currenciesBiz;
            _paymentTermsBiz = paymentTermsBiz;
            _performaInvoicesBiz = performaInvoicesBiz;
            _vATValuesBiz = vATValuesBiz;
            _quotationOfferVersionsBiz = quotationOfferVersionsBiz;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Invoices()
        {
            return View();
        }

        public async Task<IActionResult> CreatePerformaInvoice()
        {
            var purchaseOrders = _mapper.Map<List<PurchaseOrderViewModel>>(await _purchaseOrdersBiz.GetCompletedAsync());
            ViewData["PurchaseOrderId"] = purchaseOrders;

            ViewData["CurrencyId"] = await _currenciesBiz.GetAllUnarchivedAsync();

            ViewData["PaymentTermId"] = await _paymentTermsBiz.GetAllUnarchivedAsync();

            ViewData["VATValueId"] = _mapper.Map<List<VATValueViewModel>>(await _vATValuesBiz.GetAllUnarchivedAsync());

            return View();
        }

        public async Task<IActionResult> GetPurchaseOrderByCustomerPO(string poNumber)
        {
            var purchaseOrder = _mapper.Map<PurchaseOrderBasicDataViewModel>(await _purchaseOrdersBiz.GetByCustomerPONumber(poNumber));
            if (purchaseOrder is not null)
            {
                return Json(purchaseOrder, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = null
                });
            }
            else
            {
                return Json("");
            }
        }

        public IActionResult CalculateDueDate(DateTime invoiceDate, int daysNo)
        {
            return Json(invoiceDate.AddDays(daysNo).ToString("dd/MM/yyyy"));
        }

        public async Task<IActionResult> GenerateCode(int departmentId, DateTime invoiceDate)
        {
            var generatedCode = await _performaInvoicesBiz.GenerateCode(departmentId, invoiceDate);

            return Json(generatedCode);
        }

        public async Task<IActionResult> GetPurchaseOrderProducts(int purchaseOrderId)
        {
            var purchaseOrder = await _purchaseOrdersBiz.GetByIdAsync(purchaseOrderId);

            var versionProducts = _mapper.Map<List<QuotationOfferProductViewModel>>(
                await _quotationOfferVersionsBiz.GetVersionProductsAsync(purchaseOrder.SelectedVersionId));

            ViewData["VATValueId"] = _mapper.Map<List<VATValueViewModel>>(await _vATValuesBiz.GetAllUnarchivedAsync());

            return PartialView("_ProductsPartial", versionProducts);
        }
    }
}

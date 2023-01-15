using AutoMapper;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using portal.speedspot.BizLayer.BizMethods;
using portal.speedspot.BizLayer.IdentityModule;
using portal.speedspot.Models.Concretes;
using portal.speedspot.Models.Constants;
using portal.speedspot.Models.ViewModels;
using portal.speedspot.WebUI.Controllers.Infrastructure;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace portal.speedspot.WebUI.Controllers
{
    public class PaymentsController : BaseController
    {
        private readonly CurrenciesBiz _currenciesBiz;
        private readonly DepartmentsBiz _departmentsBiz;
        private readonly ProjectsBiz _projectsBiz;
        private readonly FinancialAccountsBiz _financialAccountsBiz;
        private readonly PaymentRequestsBiz _paymentRequestsBiz;
        private readonly UserDepartmentsBiz _userDepartmentsBiz;
        private readonly TreasuriesBiz _treasuriesBiz;
        private readonly BanksBiz _banksBiz;
        private readonly AttachmentsBiz _attachmentsBiz;
        private readonly FinancialAccountTransactionsBiz _financialAccountTransactionsBiz;
        private readonly TreasuryTransactionsBiz _treasuryTransactionsBiz;

        public PaymentsController(
            IMapper mapper,
            ApplicationUserManager userManager,
            IWebHostEnvironment hostEnvironment,
            CurrenciesBiz currenciesBiz,
            DepartmentsBiz departmentsBiz,
            ProjectsBiz projectsBiz,
            FinancialAccountsBiz financialAccountsBiz,
            PaymentRequestsBiz paymentRequestsBiz,
            UserDepartmentsBiz userDepartmentsBiz,
            TreasuriesBiz treasuriesBiz,
            BanksBiz banksBiz,
            AttachmentsBiz attachmentsBiz,
            FinancialAccountTransactionsBiz financialAccountTransactionsBiz,
            TreasuryTransactionsBiz treasuryTransactionsBiz) : base(mapper, hostEnvironment, userManager)
        {
            _currenciesBiz = currenciesBiz;
            _departmentsBiz = departmentsBiz;
            _projectsBiz = projectsBiz;
            _financialAccountsBiz = financialAccountsBiz;
            _paymentRequestsBiz = paymentRequestsBiz;
            _userDepartmentsBiz = userDepartmentsBiz;
            _treasuriesBiz = treasuriesBiz;
            _banksBiz = banksBiz;
            _attachmentsBiz = attachmentsBiz;
            _financialAccountTransactionsBiz = financialAccountTransactionsBiz;
            _treasuryTransactionsBiz = treasuryTransactionsBiz;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userDepartments = await _userDepartmentsBiz.GetByUserAsync(user.Id);

            List<PaymentRequestViewModel> allRequests = new();
            if (await _userManager.IsSuperAdmin(user))
            {
                allRequests = _mapper.Map<List<PaymentRequestViewModel>>(await _paymentRequestsBiz.GetAllUnarchivedAsync());
            }
            else
            {
                allRequests = _mapper.Map<List<PaymentRequestViewModel>>(await _paymentRequestsBiz
                    .GetAllUnarchivedByUserAsync(user.Id, user.DepartmentId, userDepartments.Select(x => x.DepartmentId)
                    .ToList()));
            }

            ViewData["PendingRequests"] = allRequests
                .Where(r => r.Status == ApplicationEnums.RequestStatusEnum.Pending)
                .ToList();

            ViewData["InprogressPayments"] = allRequests
                .Where(r => r.Status == ApplicationEnums.RequestStatusEnum.Approved &&
                ((r.PaymentDetailsVM?.Type == ApplicationEnums.PaymentTypeEnum.Cash && r.ReceiptAttachmentId == null) ||
                (r.PaymentDetailsVM?.Type == ApplicationEnums.PaymentTypeEnum.Cheque && (r.ReceiptAttachmentId == null || r.CashAttachmentId == null)) ||
                (r.PaymentDetailsVM?.Type == ApplicationEnums.PaymentTypeEnum.Transfer && r.TransferAttachmentId == null)))
                .ToList();

            ViewData["PaidPayments"] = allRequests
                .Where(r =>
                (r.PaymentDetailsVM?.Type == ApplicationEnums.PaymentTypeEnum.Cash && r.ReceiptAttachmentId != null) ||
                (r.PaymentDetailsVM?.Type == ApplicationEnums.PaymentTypeEnum.Cheque && r.ReceiptAttachmentId != null && r.CashAttachmentId != null) ||
                (r.PaymentDetailsVM?.Type == ApplicationEnums.PaymentTypeEnum.Transfer && r.TransferAttachmentId != null))
                .ToList();

            return View();
        }

        public async Task<IActionResult> GetAllPayments()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userDepartments = await _userDepartmentsBiz.GetByUserAsync(user.Id);

            List<PaymentRequestViewModel> allRequests = new();
            if (await _userManager.IsSuperAdmin(user))
            {
                allRequests = _mapper.Map<List<PaymentRequestViewModel>>(await _paymentRequestsBiz.GetAllUnarchivedAsync());
            }
            else
            {
                allRequests = _mapper.Map<List<PaymentRequestViewModel>>(await _paymentRequestsBiz
                    .GetAllUnarchivedByUserAsync(user.Id, user.DepartmentId, userDepartments.Select(x => x.DepartmentId)
                    .ToList()));
            }

            ViewData["PendingRequests"] = allRequests
               .Where(r => r.Status == ApplicationEnums.RequestStatusEnum.Pending)
               .ToList();

            ViewData["InprogressPayments"] = allRequests
                .Where(r => r.Status == ApplicationEnums.RequestStatusEnum.Approved &&
                ((r.PaymentDetailsVM?.Type == ApplicationEnums.PaymentTypeEnum.Cash && r.ReceiptAttachmentId == null) ||
                (r.PaymentDetailsVM?.Type == ApplicationEnums.PaymentTypeEnum.Cheque && (r.ReceiptAttachmentId == null || r.CashAttachmentId == null)) ||
                (r.PaymentDetailsVM?.Type == ApplicationEnums.PaymentTypeEnum.Transfer && r.TransferAttachmentId == null)))
                .ToList();

            ViewData["PaidPayments"] = allRequests
                .Where(r =>
                (r.PaymentDetailsVM?.Type == ApplicationEnums.PaymentTypeEnum.Cash && r.ReceiptAttachmentId != null) ||
                (r.PaymentDetailsVM?.Type == ApplicationEnums.PaymentTypeEnum.Cheque && r.ReceiptAttachmentId != null && r.CashAttachmentId != null) ||
                (r.PaymentDetailsVM?.Type == ApplicationEnums.PaymentTypeEnum.Transfer && r.TransferAttachmentId != null))
                .ToList();

            return PartialView("_AllPaymentsPartial");
        }

        public IActionResult Insurance()
        {
            return View();
        }

        public IActionResult Bills()
        {
            return View();
        }

        public async Task<IActionResult> GetProjectByCode(string code)
        {
            var project = await _projectsBiz.GetByCode(code);
            if (project is not null)
            {
                return Json(project.Id);
            }
            else
            {
                return Json("");
            }
        }

        public class DirectionsModel
        {
            public IList<PaymentRequestDirectionViewModel> Directions { get; set; }
        }

        public async Task<IActionResult> AddDirection(DirectionsModel model)
        {
            model.Directions ??= new List<PaymentRequestDirectionViewModel>();

            model.Directions.Add(new PaymentRequestDirectionViewModel());

            ViewData["DirectionId"] = _mapper.Map<List<FinancialAccountViewModel>>(await _financialAccountsBiz.GetAllUnarchivedAsync());

            return PartialView("_DirectionsPartial", model.Directions);
        }

        public async Task<IActionResult> DeleteDirection(int index, DirectionsModel model)
        {
            model.Directions.RemoveAt(index);

            ViewData["DirectionId"] = _mapper.Map<List<FinancialAccountViewModel>>(await _financialAccountsBiz.GetAllUnarchivedAsync());

            return PartialView("_DirectionsPartial", model.Directions);
        }

        public async Task<IActionResult> RequestPayment()
        {
            ViewData["CurrencyId"] = await _currenciesBiz.GetAllUnarchivedAsync();
            ViewData["DepartmentId"] = await _departmentsBiz.GetAllUnarchivedAsync();
            ViewData["ProjectId"] = _mapper.Map<List<ProjectViewModel>>(await _projectsBiz.GetAllUnarchivedAsync());
            ViewData["DirectionId"] = _mapper.Map<List<FinancialAccountViewModel>>(await _financialAccountsBiz.GetAllUnarchivedAsync());

            return View(new PaymentRequestViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> RequestPayment(PaymentRequestViewModel model)
        {
            if (!model.IsProject)
            {
                ModelState.Remove("ProjectId");
                ModelState.Remove("Directions[0].AccountId");
                ModelState.Remove("Directions[0].Amount");
                ModelState.Remove("Directions[0].Description");

                model.Directions.Clear();

                model.Directions = new List<PaymentRequestDirectionViewModel>
                {
                    new PaymentRequestDirectionViewModel
                    {
                        AccountId = model.AccountId,
                        Amount = model.TotalAmount,
                        Description = model.Description
                    }
                };
            }
            else
            {
                ModelState.Remove("AccountId");
                ModelState.Remove("Description");
            }

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                var paymentRequest = _mapper.Map<PaymentRequest>(model);

                paymentRequest.SerialNo = await _paymentRequestsBiz.GetNewSerialNo(null, DateTime.UtcNow.Year, model.DepartmentId);
                paymentRequest.DateCreated = DateTime.UtcNow;
                paymentRequest.CreatedByUserId = user.Id;

                await _paymentRequestsBiz.AddAsync(paymentRequest);

                return RedirectToAction(nameof(Index));
            }

            ViewData["CurrencyId"] = await _currenciesBiz.GetAllUnarchivedAsync();
            ViewData["DepartmentId"] = await _departmentsBiz.GetAllUnarchivedAsync();
            ViewData["ProjectId"] = _mapper.Map<List<ProjectViewModel>>(await _projectsBiz.GetAllUnarchivedAsync());
            ViewData["DirectionId"] = _mapper.Map<List<FinancialAccountViewModel>>(await _financialAccountsBiz.GetAllUnarchivedAsync());

            return View(model);
        }

        public async Task<IActionResult> AddPaymentType(int requestId)
        {
            var paymentRequestVM = _mapper.Map<PaymentRequestViewModel>(await _paymentRequestsBiz.GetByIdAsync(requestId));
            ViewData["PaymentRequestVM"] = paymentRequestVM;

            ViewData["Treasuries"] = _mapper.Map<List<TreasuryViewModel>>(await _treasuriesBiz.GetUnarchivedAsync(paymentRequestVM.DepartmentId));

            ViewData["Banks"] = _mapper.Map<List<BankViewModel>>(await _banksBiz.GetAllUnarchivedAsync());

            return PartialView("_PaymentTypePartial", new PaymentDetailsViewModel { PaymentRequestId = paymentRequestVM.Id });
        }

        public async Task<IActionResult> EditPaymentType(int requestId)
        {
            var paymentRequestVM = _mapper.Map<PaymentRequestViewModel>(await _paymentRequestsBiz.GetByIdAsync(requestId));
            ViewData["PaymentRequestVM"] = paymentRequestVM;

            ViewData["Treasuries"] = _mapper.Map<List<TreasuryViewModel>>(await _treasuriesBiz.GetUnarchivedAsync(paymentRequestVM.DepartmentId));

            ViewData["Banks"] = _mapper.Map<List<BankViewModel>>(await _banksBiz.GetAllUnarchivedAsync());

            return PartialView("_PaymentTypePartial", paymentRequestVM.PaymentDetailsVM);
        }

        [HttpPost]
        public async Task<IActionResult> SavePaymentType(PaymentDetailsViewModel model)
        {
            if (model.Type == ApplicationEnums.PaymentTypeEnum.Cash)
            {
                ModelState.Remove("ReceiptNo");
                ModelState.Remove("IssueDate");
                ModelState.Remove("ReceiveBankId");
                ModelState.Remove("DueDate");
            }
            else if (model.Type == ApplicationEnums.PaymentTypeEnum.Transfer)
            {
                ModelState.Remove("DueDate");
            }
            else if (model.Type == ApplicationEnums.PaymentTypeEnum.Cheque)
            {
                ModelState.Remove("ReceiveBankId");
                ModelState.Remove("ExchangeRate");
            }

            if (ModelState.IsValid)
            {
                var paymentDetails = _mapper.Map<PaymentDetails>(model);
                if (model.Id == 0)
                {
                    var result = await _paymentRequestsBiz.AddPaymentDetails(paymentDetails);

                    return Json(new { result });
                }
                else
                {
                    var result = await _paymentRequestsBiz.EditPaymentDetails(paymentDetails);

                    return Json(new { result });
                }

            }

            return Json(new { result = false });
        }

        public async Task<IActionResult> GetTreasuryInfo(int id)
        {
            var treasury = _mapper.Map<TreasuryViewModel>(await _treasuriesBiz.GetByIdAsync(id));

            return Json(treasury,
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = null,
                    ReferenceHandler = ReferenceHandler.Preserve
                });
        }

        public async Task<IActionResult> GetPaymentDetails(int id)
        {
            var model = _mapper.Map<PaymentRequestViewModel>(await _paymentRequestsBiz.GetByIdAsync(id));

            return PartialView("_PaymentDetailsPartial", model);
        }

        public async Task<IActionResult> ApproveRequest(int requestId)
        {
            var request = await _paymentRequestsBiz.GetByIdAsync(requestId);

            var treasuryVM = _mapper.Map<TreasuryViewModel>(request.PaymentDetails.Treasury);

            await _treasuryTransactionsBiz.AddAsync(new TreasuryTransaction
            {
                Amount = request.TotalAmount,
                CurrencyId = request.CurrencyId,
                ExchangeRate = request.PaymentDetails.ExchangeRate,
                Type = ApplicationEnums.TransactionType.Credit,
                DateCreated = DateTime.UtcNow,
                TreasuryId = request.PaymentDetails.TreasuryId,
                Description = String.Join(", ", request.Directions.Select(d => d.Account.Name)),
                DescriptionAr = String.Join(", ", request.Directions.Select(d => d.Account.NameAr))
            });

            foreach (var direction in request.Directions)
            {
                await _financialAccountTransactionsBiz.AddAsync(new FinancialAccountTransaction
                {
                    Amount = direction.Amount,
                    CurrencyId = request.CurrencyId,
                    ExchangeRate = request.PaymentDetails.ExchangeRate,
                    Type = ApplicationEnums.TransactionType.Debit,
                    DateCreated = DateTime.UtcNow,
                    FinancialAccountId = direction.AccountId,
                    Description = treasuryVM.Description,
                    DescriptionAr = treasuryVM.DescriptionAr
                });
            }

            var result = await _paymentRequestsBiz.ApproveRequestAsync(requestId);
            return Json(result);
        }

        public async Task<IActionResult> RejectRequest(int requestId, string reason)
        {
            var result = await _paymentRequestsBiz.RejectRequestAsync(requestId, reason);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRequests(int[] requestIds)
        {
            foreach (var requestId in requestIds)
            {
                await _paymentRequestsBiz.DeleteAsync(requestId);
            }

            return Json(true);
        }

        public async Task<IActionResult> GetAttachmentDetails(int requestId)
        {
            var model = _mapper.Map<PaymentRequestViewModel>(await _paymentRequestsBiz.GetByIdAsync(requestId));

            return PartialView("_AttachmentsPartial", model);
        }

        [HttpPost]
        public async Task<IActionResult> UploadAttachments(int requestId, IFormFile receiptAttachmentFile, IFormFile cashAttachmentFile, IFormFile transferAttachmentFile)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var request = await _paymentRequestsBiz.GetByIdAsync(requestId);

            bool isFileAttached = false;

            if (receiptAttachmentFile is not null)
            {
                isFileAttached = true;

                string uploadedFileName = UploadFile(receiptAttachmentFile, "Payments");

                Attachment attachment = new()
                {
                    FilePath = uploadedFileName,
                    Title = receiptAttachmentFile.FileName,
                    UploadedById = user.Id
                };

                await _attachmentsBiz.AddAsync(attachment);

                request.ReceiptAttachmentId = attachment.Id;
            }

            if (cashAttachmentFile is not null)
            {
                isFileAttached = true;

                string uploadedFileName = UploadFile(cashAttachmentFile, "Payments");

                Attachment attachment = new()
                {
                    FilePath = uploadedFileName,
                    Title = cashAttachmentFile.FileName,
                    UploadedById = user.Id
                };

                await _attachmentsBiz.AddAsync(attachment);

                request.CashAttachmentId = attachment.Id;
            }

            if (transferAttachmentFile is not null)
            {
                isFileAttached = true;

                string uploadedFileName = UploadFile(transferAttachmentFile, "Payments");

                Attachment attachment = new()
                {
                    FilePath = uploadedFileName,
                    Title = transferAttachmentFile.FileName,
                    UploadedById = user.Id
                };

                await _attachmentsBiz.AddAsync(attachment);

                request.TransferAttachmentId = attachment.Id;
            }

            if (isFileAttached)
            {
                var result = await _paymentRequestsBiz.UpdateAsync(request);

                return Json(result);
            }

            return Json(true);
        }
    }
}

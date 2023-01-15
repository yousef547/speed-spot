using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using portal.speedspot.BizLayer.BizMethods;
using portal.speedspot.BizLayer.IdentityModule;
using portal.speedspot.DALRepositories.Migrations;
using portal.speedspot.Models.Concretes;
using portal.speedspot.Models.Constants;
using portal.speedspot.Models.ViewModels;
using portal.speedspot.WebUI.Controllers.Infrastructure;
using portal.speedspot.WebUI.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using static portal.speedspot.Models.Constants.ApplicationEnums;
using static portal.speedspot.Models.Constants.Permissions;

namespace portal.speedspot.WebUI.Controllers
{
    public class CompanyProfileController : BaseController
    {
        private readonly DepartmentDocumentBiz _DepartmentDocumentBiz;
        private readonly DepartmentBanksBiz _DepartmentBanksBiz;
        private readonly CompanyDataBiz _companyDataBiz;
        private readonly DepartmentsBiz _departmentsBiz;
        private readonly BanksBiz _banksBiz;
        private readonly CurrenciesBiz _currenciesBiz;
        private readonly AttachmentsBiz _AttachmentsBiz;
        private readonly FavouritesBiz _favouritesBiz;
        private readonly FavouriteTypesBiz _favouriteTypesBiz;
        private readonly DepartmentSettingsBiz _departmentSettingsBiz;
        private readonly FinancialAccountsBiz _financialAccountsBiz;

        public CompanyProfileController(CompanyDataBiz companyDataBiz,
            DepartmentDocumentBiz DepartmentDocumentBiz,
            DepartmentBanksBiz DepartmentBanksBiz,
            FavouriteTypesBiz favouriteTypesBiz,
             FavouritesBiz favouritesBiz,
        DepartmentsBiz departmentsBiz,
            BanksBiz banksBiz,
            AttachmentsBiz attachmentsBiz,
            CurrenciesBiz currenciesBiz,
            IMapper mapper,
            IWebHostEnvironment hostEnvironment,
            ApplicationUserManager userManager,
            LocalizationService localizationService,
            DepartmentSettingsBiz departmentSettingsBiz,
            FinancialAccountsBiz financialAccountsBiz) : base(mapper, hostEnvironment, userManager, localizationService)
        {
            _favouritesBiz = favouritesBiz;
            _favouriteTypesBiz = favouriteTypesBiz;
            _AttachmentsBiz = attachmentsBiz;
            _DepartmentDocumentBiz = DepartmentDocumentBiz;
            _banksBiz = banksBiz;
            _currenciesBiz = currenciesBiz;
            _companyDataBiz = companyDataBiz;
            _departmentsBiz = departmentsBiz;
            _DepartmentBanksBiz = DepartmentBanksBiz;
            _departmentSettingsBiz = departmentSettingsBiz;
            _financialAccountsBiz = financialAccountsBiz;
        }

        [Authorize(Permissions.CompanyProfile.View)]
        public async Task<IActionResult> Index()
        {
            var model = _mapper.Map<CompanyDataViewModel>(await _companyDataBiz.GetAsync());

            ViewBag.Users = await _userManager.GetAllUsersAsync();
            return View(model);
        }

        [Authorize(Permissions.CompanyProfile.Edit)]
        public async Task<IActionResult> Edit()
        {
            var model = _mapper.Map<CompanyDataViewModel>(await _companyDataBiz.GetAsync());
            return View(model);
        }

        [Authorize(Permissions.CompanyProfile.Edit)]
        [HttpPost]
        public async Task<IActionResult> Edit(CompanyDataViewModel model)
        {
            if (model.LogoFile == null && model.ImagePath == null)
            {
                ModelState.AddModelError(nameof(model.LogoFile), string.Format(_localizationService["RequiredField"].Value, _localizationService["LogoFile"].Value));
            }

            if (ModelState.IsValid)
            {
                if (model.LogoFile != null)
                {
                    string uniqueFileName = UploadFile(model.LogoFile, "Company");
                    model.ImagePath = uniqueFileName;
                }

                var entity = _mapper.Map<CompanyData>(model);

                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                await _companyDataBiz.UpdateAsync(entity, user.Id);

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpPost]
        [Authorize(Permissions.Departments.Create)]
        public async Task<IActionResult> CreateDepartment(DepartmentViewModel model)
        {
            bool result = false;
            if (ModelState.IsValid)
            {
                var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
                string uniqueFileName = UploadFile(model.ImageFile, "Departments");
                model.Image = uniqueFileName;
                var department = _mapper.Map<Department>(model);
                result = await _departmentsBiz.AddAsync(department, userId);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return Json(result);
        }

        [Authorize(Permissions.Departments.View)]
        public async Task<IActionResult> ShowDepartments()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var favourityType = await _favouriteTypesBiz.GetByEnumValue((int)FavouriteTypeEnum.Department);
            List<Department> Department = (await _departmentsBiz.GetAllAsync()).ToList();
            List<DepartmentViewModel> model = _mapper.Map<List<DepartmentViewModel>>(Department);
            foreach (var item in model)
            {
                item.IsFavourite = await _favouritesBiz.IsFavourite(favourityType.Id, user.Id, item.Id);
            }
            return PartialView("_ShowDepartmentsPartial", model);
        }

        [Authorize(Permissions.Departments.Edit)]
        [HttpGet]
        public async Task<IActionResult> EditDepartment(int id)
        {
            var department = await _departmentsBiz.GetByIdAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<DepartmentViewModel>(department);

            ViewBag.Users = await _userManager.GetAllUsersAsync();
            return PartialView("_EditDepartmentPartial", model);
        }

        [Authorize(Permissions.Departments.Edit)]
        [HttpPost]
        public async Task<IActionResult> EditDepartment(DepartmentViewModel model)
        {
            bool result = false;
            if (ModelState.IsValid)
            {
                var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
                if (model.ImageFile != null)
                {
                    if (model.Image != null)
                    {
                        DeleteFile("Departments", model.Image);

                    }
                    string uniqueFileName = UploadFile(model.ImageFile, "Departments");
                    model.Image = uniqueFileName;
                }
                var department = _mapper.Map<Department>(model);
                result = await _departmentsBiz.UpdateAsync(department, userId);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return Json(result);
        }

        [Authorize(Permissions.Departments.Delete)]
        public async Task<IActionResult> Archive(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            var result = await _departmentsBiz.ArchiveAsync(id, userId);
            return Json(result);
        }

        [Authorize(Permissions.Departments.Delete)]
        public async Task<IActionResult> Unarchive(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            var result = await _departmentsBiz.UnarchiveAsync(id, userId);

            return Json(result);
        }

        [Authorize(Permissions.Departments.Details)]
        public async Task<IActionResult> DepartmentDetails(int id, bool? isActive)
        {
            var department = _mapper.Map<DepartmentViewModel>(await _departmentsBiz.GetByIdAsync(id));
            if (department is null)
            {
                return NotFound();
            }
            ViewBag.isActive = isActive;
            ViewData["BankId"] = await _banksBiz.GetAllUnarchivedAsync();
            ViewData["BranchId"] = new List<BankBranch>();
            ViewData["CurrencyId"] = await _currenciesBiz.GetAllUnarchivedAsync();
            ViewData["CompanyData"] = _mapper.Map<CompanyDataViewModel>(await _companyDataBiz.GetAsync());
            return View(department);
        }

        public async Task<IActionResult> GetBankBranches(int bankId)
        {
            var branches = await _banksBiz.GetBranchesAsync(bankId);

            ViewData["BranchId"] = branches;
            return PartialView("_BankBranchesPartial");
        }

        public async Task<IActionResult> GetBranchDetails(int branchId)
        {
            var branch = await _banksBiz.GetBranchAsync(branchId);

            return Json(branch, new JsonSerializerOptions
            {
                PropertyNamingPolicy = null
            });
        }

        public class CurrenciesModel
        {
            public IList<DepartmentBankCurrencyViewModel> Currencies { get; set; }

            public CurrenciesModel()
            {
                if (Currencies == null) Currencies = new List<DepartmentBankCurrencyViewModel>();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCurrency(CurrenciesModel model)
        {
            model.Currencies.Add(new DepartmentBankCurrencyViewModel());

            ViewData["CurrencyId"] = await _currenciesBiz.GetAllUnarchivedAsync();
            return PartialView("_CurrenciesPartial", model.Currencies);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCurrency(CurrenciesModel model, int index)
        {
            model.Currencies.RemoveAt(index);

            ViewData["CurrencyId"] = await _currenciesBiz.GetAllUnarchivedAsync();
            return PartialView("_CurrenciesPartial", model.Currencies);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBeneficiaryInfo(BeneficiaryInfo model)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            bool result = await _departmentsBiz.UpdateBeneficiaryInfoAsync(model.Id, model.BeneficiaryName, model.BeneficiaryAddress, userId);

            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddBank(DepartmentBankViewModel model)
        {
            var DepartmentBank = _mapper.Map<DepartmentBank>(model);

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            await _DepartmentBanksBiz.AddAsync(DepartmentBank, user.Id);

            var DepartmentBanks = _mapper.Map<List<DepartmentBankViewModel>>(await _DepartmentBanksBiz.GetByDepartmentIdAsync(model.DepartmentId));

            return PartialView("_BanksPartial", DepartmentBanks);
        }

        public async Task<IActionResult> GetBankCurrencyDetails(int currencyId)
        {
            var currency = await _DepartmentBanksBiz.GetCurrencyDetails(currencyId);

            return Json(currency, new JsonSerializerOptions
            {
                PropertyNamingPolicy = null
            });
        }

        public async Task<IActionResult> DeleteBank(int id, int bankId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            await _DepartmentBanksBiz.DeleteAsync(bankId, user.Id);

            var departmentBanks = _mapper.Map<List<DepartmentBankViewModel>>(await _DepartmentBanksBiz.GetByDepartmentIdAsync(id));

            return PartialView("_BanksPartial", departmentBanks);
        }

        public async Task<IActionResult> GetBankCurrencies(int bankId)
        {
            var currencies = _mapper.Map<List<DepartmentBankCurrencyViewModel>>(await _DepartmentBanksBiz.GetCurrencies(bankId));

            ViewData["CurrencyId"] = await _currenciesBiz.GetAllUnarchivedAsync();
            return PartialView("_CurrenciesPartial", currencies);
        }

        public async Task<IActionResult> EditBank(int bankId)
        {
            var bank = _mapper.Map<DepartmentBankViewModel>(await _DepartmentBanksBiz.GetByIdAsync(bankId));

            return Json(bank, new JsonSerializerOptions
            {
                PropertyNamingPolicy = null
            });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBank(DepartmentBankViewModel model)
        {
            var departmentBank = _mapper.Map<DepartmentBank>(model);

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            await _DepartmentBanksBiz.UpdateAsync(departmentBank, user.Id);

            var departmentBanks = _mapper.Map<List<DepartmentBankViewModel>>(await _DepartmentBanksBiz.GetByDepartmentIdAsync(model.DepartmentId));

            return PartialView("_BanksPartial", departmentBanks);
        }

        public async Task<IActionResult> GetDepartmentDocuments(int departmentId)
        {
            var documents = _mapper.Map<List<DepartmentDocumentViewModel>>(await _DepartmentDocumentBiz.GetAllDocumentByDepartmentId(departmentId));
            if (documents is null)
            {
                return NotFound();
            }
            return PartialView("_ShowDepartmentDocumentasPartial", documents);
        }

        public async Task<IActionResult> GetDepartmentDocumentsInputs(int departmentId)
        {
            var documents = _mapper.Map<List<DepartmentDocumentViewModel>>(await _DepartmentDocumentBiz.GetAllDocumentByDepartmentId(departmentId));
            if (documents is null)
            {
                return NotFound();
            }
            return PartialView("_AddNewDocumentPartial", documents);
        }

        public class DocumentsModel
        {
            public IList<DepartmentDocumentViewModel> Documents { get; set; }

            public DocumentsModel()
            {
                if (Documents == null) Documents = new List<DepartmentDocumentViewModel>();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddDocument(DocumentsModel model)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name));
            model.Documents.Add(new DepartmentDocumentViewModel());
            for (int i = 0; i < model.Documents.Count; i++)
            {
                if (model.Documents[i].DocumentFile != null)
                {
                    string uniqueFileName = UploadFile(model.Documents[i].DocumentFile, "documents");
                    var attachment = new Attachment { FilePath = uniqueFileName, Title = model.Documents[i].DocumentFile.FileName, UploadedById = userId.Id };
                    await _AttachmentsBiz.AddAsync(attachment);
                    model.Documents[i].AttachmentId = attachment.Id;
                }
            }
            return PartialView("_AddNewDocumentPartial", model.Documents);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDocument(DocumentsModel model, int index)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name));
            model.Documents.RemoveAt(index);
            for (int i = 0; i < model.Documents.Count; i++)
            {
                if (model.Documents[i].DocumentFile != null)
                {
                    string uniqueFileName = UploadFile(model.Documents[i].DocumentFile, "documents");
                    var attachment = new Attachment { FilePath = uniqueFileName, Title = model.Documents[i].DocumentFile.FileName, UploadedById = userId.Id };
                    await _AttachmentsBiz.AddAsync(attachment);
                    model.Documents[i].AttachmentId = attachment.Id;
                }
            }
            return PartialView("_AddNewDocumentPartial", model.Documents);
        }

        public async Task<IActionResult> SaveDocuments(int departmentId, DocumentsModel model)
        {
            bool result = false;
            await _DepartmentDocumentBiz.DeleteAllById(departmentId);
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name));
            var documents = new List<DepartmentDocumentViewModel>();
            if (model.Documents.Count != 0)
            {
                for (int i = 0; i < model.Documents.Count; i++)
                {
                    if (model.Documents[i].DocumentFile != null)
                    {
                        string uniqueFileName = UploadFile(model.Documents[i].DocumentFile, "documents");
                        var attachment = new Attachment { FilePath = uniqueFileName, Title = model.Documents[i].DocumentFile.FileName, UploadedById = userId.Id };
                        await _AttachmentsBiz.AddAsync(attachment);
                        model.Documents[i].AttachmentId = attachment.Id;
                    }

                    if (model.Documents[i].AttachmentId == 0 && model.Documents[i].DocumentFile == null)
                    {
                        return Json(result);
                    }

                    var document = new DepartmentDocumentViewModel()
                    {
                        Title = model.Documents[i].Title,
                        Number = model.Documents[i].Number,
                        ExpiryDate = model.Documents[i].ExpiryDate,
                        IssueDate = model.Documents[i].IssueDate,
                        AttachmentId = model.Documents[i].AttachmentId,
                        DepartmentId = departmentId,
                    };
                    documents.Add(document);
                }
                var mabDocuments = _mapper.Map<List<DepartmentDocument>>(documents);
                result = await _DepartmentDocumentBiz.AddAllDocumentsAsync(mabDocuments, userId.Id);
            }
            return Json(result);
        }

        public async Task<IActionResult> ArchiveDocument(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            var result = await _DepartmentDocumentBiz.ArchiveAsync(id, userId);
            return Json(result);
        }

        public async Task<IActionResult> ToggleFavourite(int? itemId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var favourityType = await _favouriteTypesBiz.GetByEnumValue((int)FavouriteTypeEnum.Department);
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

        public async Task<IActionResult> DepartmentSettings(int departmentId)
        {
            var settings = await _departmentSettingsBiz.GetByDepartmentIdAsync(departmentId);

            DepartmentSettingsViewModel settingsVM;
            if (settings is null)
            {
                settings = new DepartmentSettings
                {
                    DepartmentId = departmentId
                };
                await _departmentSettingsBiz.AddAsync(settings);
                settings = await _departmentSettingsBiz.GetByDepartmentIdAsync(departmentId);
            }
            settingsVM = _mapper.Map<DepartmentSettingsViewModel>(settings);

            ViewData["FinancialAccounts"] = _mapper.Map<List<FinancialAccountViewModel>>(
                await _financialAccountsBiz.GetAllByDepartmentId(departmentId));

            return View(settingsVM);
        }

        [HttpPost]
        public async Task<IActionResult> DepartmentSettings(DepartmentSettingsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var settings = _mapper.Map<DepartmentSettings>(model);
                await _departmentSettingsBiz.UpdateAsync(settings);

                return RedirectToAction(nameof(Index));
            }

            model.DepartmentVM = _mapper.Map<DepartmentViewModel>(await _departmentsBiz.GetByIdAsync(model.DepartmentId));
            ViewData["FinancialAccounts"] = _mapper.Map<List<FinancialAccountViewModel>>(
                await _financialAccountsBiz.GetAllByDepartmentId(model.DepartmentId));

            return View(model);
        }
    }

}

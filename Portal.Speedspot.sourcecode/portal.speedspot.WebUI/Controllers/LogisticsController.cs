using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using portal.speedspot.BizLayer.BizMethods;
using portal.speedspot.BizLayer.IdentityModule;
using portal.speedspot.Models.Concretes;
using portal.speedspot.Models.Constants;
using portal.speedspot.Models.ViewModels;
using portal.speedspot.WebUI.Controllers.Infrastructure;
using portal.speedspot.WebUI.Helpers;

using static portal.speedspot.Models.Constants.ApplicationEnums;

namespace portal.speedspot.WebUI.Controllers
{
    public class LogisticsController : BaseController
    {
        private readonly LogisticsBiz _logisticsBiz;
        private readonly CitiesBiz _citiesBiz;
        private readonly AttachmentsBiz _attachmentsBiz;
        private readonly ContactsBiz _contactsBiz;
        private readonly PartnerEmployeesBiz _partnerEmployeesBiz;
        private readonly FavouriteTypesBiz _favouriteTypesBiz;
        private readonly FavouritesBiz _favouritesBiz;
        private readonly ActivityTypesBiz _activityTypesBiz;
        private readonly UserDepartmentsBiz _userDepartmentsBiz;
        private readonly OrganizationTypesBiz _organizationTypesBiz;
        private readonly DepartmentsBiz _departmentsBiz;
        private readonly CountriesBiz _countriesBiz;
        private readonly ContactTypesBiz _contactTypesBiz;
        private readonly BanksBiz _banksBiz;
        private readonly CurrenciesBiz _currenciesBiz;
        private readonly LogisticBanksBiz _logisticBanksBiz;

        public LogisticsController(
            LogisticsBiz logisticsBiz,
            CitiesBiz citiesBiz,
            AttachmentsBiz attachmentsBiz,
            ContactsBiz contactsBiz,
            PartnerEmployeesBiz partnerEmployeesBiz,
            FavouriteTypesBiz favouriteTypesBiz,
            FavouritesBiz favoritesBiz,
            ActivityTypesBiz activityTypesBiz,
            UserDepartmentsBiz userDepartmentsBiz,
            OrganizationTypesBiz organizationTypesBiz,
            DepartmentsBiz departmentsBiz,
            CountriesBiz countriesBiz,
            ContactTypesBiz contactTypesBiz,
            BanksBiz banksBiz,
            CurrenciesBiz currenciesBiz,
            LogisticBanksBiz logisticBanksBiz,
            IMapper mapper,
            IWebHostEnvironment hostEnvironment,
            ApplicationUserManager userManager,
            LocalizationService localizationService) : base(mapper, hostEnvironment, userManager, localizationService)
        {
            _logisticsBiz = logisticsBiz;
            _citiesBiz = citiesBiz;
            _attachmentsBiz = attachmentsBiz;
            _contactsBiz = contactsBiz;
            _partnerEmployeesBiz = partnerEmployeesBiz;
            _favouriteTypesBiz = favouriteTypesBiz;
            _favouritesBiz = favoritesBiz;
            _activityTypesBiz = activityTypesBiz;
            _userDepartmentsBiz = userDepartmentsBiz;
            _organizationTypesBiz = organizationTypesBiz;
            _departmentsBiz = departmentsBiz;
            _countriesBiz = countriesBiz;
            _contactTypesBiz = contactTypesBiz;
            _banksBiz = banksBiz;
            _currenciesBiz = currenciesBiz;
            _logisticBanksBiz = logisticBanksBiz;
        }

        public async Task<IActionResult> GetCities(int countryId, int? cityId)
        {
            var cities = await _citiesBiz.GetByCountryId(countryId);

            CityListViewModel model = new()
            {
                Cities = cities,
                CityId = cityId
            };
            return PartialView("_CitiesPartial", model);
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            string uniqueFileName = UploadFile(file, "Logistics");

            var attachment = new Attachment
            {
                FilePath = uniqueFileName,
                Title = file.FileName,
                UploadedById = user.Id
            };
            await _attachmentsBiz.AddAsync(attachment);

            return Json(new
            {
                FilePath = uniqueFileName,
                AttachmentId = attachment.Id
            });
        }

        public class AddContactModel
        {
            public List<LogisticContactViewModel> Contacts { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> AddContact(AddContactModel model)
        {
            model.Contacts.Add(new LogisticContactViewModel());

            ViewData["CountryId"] = await _countriesBiz.GetAllUnarchivedAsync();
            ViewData["ContactTypeId"] = await _contactTypesBiz.GetAllUnarchivedAsync();
            return PartialView("_ContactsPartial", model.Contacts);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteContact(int index, AddContactModel model)
        {
            model.Contacts.RemoveAt(index);

            ViewData["CountryId"] = await _countriesBiz.GetAllUnarchivedAsync();
            ViewData["ContactTypeId"] = await _contactTypesBiz.GetAllUnarchivedAsync();
            return PartialView("_ContactsPartial", model.Contacts);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDbContact(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var result = await _contactsBiz.DeleteAsync(id, user.Id);

            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDbAttachment(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var result = await _attachmentsBiz.DeleteAsync(id, user.Id);

            return Json(result);
        }

        public class AddEmployeeModel
        {
            public List<LogisticEmployeeViewModel> Employees { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(AddEmployeeModel model)
        {
            model.Employees.Add(new LogisticEmployeeViewModel());

            ViewData["CountryId"] = await _countriesBiz.GetAllUnarchivedAsync();
            return PartialView("_EmployeesPartial", model.Employees);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEmployee(int index, AddEmployeeModel model)
        {
            model.Employees.RemoveAt(index);

            ViewData["CountryId"] = await _countriesBiz.GetAllUnarchivedAsync();
            return PartialView("_EmployeesPartial", model.Employees);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDbEmployee(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var result = await _partnerEmployeesBiz.DeleteAsync(id, user.Id);

            return Json(result);
        }

        public async Task<IActionResult> ToggleFavourite(int? itemId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var favourityType = await _favouriteTypesBiz.GetByEnumValue((int)FavouriteTypeEnum.Logistics);
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
            public IList<LogisticBankCurrencyViewModel> Currencies { get; set; }

            public CurrenciesModel()
            {
                if (Currencies == null) Currencies = new List<LogisticBankCurrencyViewModel>();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCurrency(CurrenciesModel model)
        {
            model.Currencies.Add(new LogisticBankCurrencyViewModel());

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

        public async Task<IActionResult> ClearCurrencies()
        {
            ViewData["CurrencyId"] = await _currenciesBiz.GetAllUnarchivedAsync();
            return PartialView("_CurrenciesPartial", new List<LogisticBankCurrencyViewModel>());
        }

        [HttpPost]
        public async Task<IActionResult> AddBank(LogisticBankViewModel model)
        {
            var logisticBank = _mapper.Map<LogisticBank>(model);

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            await _logisticBanksBiz.AddAsync(logisticBank, user.Id);

            var logisticBanks = _mapper.Map<List<LogisticBankViewModel>>(await _logisticBanksBiz.GetByLogisticIdAsync(model.LogisticId));

            return PartialView("_BanksPartial", logisticBanks);
        }

        public async Task<IActionResult> DeleteBank(int id, int bankId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            await _logisticBanksBiz.DeleteAsync(bankId, user.Id);

            var logisticBanks = _mapper.Map<List<LogisticBankViewModel>>(await _logisticBanksBiz.GetByLogisticIdAsync(id));

            return PartialView("_BanksPartial", logisticBanks);
        }

        public async Task<IActionResult> EditBank(int bankId)
        {
            var bank = _mapper.Map<LogisticBankViewModel>(await _logisticBanksBiz.GetByIdAsync(bankId));

            return Json(bank, new JsonSerializerOptions
            {
                PropertyNamingPolicy = null
            });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBank(LogisticBankViewModel model)
        {
            var logisticBank = _mapper.Map<LogisticBank>(model);

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            await _logisticBanksBiz.UpdateAsync(logisticBank, user.Id);

            var customerBanks = _mapper.Map<List<LogisticBankViewModel>>(await _logisticBanksBiz.GetByLogisticIdAsync(model.LogisticId));

            return PartialView("_BanksPartial", customerBanks);
        }

        public async Task<IActionResult> GetBankCurrencies(int bankId)
        {
            var currencies = _mapper.Map<List<LogisticBankCurrencyViewModel>>(await _logisticBanksBiz.GetCurrencies(bankId));

            ViewData["CurrencyId"] = await _currenciesBiz.GetAllUnarchivedAsync();
            return PartialView("_CurrenciesPartial", currencies);
        }

        public async Task<IActionResult> GetBankCurrencyDetails(int currencyId)
        {
            var currency = await _logisticBanksBiz.GetCurrencyDetails(currencyId);

            return Json(currency, new JsonSerializerOptions
            {
                PropertyNamingPolicy = null
            });
        }

        public class ProcessDataViewModel
        {
            public bool IsValid { get; set; }
            public Logistic Logistic { get; set; }
            public LogisticViewModel ViewModel { get; set; }
        }

        public async Task<ProcessDataViewModel> ProcessData(LogisticViewModel model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            bool isValid = true;

            //Process Departments
            foreach (int departmentId in model.DepartmentIds)
            {
                model.DepartmentVMs.Add(new LogisticDepartmentViewModel
                {
                    LogisticId = model.Id,
                    DepartmentId = departmentId
                });
            }

            //Process Contacts
            var deletedContacts = new List<LogisticContactViewModel>();
            for (int i = 0; i < model.ContactVMs.Count; i++)
            {
                var contact = model.ContactVMs[i];
                if (contact.ContactVM.TypeId == null || contact.ContactVM.PhoneCodeId == null || contact.ContactVM.Number == null)
                {
                    deletedContacts.Add(contact);
                }
            }
            foreach (var contact in deletedContacts)
            {
                model.ContactVMs.Remove(contact);
            }

            //Process Employees
            var deletedEmployees = new List<LogisticEmployeeViewModel>();
            for (int i = 0; i < model.EmployeeVMs.Count; i++)
            {
                var employee = model.EmployeeVMs[i];
                if (employee.EmployeeVM.Department == null || employee.EmployeeVM.Name == null
                    || employee.EmployeeVM.Phone == null || employee.EmployeeVM.PhoneCodeId == 0 || employee.EmployeeVM.Position == null)
                {
                    deletedEmployees.Add(employee);
                }
            }
            foreach (var employee in deletedEmployees)
            {
                model.EmployeeVMs.Remove(employee);
            }

            //Process Parent Company
            if (model.IsSubCompany)
            {
                if (model.ParentId == null)
                {
                    ModelState.AddModelError("ParentId", string.Format(_localizationService["RequiredField"].Value, _localizationService["ParentCompany"].Value));
                    isValid = false;
                }
            }
            else
            {
                model.ParentId = null;
            }

            var logistic = _mapper.Map<Logistic>(model);


            //Process Attachments
            if (model.Attachments != null && model.Attachments.Count > 0)
            {
                if (logistic.Files == null)
                    logistic.Files = new List<LogisticFile>();

                foreach (var file in model.Attachments)
                {
                    string uniqueFileName = UploadFile(file, "Logistics");

                    var attachment = new Attachment
                    {
                        FilePath = uniqueFileName,
                        Title = file.FileName,
                        UploadedById = user.Id
                    };
                    await _attachmentsBiz.AddAsync(attachment);

                    logistic.Files.Add(new LogisticFile
                    {
                        AttachmentId = attachment.Id
                    });

                    model.FileVMs.Add(new LogisticFileViewModel
                    {
                        AttachmentPath = uniqueFileName,
                        AttachmentId = attachment.Id,
                        LogisticId = logistic.Id,
                        AttachmentTitle = file.FileName
                    });
                }
            }

            //Logo
            if (model.LogoFile != null)
            {
                string uniqueFileName = UploadFile(model.LogoFile, "Logistics");
                if (model.LogoAttachmentId != 0)
                {
                    logistic.LogoAttachment = new Attachment
                    {
                        FilePath = uniqueFileName,
                        Title = model.LogoFile.FileName,
                        Id = (int)logistic.LogoAttachmentId,
                        UploadedById = user.Id
                    };
                }
                else
                {
                    logistic.LogoAttachment = new Attachment
                    {
                        FilePath = uniqueFileName,
                        Title = model.LogoFile.FileName,
                        UploadedById = user.Id
                    };
                }
            }
            else
            {
                if (model.LogoAttachmentId == 0)
                {
                    ModelState.AddModelError("LogoFile", string.Format(_localizationService["RequiredField"].Value, _localizationService["LogoFile"].Value));
                    isValid = false;
                }
            }

            //Legal Info
            if (model.LegalInfoVM != null && model.LegalInfoVM.File != null)
            {
                string uniqueFileName = UploadFile(model.LegalInfoVM.File, "Logistics");
                if (model.LegalInfoVM.AttachmentId != null)
                {
                    logistic.LegalInfo.Attachment = new Attachment
                    {
                        FilePath = uniqueFileName,
                        Title = model.LegalInfoVM.File.FileName,
                        Id = (int)logistic.LegalInfo.AttachmentId,
                        UploadedById = user.Id
                    };
                }
                else
                {
                    logistic.LegalInfo.Attachment = new Attachment
                    {
                        FilePath = uniqueFileName,
                        Title = model.LegalInfoVM.File.FileName,
                        UploadedById = user.Id
                    };
                }
            }

            return new ProcessDataViewModel
            {
                IsValid = isValid,
                Logistic = logistic,
                ViewModel = model
            };
        }


        [Authorize(Permissions.Logistics.View)]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var favourityType = await _favouriteTypesBiz.GetByEnumValue((int)FavouriteTypeEnum.Logistics);
            var isFavourite = await _favouritesBiz.IsFavourite(favourityType.Id, user.Id, null);
            ViewData["IsFavourite"] = isFavourite;
            return View();
        }

        public async Task<IActionResult> GetLogistics()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userDepartments = await _userDepartmentsBiz.GetByUserAsync(user.Id);
            List<LogisticViewModel> logisticVMs;
            if (await _userManager.IsSuperAdmin(user))
                logisticVMs = _mapper.Map<List<LogisticViewModel>>(await _logisticsBiz.GetParents());
            else
                logisticVMs = _mapper.Map<List<LogisticViewModel>>(await _logisticsBiz.GetParentsByUserAsync(user.Id, user.DepartmentId,
                     userDepartments.Select(x => x.DepartmentId).ToList()));
            var favourityType = await _favouriteTypesBiz.GetByEnumValue((int)FavouriteTypeEnum.Logistics);
            foreach (var logisticVM in logisticVMs)
            {
                logisticVM.IsFavourite = await _favouritesBiz.IsFavourite(favourityType.Id, user.Id, logisticVM.Id);
            }
            return PartialView("_ShowLogisticsPartial", logisticVMs);
        }

        public async Task<IActionResult> GetChildLogistics(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var logisticVMs = _mapper.Map<List<LogisticViewModel>>(await _logisticsBiz.GetChildsAsync(id));

            var favourityType = await _favouriteTypesBiz.GetByEnumValue((int)FavouriteTypeEnum.Logistics);
            foreach (var logisticVM in logisticVMs)
            {
                logisticVM.IsFavourite = await _favouritesBiz.IsFavourite(favourityType.Id, user.Id, logisticVM.Id);
            }

            return PartialView("_ChildsPartial", logisticVMs);
        }

        [Authorize(Permissions.Logistics.Create)]
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userDepartments = await _userDepartmentsBiz.GetByUserAsync(user.Id);
            bool userIsSuperAdmin = await _userManager.IsSuperAdmin(user);

            ViewData["ActivityTypeId"] = await _activityTypesBiz.GetAllUnarchivedAsync();
            ViewData["OrganizationTypeId"] = await _organizationTypesBiz.GetAllUnarchivedAsync();

            if (userIsSuperAdmin)
                ViewData["DepartmentId"] = await _departmentsBiz.GetAllUnarchivedAsync();
            else
                ViewData["DepartmentId"] = await _departmentsBiz.GetAllUnarchivedByUserAsync(user.DepartmentId,
                    userDepartments.Select(x => x.DepartmentId).ToList());

            ViewData["CountryId"] = await _countriesBiz.GetAllUnarchivedAsync();
            ViewData["ContactTypeId"] = await _contactTypesBiz.GetAllUnarchivedAsync();

            if (userIsSuperAdmin)
                ViewData["ParentId"] = await _logisticsBiz.GetAllExceptAsync();
            else
                ViewData["ParentId"] = await _logisticsBiz.GetAllExceptByUserAsync(user.Id, user.DepartmentId,
                    userDepartments.Select(x => x.DepartmentId).ToList());

            return View(new LogisticViewModel());
        }

        [HttpPost]
        [Authorize(Permissions.Logistics.Create)]
        public async Task<IActionResult> Create(LogisticViewModel model, string actionType = "Add")
        {
            ProcessDataViewModel processResult = new()
            {
                ViewModel = model
            };

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (ModelState.IsValid)
            {
                processResult = await ProcessData(model);
                if (processResult.IsValid)
                {
                    processResult.Logistic.CreatedById = user.Id;
                    var result = await _logisticsBiz.AddAsync(processResult.Logistic, user.Id);
                    if (result)
                    {
                        switch (actionType)
                        {
                            case "Add":
                                return RedirectToAction(nameof(Index));
                            case "Save":
                                return RedirectToAction(nameof(Details), new { id = processResult.Logistic.Id });
                        }
                    }
                }
            }

            var userDepartments = await _userDepartmentsBiz.GetByUserAsync(user.Id);
            bool userIsSuperAdmin = await _userManager.IsSuperAdmin(user);

            ViewData["ActivityTypeId"] = await _activityTypesBiz.GetAllUnarchivedAsync();
            ViewData["OrganizationTypeId"] = await _organizationTypesBiz.GetAllUnarchivedAsync();

            if (userIsSuperAdmin)
                ViewData["DepartmentId"] = await _departmentsBiz.GetAllUnarchivedAsync();
            else
                ViewData["DepartmentId"] = await _departmentsBiz.GetAllUnarchivedByUserAsync(user.DepartmentId,
                    userDepartments.Select(x => x.DepartmentId).ToList());

            ViewData["CountryId"] = await _countriesBiz.GetAllUnarchivedAsync();
            ViewData["ContactTypeId"] = await _contactTypesBiz.GetAllUnarchivedAsync();

            if (userIsSuperAdmin)
                ViewData["ParentId"] = await _logisticsBiz.GetAllExceptAsync();
            else
                ViewData["ParentId"] = await _logisticsBiz.GetAllExceptByUserAsync(user.Id, user.DepartmentId,
                    userDepartments.Select(x => x.DepartmentId).ToList());

            return View(model);
        }

        [Authorize(Permissions.Logistics.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var model = _mapper.Map<LogisticViewModel>(await _logisticsBiz.GetByIdAsync(id));

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userDepartments = await _userDepartmentsBiz.GetByUserAsync(user.Id);
            bool userIsSuperAdmin = await _userManager.IsSuperAdmin(user);

            ViewData["ActivityTypeId"] = await _activityTypesBiz.GetAllUnarchivedAsync();
            ViewData["OrganizationTypeId"] = await _organizationTypesBiz.GetAllUnarchivedAsync();

            if (userIsSuperAdmin)
                ViewData["DepartmentId"] = await _departmentsBiz.GetAllUnarchivedAsync();
            else
                ViewData["DepartmentId"] = await _departmentsBiz.GetAllUnarchivedByUserAsync(user.DepartmentId,
                    userDepartments.Select(x => x.DepartmentId).ToList());

            ViewData["CountryId"] = await _countriesBiz.GetAllUnarchivedAsync();
            ViewData["ContactTypeId"] = await _contactTypesBiz.GetAllUnarchivedAsync();

            if (userIsSuperAdmin)
                ViewData["ParentId"] = await _logisticsBiz.GetAllExceptAsync(id);
            else
                ViewData["ParentId"] = await _logisticsBiz.GetAllExceptByUserAsync(user.Id, user.DepartmentId,
                    userDepartments.Select(x => x.DepartmentId).ToList(), id);

            return View(model);
        }

        [HttpPost]
        [Authorize(Permissions.Logistics.Edit)]
        public async Task<IActionResult> Edit(LogisticViewModel model)
        {
            ProcessDataViewModel processResult = new()
            {
                ViewModel = model
            };

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (ModelState.IsValid)
            {
                processResult = await ProcessData(model);
                if (processResult.IsValid)
                {
                    _logisticsBiz.DeleteCompetitorDepartments(model.Id);
                    var result = await _logisticsBiz.UpdateAsync(processResult.Logistic, user.Id);
                    if (result)
                    {
                        return RedirectToAction(nameof(Details), new
                        {
                            id = processResult.Logistic.Id
                        });
                    }
                }
            }

            var userDepartments = await _userDepartmentsBiz.GetByUserAsync(user.Id);
            bool userIsSuperAdmin = await _userManager.IsSuperAdmin(user);

            ViewData["ActivityTypeId"] = await _activityTypesBiz.GetAllUnarchivedAsync();
            ViewData["OrganizationTypeId"] = await _organizationTypesBiz.GetAllUnarchivedAsync();

            if (userIsSuperAdmin)
                ViewData["DepartmentId"] = await _departmentsBiz.GetAllUnarchivedAsync();
            else
                ViewData["DepartmentId"] = await _departmentsBiz.GetAllUnarchivedByUserAsync(user.DepartmentId,
                    userDepartments.Select(x => x.DepartmentId).ToList());

            ViewData["CountryId"] = await _countriesBiz.GetAllUnarchivedAsync();
            ViewData["ContactTypeId"] = await _contactTypesBiz.GetAllUnarchivedAsync();

            if (userIsSuperAdmin)
                ViewData["ParentId"] = await _logisticsBiz.GetAllExceptAsync(model.Id);
            else
                ViewData["ParentId"] = await _logisticsBiz.GetAllExceptByUserAsync(user.Id, user.DepartmentId,
                    userDepartments.Select(x => x.DepartmentId).ToList(), model.Id);

            return View(model);
        }

        [Authorize(Permissions.Logistics.Details)]
        public async Task<IActionResult> Details(int id)
        {
            var model = _mapper.Map<LogisticViewModel>(await _logisticsBiz.GetByIdAsync(id));

            ViewData["BankId"] = await _banksBiz.GetAllUnarchivedAsync();
            ViewData["BranchId"] = new List<BankBranch>();
            ViewData["CurrencyId"] = await _currenciesBiz.GetAllUnarchivedAsync();
            return View(model);
        }

        [Authorize(Permissions.Logistics.Delete)]
        public async Task<IActionResult> Archive(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            var result = await _logisticsBiz.ArchiveAsync(id, userId);

            return Json(result);
        }

        [Authorize(Permissions.Logistics.Delete)]
        public async Task<IActionResult> Unarchive(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            var result = await _logisticsBiz.UnarchiveAsync(id, userId);

            return Json(result);
        }

        public class BeneficiaryInfo
        {
            public int Id { get; set; }
            public string BeneficiaryName { get; set; }
            public string BeneficiaryAddress { get; set; }
        }
        [HttpPost]
        public async Task<IActionResult> UpdateBeneficiaryInfo(BeneficiaryInfo model)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            bool result = await _logisticsBiz.UpdateBeneficiaryInfoAsync(model.Id, model.BeneficiaryName, model.BeneficiaryAddress, userId);

            return Json(result);
        }
    }
}

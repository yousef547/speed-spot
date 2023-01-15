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
    public class CompetitorsController : BaseController
    {
        private readonly CompetitorsBiz _competitorsBiz;
        private readonly ActivityTypesBiz _activityTypesBiz;
        private readonly OrganizationTypesBiz _organizationTypesBiz;
        private readonly DepartmentsBiz _departmentsBiz;
        private readonly UserDepartmentsBiz _userDepartmentsBiz;
        private readonly CountriesBiz _countriesBiz;
        private readonly ContactTypesBiz _contactTypesBiz;
        private readonly AttachmentsBiz _attachmentsBiz;
        private readonly CitiesBiz _citiesBiz;
        private readonly ContactsBiz _contactsBiz;
        private readonly PartnerEmployeesBiz _partnerEmployeesBiz;
        private readonly ProductsBiz _productsBiz;
        private readonly CompetitorProductsBiz _competitorProductsBiz;
        private readonly CompetitorFilesBiz _competitorFilesBiz;
        private readonly FavouriteTypesBiz _favouriteTypesBiz;
        private readonly FavouritesBiz _favouritesBiz;
        private readonly BanksBiz _banksBiz;
        private readonly CurrenciesBiz _currenciesBiz;
        private readonly CompetitorBanksBiz _competitorBanksBiz;

        public CompetitorsController(CompetitorsBiz competitorsBiz,
            ActivityTypesBiz activityTypesBiz,
            OrganizationTypesBiz organizationTypesBiz,
            DepartmentsBiz departmentsBiz,
            UserDepartmentsBiz userDepartmentsBiz,
            CountriesBiz countriesBiz,
            ContactTypesBiz contactTypesBiz,
            AttachmentsBiz attachmentsBiz,
            CitiesBiz citiesBiz,
            ContactsBiz contactsBiz,
            PartnerEmployeesBiz partnerEmployeesBiz,
            ProductsBiz productsBiz,
            CompetitorProductsBiz competitorProductsBiz,
            CompetitorFilesBiz competitorFilesBiz,
            FavouriteTypesBiz favouriteTypesBiz,
            FavouritesBiz favouritesBiz,
            BanksBiz banksBiz,
            CurrenciesBiz currenciesBiz,
            CompetitorBanksBiz competitorBanksBiz,
            ApplicationUserManager userManager,
            IWebHostEnvironment hostEnvironment,
            LocalizationService localizationService,
            IMapper mapper) : base(mapper, hostEnvironment, userManager, localizationService)
        {
            _competitorsBiz = competitorsBiz;
            _activityTypesBiz = activityTypesBiz;
            _organizationTypesBiz = organizationTypesBiz;
            _departmentsBiz = departmentsBiz;
            _userDepartmentsBiz = userDepartmentsBiz;
            _countriesBiz = countriesBiz;
            _contactTypesBiz = contactTypesBiz;
            _attachmentsBiz = attachmentsBiz;
            _citiesBiz = citiesBiz;
            _contactsBiz = contactsBiz;
            _partnerEmployeesBiz = partnerEmployeesBiz;
            _productsBiz = productsBiz;
            _competitorProductsBiz = competitorProductsBiz;
            _competitorFilesBiz = competitorFilesBiz;
            _favouriteTypesBiz = favouriteTypesBiz;
            _favouritesBiz = favouritesBiz;
            _banksBiz = banksBiz;
            _currenciesBiz = currenciesBiz;
            _competitorBanksBiz = competitorBanksBiz;
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            string uniqueFileName = UploadFile(file, "Competitors");

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

        public class AddContactModel
        {
            public List<CompetitorContactViewModel> Contacts { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> AddContact(AddContactModel model)
        {
            model.Contacts.Add(new CompetitorContactViewModel());

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

        public class AddEmployeeModel
        {
            public List<CompetitorEmployeeViewModel> Employees { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(AddEmployeeModel model)
        {
            model.Employees.Add(new CompetitorEmployeeViewModel());

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

        [HttpPost]
        public async Task<IActionResult> DeleteDbAttachment(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var result = await _attachmentsBiz.DeleteAsync(id, user.Id);

            return Json(result);
        }

        public class AddProductModel
        {
            public List<CompetitorProductViewModel> Products { get; set; }
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductModel model)
        {
            if (model == null || model.Products == null) model.Products = new List<CompetitorProductViewModel>();
            else
            {
                foreach (var supplierProduct in model.Products)
                {
                    if (supplierProduct.ProductId != null && supplierProduct.ProductId != 0)
                    {
                        var product = await _productsBiz.GetByIdAsync((int)supplierProduct.ProductId);
                        supplierProduct.ProductName = product.Name;
                        supplierProduct.ProductNameAr = product.NameAr;
                    }
                }
            }

            //model.Products.Add(new SupplierProductViewModel());

            return PartialView("_ProductsPartial", model.Products);
        }

        [HttpPost]
        public async Task<IActionResult> RefreshProducts(AddProductModel model)
        {
            if (model == null || model.Products == null) model.Products = new List<CompetitorProductViewModel>();
            else
            {
                foreach (var competitorProduct in model.Products)
                {
                    if (competitorProduct.ProductId != null && competitorProduct.ProductId != 0)
                    {
                        var product = await _productsBiz.GetByIdAsync((int)competitorProduct.ProductId);
                        competitorProduct.ProductName = product.Name;
                        competitorProduct.ProductNameAr = product.NameAr;
                        competitorProduct.ProductVM = _mapper.Map<ProductViewModel>(product);
                    }
                }
            }

            List<IGrouping<string, CompetitorProductViewModel>> DepartmentProductsGrouping = new();
            if (_localizationService.IsRightToLeft())
            {
                DepartmentProductsGrouping = model.Products.GroupBy(g => g.ProductVM.CategoryVM.DepartmentNameAr).ToList();
            }
            else
            {
                DepartmentProductsGrouping = model.Products.GroupBy(g => g.ProductVM.CategoryVM.DepartmentName).ToList();
            }
            return PartialView("_ProductsPartial", DepartmentProductsGrouping);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDbProduct(int id, int productId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            await _competitorProductsBiz.DeleteAsync(productId, user.Id);

            var model = _mapper.Map<List<CompetitorProductViewModel>>(await _competitorProductsBiz.GetByCompetitorIdAsync(id));

            return PartialView("_DetailsProductsPartial", model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDetailsDbAttachment(int id, int attachmentId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            await _attachmentsBiz.DeleteAsync(attachmentId, user.Id);

            var model = _mapper.Map<List<CompetitorFileViewModel>>(await _competitorFilesBiz.GetByCompetitorIdAsync(id));

            return PartialView("_DetailsFilesPartial", model);
        }

        public async Task<IActionResult> ToggleFavourite(int? itemId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var favourityType = await _favouriteTypesBiz.GetByEnumValue((int)FavouriteTypeEnum.Competitors);
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
            public IList<CompetitorBankCurrencyViewModel> Currencies { get; set; }

            public CurrenciesModel()
            {
                if (Currencies == null) Currencies = new List<CompetitorBankCurrencyViewModel>();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCurrency(CurrenciesModel model)
        {
            model.Currencies.Add(new CompetitorBankCurrencyViewModel());

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
            return PartialView("_CurrenciesPartial", new List<CompetitorBankCurrencyViewModel>());
        }

        [HttpPost]
        public async Task<IActionResult> AddBank(CompetitorBankViewModel model)
        {
            var supplierBank = _mapper.Map<CompetitorBank>(model);

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            await _competitorBanksBiz.AddAsync(supplierBank, user.Id);

            var competitorBanks = _mapper.Map<List<CompetitorBankViewModel>>(await _competitorBanksBiz.GetByCompetitorIdAsync(model.CompetitorId));

            return PartialView("_BanksPartial", competitorBanks);
        }

        public async Task<IActionResult> DeleteBank(int id, int bankId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            await _competitorBanksBiz.DeleteAsync(bankId, user.Id);

            var competitorBanks = _mapper.Map<List<CompetitorBankViewModel>>(await _competitorBanksBiz.GetByCompetitorIdAsync(id));

            return PartialView("_BanksPartial", competitorBanks);
        }

        public async Task<IActionResult> EditBank(int bankId)
        {
            var bank = _mapper.Map<CompetitorBankViewModel>(await _competitorBanksBiz.GetByIdAsync(bankId));

            return Json(bank, new JsonSerializerOptions
            {
                PropertyNamingPolicy = null
            });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBank(CompetitorBankViewModel model)
        {
            var competitorBank = _mapper.Map<CompetitorBank>(model);

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            await _competitorBanksBiz.UpdateAsync(competitorBank, user.Id);

            var competitorBanks = _mapper.Map<List<CompetitorBankViewModel>>(await _competitorBanksBiz.GetByCompetitorIdAsync(model.CompetitorId));

            return PartialView("_BanksPartial", competitorBanks);
        }

        public async Task<IActionResult> GetBankCurrencies(int bankId)
        {
            var currencies = _mapper.Map<List<CompetitorBankCurrencyViewModel>>(await _competitorBanksBiz.GetCurrencies(bankId));

            ViewData["CurrencyId"] = await _currenciesBiz.GetAllUnarchivedAsync();
            return PartialView("_CurrenciesPartial", currencies);
        }

        public async Task<IActionResult> GetBankCurrencyDetails(int currencyId)
        {
            var currency = await _competitorBanksBiz.GetCurrencyDetails(currencyId);

            return Json(currency, new JsonSerializerOptions
            {
                PropertyNamingPolicy = null
            });
        }

        public class ProcessDataViewModel
        {
            public bool IsValid { get; set; }
            public Competitor Competitor { get; set; }
            public CompetitorViewModel ViewModel { get; set; }
        }

        public async Task<ProcessDataViewModel> ProcessData(CompetitorViewModel model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            bool isValid = true;

            //Process Departments
            foreach (int departmentId in model.DepartmentIds)
            {
                model.DepartmentVMs.Add(new CompetitorDepartmentViewModel
                {
                    CompetitorId = model.Id,
                    DepartmentId = departmentId
                });
            }

            //Process Contacts
            var deletedContacts = new List<CompetitorContactViewModel>();
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
            var deletedEmployees = new List<CompetitorEmployeeViewModel>();
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

            //Process Products
            var deletedProducts = new List<CompetitorProductViewModel>();
            for (int i = 0; i < model.ProductVMs.Count; i++)
            {
                var product = model.ProductVMs[i];
                if (product.ProductId == null || product.ProductId == 0)
                {
                    deletedProducts.Add(product);
                }
            }
            foreach (var product in deletedProducts)
            {
                model.ProductVMs.Remove(product);
            }
            foreach (var product in model.ProductVMs)
            {
                product.CompetitorId = model.Id;
            }

            var competitor = _mapper.Map<Competitor>(model);


            //Process Attachments
            if (model.Attachments != null && model.Attachments.Count > 0)
            {
                if (competitor.Files == null)
                    competitor.Files = new List<CompetitorFile>();

                foreach (var file in model.Attachments)
                {
                    string uniqueFileName = UploadFile(file, "Competitors");

                    var attachment = new Attachment
                    {
                        FilePath = uniqueFileName,
                        Title = file.FileName,
                        UploadedById = user.Id
                    };
                    await _attachmentsBiz.AddAsync(attachment);

                    competitor.Files.Add(new CompetitorFile
                    {
                        AttachmentId = attachment.Id
                    });

                    model.FileVMs.Add(new CompetitorFileViewModel
                    {
                        AttachmentPath = uniqueFileName,
                        AttachmentId = attachment.Id,
                        CompetitorId = competitor.Id,
                        AttachmentTitle = file.FileName
                    });
                }
            }

            //Logo
            if (model.LogoFile != null)
            {
                string uniqueFileName = UploadFile(model.LogoFile, "Competitors");
                if (model.LogoAttachmentId != 0)
                {
                    competitor.LogoAttachment = new Attachment
                    {
                        FilePath = uniqueFileName,
                        Title = model.LogoFile.FileName,
                        Id = (int)competitor.LogoAttachmentId,
                        UploadedById = user.Id
                    };
                }
                else
                {
                    competitor.LogoAttachment = new Attachment
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
                string uniqueFileName = UploadFile(model.LegalInfoVM.File, "Competitors");
                if (model.LegalInfoVM.AttachmentId != null)
                {
                    competitor.LegalInfo.Attachment = new Attachment
                    {
                        FilePath = uniqueFileName,
                        Title = model.LegalInfoVM.File.FileName,
                        Id = (int)competitor.LegalInfo.AttachmentId,
                        UploadedById = user.Id
                    };
                }
                else
                {
                    competitor.LegalInfo.Attachment = new Attachment
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
                Competitor = competitor,
                ViewModel = model
            };
        }

        [Authorize(Permissions.Competitors.View)]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var favourityType = await _favouriteTypesBiz.GetByEnumValue((int)FavouriteTypeEnum.Competitors);
            var isFavourite = await _favouritesBiz.IsFavourite(favourityType.Id, user.Id, null);
            ViewData["IsFavourite"] = isFavourite;
            return View();
        }

        public async Task<IActionResult> GetCompetitors()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userDepartments = await _userDepartmentsBiz.GetByUserAsync(user.Id);

            List<CompetitorViewModel> competitorVMs;
            if (await _userManager.IsSuperAdmin(user))
                competitorVMs = _mapper.Map<List<CompetitorViewModel>>(await _competitorsBiz.GetParents());
            else
                competitorVMs = _mapper.Map<List<CompetitorViewModel>>(await _competitorsBiz.GetParentsByUserAsync(user.Id, user.DepartmentId,
                     userDepartments.Select(x => x.DepartmentId).ToList()));

            var favourityType = await _favouriteTypesBiz.GetByEnumValue((int)FavouriteTypeEnum.Competitors);
            foreach (var competitorVM in competitorVMs)
            {
                competitorVM.IsFavourite = await _favouritesBiz.IsFavourite(favourityType.Id, user.Id, competitorVM.Id);
            }
            return PartialView("_ShowCompetitorsPartial", competitorVMs);
        }

        public async Task<IActionResult> GetChildCompetitors(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var competitorVMs = _mapper.Map<List<CompetitorViewModel>>(await _competitorsBiz.GetChildsAsync(id));

            var favourityType = await _favouriteTypesBiz.GetByEnumValue((int)FavouriteTypeEnum.Competitors);
            foreach (var competitorVM in competitorVMs)
            {
                competitorVM.IsFavourite = await _favouritesBiz.IsFavourite(favourityType.Id, user.Id, competitorVM.Id);
            }

            return PartialView("_ChildsPartial", competitorVMs);
        }

        [Authorize(Permissions.Competitors.Create)]
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

            if (userIsSuperAdmin)
                ViewData["ParentId"] = await _competitorsBiz.GetAllExceptAsync();
            else
                ViewData["ParentId"] = await _competitorsBiz.GetAllExceptByUserAsync(user.Id, user.DepartmentId,
                    userDepartments.Select(x => x.DepartmentId).ToList());

            ViewBag.CountryId = await _countriesBiz.GetAllUnarchivedAsync();

            ViewData["ContactTypeId"] = await _contactTypesBiz.GetAllUnarchivedAsync();

            return View(new CompetitorViewModel());
        }

        [Authorize(Permissions.Competitors.Create)]
        [HttpPost]
        public async Task<IActionResult> Create(CompetitorViewModel model, string actionType = "Add")
        {
            ProcessDataViewModel processResult = new()
            {
                ViewModel = model
            };

            if (ModelState.IsValid)
            {
                processResult = await ProcessData(model);
                if (processResult.IsValid)
                {
                    var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;

                    processResult.Competitor.CreatedById = userId;
                    var result = await _competitorsBiz.AddAsync(processResult.Competitor, userId);
                    if (result)
                    {
                        switch (actionType)
                        {
                            case "Add":
                                return RedirectToAction(nameof(Index));
                            case "Save":
                                return RedirectToAction(nameof(Details), new { id = processResult.Competitor.Id });
                        }
                    }
                }
            }

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

            if (userIsSuperAdmin)
                ViewData["ParentId"] = await _competitorsBiz.GetAllExceptAsync();
            else
                ViewData["ParentId"] = await _competitorsBiz.GetAllExceptByUserAsync(user.Id, user.DepartmentId,
                    userDepartments.Select(x => x.DepartmentId).ToList());

            ViewBag.CountryId = await _countriesBiz.GetAllUnarchivedAsync();

            ViewData["ContactTypeId"] = await _contactTypesBiz.GetAllUnarchivedAsync();

            return View(model);
        }

        [Authorize(Permissions.Competitors.Edit)]
        public async Task<IActionResult> Edit(int id)
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

            if (userIsSuperAdmin)
                ViewData["ParentId"] = await _competitorsBiz.GetAllExceptAsync(id);
            else
                ViewData["ParentId"] = await _competitorsBiz.GetAllExceptByUserAsync(user.Id, user.DepartmentId,
                    userDepartments.Select(x => x.DepartmentId).ToList(), id);

            ViewBag.CountryId = await _countriesBiz.GetAllUnarchivedAsync();

            ViewData["ContactTypeId"] = await _contactTypesBiz.GetAllUnarchivedAsync();

            var model = _mapper.Map<CompetitorViewModel>(await _competitorsBiz.GetByIdAsync(id));
            return View(model);
        }

        [Authorize(Permissions.Competitors.Edit)]
        [HttpPost]
        public async Task<IActionResult> Edit(CompetitorViewModel model)
        {
            ProcessDataViewModel processResult = new()
            {
                ViewModel = model
            };

            if (ModelState.IsValid)
            {
                processResult = await ProcessData(model);
                if (processResult.IsValid)
                {
                    var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;

                    _competitorsBiz.DeleteCompetitorDepartments(model.Id);
                    _competitorsBiz.DeleteCompetitorProducts(model.Id);
                    var result = await _competitorsBiz.UpdateAsync(processResult.Competitor, userId);
                    if (result)
                    {
                        return RedirectToAction(nameof(Details), new
                        {
                            id = processResult.Competitor.Id
                        });
                    }
                }
            }

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

            if (userIsSuperAdmin)
                ViewData["ParentId"] = await _competitorsBiz.GetAllExceptAsync(model.Id);
            else
                ViewData["ParentId"] = await _competitorsBiz.GetAllExceptByUserAsync(user.Id, user.DepartmentId,
                    userDepartments.Select(x => x.DepartmentId).ToList(), model.Id);

            ViewBag.CountryId = await _countriesBiz.GetAllUnarchivedAsync();

            ViewData["ContactTypeId"] = await _contactTypesBiz.GetAllUnarchivedAsync();

            return View(model);
        }

        [Authorize(Permissions.Competitors.Details)]
        public async Task<IActionResult> Details(int id)
        {
            var model = _mapper.Map<CompetitorViewModel>(await _competitorsBiz.GetByIdAsync(id));

            ViewData["BankId"] = await _banksBiz.GetAllUnarchivedAsync();
            ViewData["BranchId"] = new List<BankBranch>();
            ViewData["CurrencyId"] = await _currenciesBiz.GetAllUnarchivedAsync();
            return View(model);
        }

        [Authorize(Permissions.Competitors.Delete)]
        public async Task<IActionResult> Archive(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            await _competitorsBiz.ArchiveAsync(id, userId);

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Permissions.Competitors.Delete)]
        public async Task<IActionResult> Unarchive(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            await _competitorsBiz.UnarchiveAsync(id, userId);

            return RedirectToAction(nameof(Index));
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
            bool result = await _competitorsBiz.UpdateBeneficiaryInfoAsync(model.Id, model.BeneficiaryName, model.BeneficiaryAddress, userId);

            return Json(result);
        }
    }
}

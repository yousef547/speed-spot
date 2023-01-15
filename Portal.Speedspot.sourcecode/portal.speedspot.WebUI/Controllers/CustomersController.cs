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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

using static portal.speedspot.Models.Constants.ApplicationEnums;
using static portal.speedspot.Models.Constants.Permissions;

namespace portal.speedspot.WebUI.Controllers
{
    public class CustomersController : BaseController
    {
        private readonly QuotationsBiz _quotationsBiz;
        private readonly QuotationStatusesBiz _quotationStatusesBiz;
        private readonly OpportunitiesBiz _opportunitiesBiz;
        private readonly CustomersBiz _customersBiz;
        private readonly ActivityTypesBiz _activityTypesBiz;
        private readonly OrganizationTypesBiz _organizationTypesBiz;
        private readonly DepartmentsBiz _departmentsBiz;
        private readonly CountriesBiz _countriesBiz;
        private readonly CitiesBiz _citiesBiz;
        private readonly ContactTypesBiz _contactTypesBiz;
        private readonly AttachmentsBiz _attachmentsBiz;
        private readonly ContactsBiz _contactsBiz;
        private readonly PartnerEmployeesBiz _partnerEmployeesBiz;
        private readonly FavouriteTypesBiz _favouriteTypesBiz;
        private readonly FavouritesBiz _favouritesBiz;
        private readonly UserDepartmentsBiz _userDepartmentsBiz;
        private readonly BanksBiz _banksBiz;
        private readonly CurrenciesBiz _currenciesBiz;
        private readonly CustomerBanksBiz _customerBanksBiz;

        public CustomersController(
            LocalizationService localizationService,
            IMapper mapper,
            IWebHostEnvironment hostEnvironment,
            ApplicationUserManager userManager,
            CustomersBiz customersBiz,
            ActivityTypesBiz activityTypesBiz,
            OrganizationTypesBiz organizationTypesBiz,
            QuotationStatusesBiz quotationStatusesBiz,
            QuotationsBiz quotationsBiz,
            DepartmentsBiz departmentsBiz,
            OpportunitiesBiz opportunitiesBiz,
            CountriesBiz countriesBiz,
            CitiesBiz citiesBiz,
            ContactTypesBiz contactTypesBiz,
            AttachmentsBiz attachmentsBiz,
            ContactsBiz contactsBiz,
            PartnerEmployeesBiz partnerEmployeesBiz,
            FavouriteTypesBiz favouriteTypesBiz,
            FavouritesBiz favouritesBiz,
            UserDepartmentsBiz userDepartmentsBiz,
            BanksBiz banksBiz,
            CurrenciesBiz currenciesBiz,
            CustomerBanksBiz customerBanksBiz) : base(mapper, hostEnvironment, userManager, localizationService)
        {
            _customersBiz = customersBiz;
            _activityTypesBiz = activityTypesBiz;
            _organizationTypesBiz = organizationTypesBiz;
            _quotationStatusesBiz = quotationStatusesBiz;
            _departmentsBiz = departmentsBiz;
            _quotationsBiz = quotationsBiz;
            _countriesBiz = countriesBiz;
            _citiesBiz = citiesBiz;
            _contactTypesBiz = contactTypesBiz;
            _attachmentsBiz = attachmentsBiz;
            _contactsBiz = contactsBiz;
            _partnerEmployeesBiz = partnerEmployeesBiz;
            _favouriteTypesBiz = favouriteTypesBiz;
            _favouritesBiz = favouritesBiz;
            _userDepartmentsBiz = userDepartmentsBiz;
            _opportunitiesBiz = opportunitiesBiz;
            _banksBiz = banksBiz;
            _currenciesBiz = currenciesBiz;
            _customerBanksBiz = customerBanksBiz;
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

            string uniqueFileName = UploadFile(file, "Customers");

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
            public List<CustomerContactViewModel> Contacts { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> AddContact(AddContactModel model)
        {
            model.Contacts.Add(new CustomerContactViewModel());

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

        public class AddVendorModel
        {
            public List<CustomerVendorRegistrationViewModel> Vendors { get; set; }
        }

        [HttpPost]
        public IActionResult AddVendorRegistration(AddVendorModel model)
        {
            model.Vendors.Add(new CustomerVendorRegistrationViewModel());

            return PartialView("_VendorListPartial", model.Vendors);
        }

        public class AddEmployeeModel
        {
            public List<CustomerEmployeeViewModel> Employees { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(AddEmployeeModel model)
        {
            model.Employees.Add(new CustomerEmployeeViewModel());

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
            var favourityType = await _favouriteTypesBiz.GetByEnumValue((int)FavouriteTypeEnum.Customers);
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
            public IList<CustomerBankCurrencyViewModel> Currencies { get; set; }

            public CurrenciesModel()
            {
                if (Currencies == null) Currencies = new List<CustomerBankCurrencyViewModel>();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCurrency(CurrenciesModel model)
        {
            model.Currencies.Add(new CustomerBankCurrencyViewModel());

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
            return PartialView("_CurrenciesPartial", new List<CustomerBankCurrencyViewModel>());
        }

        [HttpPost]
        public async Task<IActionResult> AddBank(CustomerBankViewModel model)
        {
            var customerBank = _mapper.Map<CustomerBank>(model);

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            await _customerBanksBiz.AddAsync(customerBank, user.Id);

            var customerBanks = _mapper.Map<List<CustomerBankViewModel>>(await _customerBanksBiz.GetByCustomerIdAsync(model.CustomerId));

            return PartialView("_BanksPartial", customerBanks);
        }

        public async Task<IActionResult> DeleteBank(int id, int bankId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            await _customerBanksBiz.DeleteAsync(bankId, user.Id);

            var customerBanks = _mapper.Map<List<CustomerBankViewModel>>(await _customerBanksBiz.GetByCustomerIdAsync(id));

            return PartialView("_BanksPartial", customerBanks);
        }

        public async Task<IActionResult> EditBank(int bankId)
        {
            var bank = _mapper.Map<CustomerBankViewModel>(await _customerBanksBiz.GetByIdAsync(bankId));

            return Json(bank, new JsonSerializerOptions
            {
                PropertyNamingPolicy = null
            });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBank(CustomerBankViewModel model)
        {
            var customerBank = _mapper.Map<CustomerBank>(model);

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            await _customerBanksBiz.UpdateAsync(customerBank, user.Id);

            var customerBanks = _mapper.Map<List<CustomerBankViewModel>>(await _customerBanksBiz.GetByCustomerIdAsync(model.CustomerId));

            return PartialView("_BanksPartial", customerBanks);
        }

        public async Task<IActionResult> GetBankCurrencies(int bankId)
        {
            var currencies = _mapper.Map<List<CustomerBankCurrencyViewModel>>(await _customerBanksBiz.GetCurrencies(bankId));

            ViewData["CurrencyId"] = await _currenciesBiz.GetAllUnarchivedAsync();
            return PartialView("_CurrenciesPartial", currencies);
        }

        public async Task<IActionResult> GetBankCurrencyDetails(int currencyId)
        {
            var currency = await _customerBanksBiz.GetCurrencyDetails(currencyId);

            return Json(currency, new JsonSerializerOptions
            {
                PropertyNamingPolicy = null
            });
        }

        public IActionResult MapTest()
        {
            return View();
        }

        public class ProcessDataViewModel
        {
            public bool IsValid { get; set; }
            public Customer Customer { get; set; }
            public CustomerViewModel ViewModel { get; set; }
        }

        public async Task<ProcessDataViewModel> ProcessData(CustomerViewModel model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            bool isValid = true;

            //Process Departments
            foreach (int departmentId in model.DepartmentIds)
            {
                model.DepartmentVMs.Add(new CustomerDepartmentViewModel
                {
                    CustomerId = model.Id,
                    DepartmentId = departmentId
                });
            }

            //Process Sales Agents
            foreach (string salesAgentId in model.SalesAgentIds)
            {
                model.SalesAgentVMs.Add(new CustomerAgentViewModel
                {
                    CustomerId = model.Id,
                    SalesAgentId = salesAgentId
                });
            }

            //Process Contacts
            var deletedContacts = new List<CustomerContactViewModel>();
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

            //Process Vendor Registrations
            var deletedRegistrations = new List<CustomerVendorRegistrationViewModel>();
            for (int i = 0; i < model.VendorRegistrationVMs.Count; i++)
            {
                var registration = model.VendorRegistrationVMs[i];
                if (registration.BookRegistrationNumber == null || registration.Value == null || registration.RegistrationDate == null || registration.VAT == null ||
                    (registration.AttachmentId == null && registration.File == null))
                {
                    deletedRegistrations.Add(registration);
                }
            }
            foreach (var registration in deletedRegistrations)
            {
                model.VendorRegistrationVMs.Remove(registration);
            }
            foreach (var registration in model.VendorRegistrationVMs)
            {
                if (registration.File != null)
                {
                    string uniqueFileName = UploadFile(registration.File, "Customers");
                    if (registration.AttachmentId != null && registration.AttachmentId != 0)
                    {
                        var attachment = await _attachmentsBiz.GetByIdAsync((int)registration.AttachmentId);
                        attachment.FilePath = uniqueFileName;
                        attachment.UploadedById = user.Id;
                        await _attachmentsBiz.UpdateAsync(attachment);
                    }
                    else
                    {
                        var attachment = new Attachment
                        {
                            FilePath = uniqueFileName,
                            Title = registration.File.FileName,
                            UploadedById = user.Id
                        };

                        await _attachmentsBiz.AddAsync(attachment);
                        registration.AttachmentId = attachment.Id;
                        registration.AttachmentPath = attachment.FilePath;
                    }
                }
            }

            //Process Employees
            var deletedEmployees = new List<CustomerEmployeeViewModel>();
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

            var customer = _mapper.Map<Customer>(model);

            if (model.Attachments != null && model.Attachments.Count > 0)
            {
                if (customer.Files == null)
                    customer.Files = new List<CustomerFile>();

                foreach (var file in model.Attachments)
                {
                    string uniqueFileName = UploadFile(file, "Customers");

                    var attachment = new Attachment
                    {
                        FilePath = uniqueFileName,
                        Title = file.FileName,
                        UploadedById = user.Id
                    };
                    await _attachmentsBiz.AddAsync(attachment);

                    customer.Files.Add(new CustomerFile
                    {
                        AttachmentId = attachment.Id
                    });

                    model.FileVMs.Add(new CustomerFileViewModel
                    {
                        AttachmentPath = uniqueFileName,
                        AttachmentId = attachment.Id,
                        CustomerId = customer.Id,
                        AttachmentTitle = file.FileName
                    });
                }
            }

            if (model.LogoFile != null)
            {
                string uniqueFileName = UploadFile(model.LogoFile, "Customers");
                if (model.LogoAttachmentId != 0)
                {
                    customer.LogoAttachment = new Attachment
                    {
                        FilePath = uniqueFileName,
                        Title = model.LogoFile.FileName,
                        Id = (int)customer.LogoAttachmentId,
                        UploadedById = user.Id
                    };
                }
                else
                {
                    customer.LogoAttachment = new Attachment
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

            if (model.LegalInfoVM != null && model.LegalInfoVM.File != null)
            {
                string uniqueFileName = UploadFile(model.LegalInfoVM.File, "Customers");
                if (model.LegalInfoVM.AttachmentId != null)
                {
                    customer.LegalInfo.Attachment = new Attachment
                    {
                        FilePath = uniqueFileName,
                        Title = model.LegalInfoVM.File.FileName,
                        Id = (int)customer.LegalInfo.AttachmentId,
                        UploadedById = user.Id
                    };
                }
                else
                {
                    customer.LegalInfo.Attachment = new Attachment
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
                Customer = customer,
                ViewModel = model
            };
        }

        [Authorize(Permissions.Customers.View)]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var favourityType = await _favouriteTypesBiz.GetByEnumValue((int)FavouriteTypeEnum.Customers);
            var isFavourite = await _favouritesBiz.IsFavourite(favourityType.Id, user.Id, null);
            ViewData["IsFavourite"] = isFavourite;
            return View();
        }

        public async Task<IActionResult> GetCustomers()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userDepartments = await _userDepartmentsBiz.GetByUserAsync(user.Id);

            List<CustomerViewModel> customerVMs;
            if (await _userManager.IsSuperAdmin(user))
            {
                customerVMs = _mapper.Map<List<CustomerViewModel>>(await _customersBiz.GetParents());
            }
            else
            {
                customerVMs = _mapper.Map<List<CustomerViewModel>>(await _customersBiz.GetParentsByUserAsync(user.Id, user.DepartmentId,
                     userDepartments.Select(x => x.DepartmentId).ToList()));
            }

            var favourityType = await _favouriteTypesBiz.GetByEnumValue((int)FavouriteTypeEnum.Customers);
            foreach (var customerVM in customerVMs)
            {
                customerVM.IsFavourite = await _favouritesBiz.IsFavourite(favourityType.Id, user.Id, customerVM.Id);
            }

            return PartialView("_ShowCustomerPartial", customerVMs);
        }

        public async Task<IActionResult> GetChildCustomers(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var customerVMs = _mapper.Map<List<CustomerViewModel>>(await _customersBiz.GetChildsAsync(id));

            var favourityType = await _favouriteTypesBiz.GetByEnumValue((int)FavouriteTypeEnum.Customers);
            foreach (var customerVM in customerVMs)
            {
                customerVM.IsFavourite = await _favouritesBiz.IsFavourite(favourityType.Id, user.Id, customerVM.Id);
            }

            return PartialView("_ChildsPartial", customerVMs);
        }

        [Authorize(Permissions.Customers.Create)]
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

            ViewData["SalesAgentId"] = (await _userManager.GetSalesAgentsAsync()).Select(u => _mapper.Map<UserViewModel>(u)).ToList();

            if (userIsSuperAdmin)
                ViewData["ParentId"] = await _customersBiz.GetAllExceptAsync();
            else
                ViewData["ParentId"] = await _customersBiz.GetAllExceptByUserAsync(user.Id, user.DepartmentId,
                    userDepartments.Select(x => x.DepartmentId).ToList());

            ViewData["CountryId"] = await _countriesBiz.GetAllUnarchivedAsync();
            ViewData["ContactTypeId"] = await _contactTypesBiz.GetAllUnarchivedAsync();

            return View(new CustomerViewModel());
        }

        [HttpPost]
        [Authorize(Permissions.Customers.Create)]
        public async Task<IActionResult> Create(CustomerViewModel model, string actionType = "Add")
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

                    processResult.Customer.CreatedById = userId;
                    var result = await _customersBiz.AddAsync(processResult.Customer, userId);
                    if (result)
                    {
                        switch (actionType)
                        {
                            case "Add":
                                return RedirectToAction(nameof(Index));
                            case "Save":
                                return RedirectToAction(nameof(Details), new { id = processResult.Customer.Id });
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

            ViewData["SalesAgentId"] = (await _userManager.GetSalesAgentsAsync()).Select(u => _mapper.Map<UserViewModel>(u)).ToList();

            if (userIsSuperAdmin)
                ViewData["ParentId"] = await _customersBiz.GetAllExceptAsync();
            else
                ViewData["ParentId"] = await _customersBiz.GetAllExceptByUserAsync(user.Id, user.DepartmentId,
                    userDepartments.Select(x => x.DepartmentId).ToList());

            ViewData["CountryId"] = await _countriesBiz.GetAllUnarchivedAsync();
            ViewData["ContactTypeId"] = await _contactTypesBiz.GetAllUnarchivedAsync();

            return View(model);
        }

        [Authorize(Permissions.Customers.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var customer = await _customersBiz.GetByIdAsync(id);
            var model = _mapper.Map<CustomerViewModel>(customer);

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

            ViewData["SalesAgentId"] = (await _userManager.GetSalesAgentsAsync()).Select(u => _mapper.Map<UserViewModel>(u)).ToList();

            if (userIsSuperAdmin)
                ViewData["ParentId"] = await _customersBiz.GetAllExceptAsync(id);
            else
                ViewData["ParentId"] = await _customersBiz.GetAllExceptByUserAsync(user.Id, user.DepartmentId,
                    userDepartments.Select(x => x.DepartmentId).ToList(), id);

            ViewData["CountryId"] = await _countriesBiz.GetAllUnarchivedAsync();
            ViewData["ContactTypeId"] = await _contactTypesBiz.GetAllUnarchivedAsync();

            return View(model);
        }

        [Authorize(Permissions.Customers.Edit)]
        [HttpPost]
        public async Task<IActionResult> Edit(CustomerViewModel model)
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

                    _customersBiz.DeleteCustomerDepartments(model.Id);
                    _customersBiz.DeleteCustomerAgents(model.Id);
                    var result = await _customersBiz.UpdateAsync(processResult.Customer, userId);
                    if (result)
                    {
                        return RedirectToAction(nameof(Details), new
                        {
                            id = processResult.Customer.Id
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

            ViewData["SalesAgentId"] = (await _userManager.GetSalesAgentsAsync()).Select(u => _mapper.Map<UserViewModel>(u)).ToList();

            if (userIsSuperAdmin)
                ViewData["ParentId"] = await _customersBiz.GetAllExceptAsync(model.Id);
            else
                ViewData["ParentId"] = await _customersBiz.GetAllExceptByUserAsync(user.Id, user.DepartmentId,
                    userDepartments.Select(x => x.DepartmentId).ToList(), model.Id);

            ViewData["CountryId"] = await _countriesBiz.GetAllUnarchivedAsync();
            ViewData["ContactTypeId"] = await _contactTypesBiz.GetAllUnarchivedAsync();

            return View(model);
        }

        [Authorize(Permissions.Customers.Details)]
        public async Task<IActionResult> Details(int id)
        {
            var model = _mapper.Map<CustomerViewModel>(await _customersBiz.GetByIdAsync(id));

            ViewData["BankId"] = await _banksBiz.GetAllUnarchivedAsync();
            ViewData["BranchId"] = new List<BankBranch>();
            ViewData["CurrencyId"] = await _currenciesBiz.GetAllUnarchivedAsync();
            return View(model);
        }

        [Authorize(Permissions.Customers.Delete)]
        public async Task<IActionResult> Archive(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            var result = await _customersBiz.ArchiveAsync(id, userId);

            return Json(result);
        }

        [Authorize(Permissions.Customers.Delete)]
        public async Task<IActionResult> Unarchive(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            var result = await _customersBiz.UnarchiveAsync(id, userId);

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
            bool result = await _customersBiz.UpdateBeneficiaryInfoAsync(model.Id, model.BeneficiaryName, model.BeneficiaryAddress, userId);

            return Json(result);
        }

        [Authorize(Permissions.Opportunities.View)]
        public async Task<ActionResult> GetOpportunities(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userDepartments = await _userDepartmentsBiz.GetByUserAsync(user.Id);

            List<OpportunityViewModel> opportunityVMs;

            if (await _userManager.IsSuperAdmin(user))
                opportunityVMs = _mapper.Map<List<OpportunityViewModel>>(await _opportunitiesBiz.GetAllUnarchivedAsync(id));
            else
                opportunityVMs = _mapper.Map<List<OpportunityViewModel>>(await _opportunitiesBiz.GetAllUnarchivedByUserAsync(user.Id, user.DepartmentId,
                    userDepartments.Select(x => x.DepartmentId).ToList(), id));
            ViewData["BudgetTotal"] = opportunityVMs.Sum(o => o.Budget);
            var favourityType = await _favouriteTypesBiz.GetByEnumValue((int)FavouriteTypeEnum.Opportunities);
            foreach (var opportunityVM in opportunityVMs)
            {
                opportunityVM.IsFavourite = await _favouritesBiz.IsFavourite(favourityType.Id, user.Id, opportunityVM.Id);
            }
            return PartialView("_HistoryOpportunitiesPartial", opportunityVMs);
        }

        public async Task<IActionResult> GetQuotations(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userDepartments = await _userDepartmentsBiz.GetByUserAsync(user.Id);
            List<QuotationViewModel> quotationVMs;
            List<OpportunityViewModel> opportunityVMs;

            if (await _userManager.IsSuperAdmin(user))
            {
                opportunityVMs = _mapper.Map<List<OpportunityViewModel>>(await _opportunitiesBiz.GetConvertedAsync(id));

                quotationVMs = _mapper.Map<List<QuotationViewModel>>(await _quotationsBiz.GetAllUnarchivedExceptAsync(opportunityVMs.Select(x => x.Id).ToList()));
            }
            else
            {
                opportunityVMs = _mapper.Map<List<OpportunityViewModel>>(await _opportunitiesBiz.GetConvertedByUserAsync(user.Id, user.DepartmentId,
                    userDepartments.Select(x => x.DepartmentId).ToList(), id));

                quotationVMs = _mapper.Map<List<QuotationViewModel>>(await _quotationsBiz.GetAllUnarchivedByUserExceptAsync(user.Id, user.DepartmentId, opportunityVMs.Select(x => x.Id).ToList()));


            }

            var favourityType = await _favouriteTypesBiz.GetByEnumValue((int)FavouriteTypeEnum.Quotations);
            foreach (var quotationVM in quotationVMs)
            {
                quotationVM.IsFavourite = await _favouritesBiz.IsFavourite(favourityType.Id, user.Id, quotationVM.Id);
            }

            var isFavourite = await _favouritesBiz.IsFavourite(favourityType.Id, user.Id, null);
            ViewData["IsFavourite"] = isFavourite;
            //ViewData["StatusEnum"] = statusEnum;
            return PartialView("_HistoryQuotationPartial", quotationVMs);
        }
    }
}

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

namespace portal.speedspot.WebUI.Controllers
{
    public class SuppliersController : BaseController
    {
        private readonly SuppliersBiz _suppliersBiz;
        private readonly CountriesBiz _countriesBiz;
        private readonly ActivityTypesBiz _activityTypesBiz;
        private readonly OrganizationTypesBiz _organizationTypesBiz;
        private readonly UserDepartmentsBiz _userDepartmentsBiz;
        private readonly DepartmentsBiz _departmentsBiz;
        private readonly ContactTypesBiz _contactTypesBiz;
        private readonly ClassificationsBiz _classificationsBiz;
        private readonly ContactsBiz _contactsBiz;
        private readonly PartnerEmployeesBiz _partnerEmployeesBiz;
        private readonly AttachmentsBiz _attachmentsBiz;
        private readonly CitiesBiz _citiesBiz;
        private readonly CertificatesBiz _certificatesBiz;
        private readonly ProductsBiz _productsBiz;
        private readonly SupplierProductsBiz _supplierProductsBiz;
        private readonly SupplierFilesBiz _supplierFilesBiz;
        private readonly FavouritesBiz _favouritesBiz;
        private readonly FavouriteTypesBiz _favouriteTypesBiz;
        private readonly BanksBiz _banksBiz;
        private readonly CurrenciesBiz _currenciesBiz;
        private readonly SupplierBanksBiz _supplierBanksBiz;
        private readonly CompetitorsBiz _competitorsBiz;
        private readonly CompanyDataBiz _companyDataBiz;

        public SuppliersController(ActivityTypesBiz activityTypesBiz,
            OrganizationTypesBiz organizationTypesBiz,
            UserDepartmentsBiz userDepartmentsBiz,
            DepartmentsBiz departmentsBiz,
            ContactTypesBiz contactTypesBiz,
            SuppliersBiz suppliersBiz,
            CountriesBiz countriesBiz,
            ClassificationsBiz classificationsBiz,
            ContactsBiz contactsBiz,
            PartnerEmployeesBiz partnerEmployeesBiz,
            AttachmentsBiz attachmentsBiz,
            CitiesBiz citiesBiz,
            CertificatesBiz certificatesBiz,
            ProductsBiz productsBiz,
            SupplierProductsBiz supplierProductsBiz,
            SupplierFilesBiz supplierFilesBiz,
            FavouritesBiz favouritesBiz,
            FavouriteTypesBiz favouriteTypesBiz,
            BanksBiz banksBiz,
            CurrenciesBiz currenciesBiz,
            SupplierBanksBiz supplierBanksBiz,
            CompetitorsBiz competitorsBiz,
            CompanyDataBiz companyDataBiz,
            ApplicationUserManager userManager,
            IWebHostEnvironment hostEnvironment,
            LocalizationService localizationService,
            IMapper mapper) : base(mapper, hostEnvironment, userManager, localizationService)
        {
            _suppliersBiz = suppliersBiz;
            _countriesBiz = countriesBiz;
            _activityTypesBiz = activityTypesBiz;
            _organizationTypesBiz = organizationTypesBiz;
            _userDepartmentsBiz = userDepartmentsBiz;
            _departmentsBiz = departmentsBiz;
            _contactTypesBiz = contactTypesBiz;
            _classificationsBiz = classificationsBiz;
            _contactsBiz = contactsBiz;
            _partnerEmployeesBiz = partnerEmployeesBiz;
            _attachmentsBiz = attachmentsBiz;
            _citiesBiz = citiesBiz;
            _certificatesBiz = certificatesBiz;
            _productsBiz = productsBiz;
            _supplierProductsBiz = supplierProductsBiz;
            _supplierFilesBiz = supplierFilesBiz;
            _favouritesBiz = favouritesBiz;
            _favouriteTypesBiz = favouriteTypesBiz;
            _banksBiz = banksBiz;
            _currenciesBiz = currenciesBiz;
            _supplierBanksBiz = supplierBanksBiz;
            _competitorsBiz = competitorsBiz;
            _companyDataBiz = companyDataBiz;
        }

        public async Task<IActionResult> GetByDepartments(List<int> departmentIds, List<int> productIds)
        {
            var products = _mapper.Map<List<ProductSuppliersTreeViewModel>>(await _productsBiz.GetWithSuppliers(departmentIds, productIds));

            return PartialView("_SuppliersSelectPartial", products);
        }

        public async Task<IActionResult> RefreshSelected(List<int> supplierIds, List<int> productIds)
        {
            List<SupplierProductsViewModel> suppliers = _mapper.Map<List<SupplierProductsViewModel>>(await _suppliersBiz.GetSelectedWithProductsAsync(supplierIds, productIds));

            return PartialView("_SelectedSuppliersPartial", suppliers);
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            string uniqueFileName = UploadFile(file, "Suppliers");

            var attachment = new Attachment
            {
                FilePath = uniqueFileName,
                Title = file.FileName,
                UploadedById = user.Id
            };
            await _attachmentsBiz.AddAsync(attachment, user.Id);

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
            public List<SupplierContactViewModel> Contacts { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> AddContact(AddContactModel model)
        {
            model.Contacts.Add(new SupplierContactViewModel());

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
            public List<SupplierEmployeeViewModel> Employees { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(AddEmployeeModel model)
        {
            model.Employees.Add(new SupplierEmployeeViewModel());

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


        public class AddCertificateModel
        {
            public List<SupplierCertificateViewModel> Certificates { get; set; }
        }

        [HttpPost]
        public IActionResult AddCertificate(AddCertificateModel model)
        {
            if (model == null || model.Certificates == null)
            {
                model.Certificates = new List<SupplierCertificateViewModel>();
            }
            model.Certificates.Add(new SupplierCertificateViewModel
            {
                CertificateVM = new CertificateViewModel()
            });

            return PartialView("_CertificationsPartial", model.Certificates);
        }

        [HttpPost]
        public IActionResult DeleteCertificate(int index, AddCertificateModel model)
        {
            model.Certificates.RemoveAt(index);

            return PartialView("_CertificationsPartial", model.Certificates);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDbCertificate(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var result = await _certificatesBiz.DeleteAsync(id, user.Id);

            return Json(result);
        }

        public class ProductsModel
        {
            public List<SupplierProductViewModel> Products { get; set; }
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductsModel model)
        {
            if (model == null || model.Products == null) model.Products = new List<SupplierProductViewModel>();
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

            List<IGrouping<string, SupplierProductViewModel>> DepartmentProductsGrouping = new();
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
        public async Task<IActionResult> RefreshProducts(ProductsModel model)
        {
            if (model == null || model.Products == null) model.Products = new List<SupplierProductViewModel>();
            else
            {
                foreach (var supplierProduct in model.Products)
                {
                    if (supplierProduct.ProductId != null && supplierProduct.ProductId != 0)
                    {
                        var product = await _productsBiz.GetByIdAsync((int)supplierProduct.ProductId);
                        supplierProduct.ProductName = product.Name;
                        supplierProduct.ProductNameAr = product.NameAr;
                        supplierProduct.ProductVM = _mapper.Map<ProductViewModel>(product);
                    }
                }
            }

            List<IGrouping<string, SupplierProductViewModel>> DepartmentProductsGrouping = new();
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
            await _supplierProductsBiz.DeleteAsync(productId, user.Id);

            var model = _mapper.Map<List<SupplierProductViewModel>>(await _supplierProductsBiz.GetBySupplierIdAsync(id));

            if (_localizationService.IsRightToLeft())
                return PartialView("_DetailsProductsPartial", model.GroupBy(g => g.ProductVM.DepartmentNameAr).ToList());
            else
                return PartialView("_DetailsProductsPartial", model.GroupBy(g => g.ProductVM.DepartmentName).ToList());
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDetailsDbAttachment(int id, int attachmentId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            await _attachmentsBiz.DeleteAsync(attachmentId, user.Id);

            var model = _mapper.Map<List<SupplierFileViewModel>>(await _supplierFilesBiz.GetBySupplierIdAsync(id));

            return PartialView("_DetailsFilesPartial", model);
        }

        public async Task<IActionResult> ToggleFavourite(int? itemId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var favourityType = await _favouriteTypesBiz.GetByEnumValue((int)FavouriteTypeEnum.Suppliers);
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
            public IList<SupplierBankCurrencyViewModel> Currencies { get; set; }

            public CurrenciesModel()
            {
                if (Currencies == null) Currencies = new List<SupplierBankCurrencyViewModel>();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCurrency(CurrenciesModel model)
        {
            model.Currencies.Add(new SupplierBankCurrencyViewModel());

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
            return PartialView("_CurrenciesPartial", new List<SupplierBankCurrencyViewModel>());
        }

        [HttpPost]
        public async Task<IActionResult> AddBank(SupplierBankViewModel model)
        {
            var supplierBank = _mapper.Map<SupplierBank>(model);

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            await _supplierBanksBiz.AddAsync(supplierBank, user.Id);

            var supplierBanks = _mapper.Map<List<SupplierBankViewModel>>(await _supplierBanksBiz.GetBySupplierIdAsync(model.SupplierId));

            return PartialView("_BanksPartial", supplierBanks);
        }

        public async Task<IActionResult> DeleteBank(int id, int bankId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            await _supplierBanksBiz.DeleteAsync(bankId, user.Id);

            var supplierBanks = _mapper.Map<List<SupplierBankViewModel>>(await _supplierBanksBiz.GetBySupplierIdAsync(id));

            return PartialView("_BanksPartial", supplierBanks);
        }

        public async Task<IActionResult> EditBank(int bankId)
        {
            var bank = _mapper.Map<SupplierBankViewModel>(await _supplierBanksBiz.GetByIdAsync(bankId));

            return Json(bank, new JsonSerializerOptions
            {
                PropertyNamingPolicy = null
            });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBank(SupplierBankViewModel model)
        {
            var supplierBank = _mapper.Map<SupplierBank>(model);

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            await _supplierBanksBiz.UpdateAsync(supplierBank, user.Id);

            var supplierBanks = _mapper.Map<List<SupplierBankViewModel>>(await _supplierBanksBiz.GetBySupplierIdAsync(model.SupplierId));

            return PartialView("_BanksPartial", supplierBanks);
        }

        public async Task<IActionResult> GetBankCurrencies(int bankId)
        {
            var currencies = _mapper.Map<List<SupplierBankCurrencyViewModel>>(await _supplierBanksBiz.GetCurrencies(bankId));

            ViewData["CurrencyId"] = await _currenciesBiz.GetAllUnarchivedAsync();
            return PartialView("_CurrenciesPartial", currencies);
        }

        public async Task<IActionResult> GetBankCurrencyDetails(int currencyId)
        {
            var currency = await _supplierBanksBiz.GetCurrencyDetails(currencyId);

            return Json(currency, new JsonSerializerOptions
            {
                PropertyNamingPolicy = null
            });
        }

        public async Task LoadViewData(int? id = null)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userDepartments = await _userDepartmentsBiz.GetByUserAsync(user.Id);
            bool userIsSuperAdmin = await _userManager.IsSuperAdmin(user);

            ViewBag.CountryId = await _countriesBiz.GetAllUnarchivedAsync();

            ViewData["ActivityTypeId"] = await _activityTypesBiz.GetAllUnarchivedAsync();
            ViewData["OrganizationTypeId"] = await _organizationTypesBiz.GetAllUnarchivedAsync();

            if (userIsSuperAdmin)
                ViewData["DepartmentId"] = await _departmentsBiz.GetAllUnarchivedAsync();
            else
                ViewData["DepartmentId"] = await _departmentsBiz.GetAllUnarchivedByUserAsync(user.DepartmentId,
                    userDepartments.Select(x => x.DepartmentId).ToList());

            if (userIsSuperAdmin)
                ViewData["ParentId"] = await _suppliersBiz.GetAllExceptAsync(id);
            else
                ViewData["ParentId"] = await _suppliersBiz.GetAllExceptByUserAsync(user.Id, user.DepartmentId,
                    userDepartments.Select(x => x.DepartmentId).ToList(), id);

            ViewData["ContactTypeId"] = await _contactTypesBiz.GetAllUnarchivedAsync();
            ViewData["ClassificationId"] = await _classificationsBiz.GetAllUnarchivedAsync();

            var competitors = new List<Competitor>();
            if (userIsSuperAdmin)
                competitors = (await _competitorsBiz.GetAllExceptAsync()).ToList();
            else
                competitors = (await _competitorsBiz.GetAllExceptByUserAsync(user.Id, user.DepartmentId,
                    userDepartments.Select(x => x.DepartmentId).ToList())).ToList();

            var compenyData = await _companyDataBiz.GetAsync();

            List<AgencyViewModel> agencyVMs = new();
            agencyVMs.Add(new AgencyViewModel
            {
                Id = null,
                Name = compenyData.Name,
                NameAr = compenyData.NameAr
            });
            foreach (var competitor in competitors)
            {
                agencyVMs.Add(new AgencyViewModel
                {
                    Id = competitor.Id,
                    Name = competitor.Name,
                    NameAr = competitor.NameAr
                });
            }
            ViewData["CompetitorId"] = agencyVMs;
        }

        public class ProcessDataViewModel
        {
            public bool IsValid { get; set; }
            public Supplier Supplier { get; set; }
            public SupplierViewModel ViewModel { get; set; }
        }

        public async Task<ProcessDataViewModel> ProcessData(SupplierViewModel model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            bool isValid = true;

            //Process Departments
            foreach (int departmentId in model.DepartmentIds)
            {
                model.DepartmentVMs.Add(new SupplierDepartmentViewModel
                {
                    SupplierId = model.Id,
                    DepartmentId = departmentId
                });
            }

            //Process Classifications
            foreach (int classificationId in model.ClassificationIds)
            {
                model.ClassificationVMs.Add(new SupplierClassificationViewModel
                {
                    SupplierId = model.Id,
                    ClassificationId = classificationId
                });
            }

            //Process Contacts
            var deletedContacts = new List<SupplierContactViewModel>();
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

            //Process Certifications
            var deletedCertificates = new List<SupplierCertificateViewModel>();
            for (int i = 0; i < model.CertificateVMs.Count; i++)
            {
                var certificate = model.CertificateVMs[i];
                if (certificate.CertificateVM.Name == null || (certificate.CertificateVM.AttachmentId == null && certificate.File == null))
                {
                    deletedCertificates.Add(certificate);
                }
            }
            foreach (var certificate in deletedCertificates)
            {
                model.CertificateVMs.Remove(certificate);
            }
            foreach (var certificate in model.CertificateVMs)
            {
                if (certificate.File != null)
                {
                    string uniqueFileName = UploadFile(certificate.File, "Suppliers");
                    if (certificate.CertificateVM.AttachmentId != null && certificate.CertificateVM.AttachmentId != 0)
                    {
                        var attachment = await _attachmentsBiz.GetByIdAsync((int)certificate.CertificateVM.AttachmentId);
                        attachment.FilePath = uniqueFileName;
                        attachment.UploadedById = user.Id;
                        await _attachmentsBiz.UpdateAsync(attachment);
                    }
                    else
                    {
                        var attachment = new Attachment
                        {
                            FilePath = uniqueFileName,
                            Title = certificate.File.FileName,
                            UploadedById = user.Id
                        };

                        await _attachmentsBiz.AddAsync(attachment);
                        certificate.CertificateVM.AttachmentId = attachment.Id;
                        certificate.CertificateVM.AttachmentPath = attachment.FilePath;
                    }
                }
            }

            //Process Employees
            var deletedEmployees = new List<SupplierEmployeeViewModel>();
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

            //Process Agency
            if (!model.IsAgency)
            {
                model.CompetitorId = null;
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
            var deletedProducts = new List<SupplierProductViewModel>();
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
                product.SupplierId = model.Id;
            }

            var supplier = _mapper.Map<Supplier>(model);

            if (model.Attachments != null && model.Attachments.Count > 0)
            {
                if (supplier.Files == null)
                    supplier.Files = new List<SupplierFile>();

                foreach (var file in model.Attachments)
                {
                    string uniqueFileName = UploadFile(file, "Suppliers");

                    var attachment = new Attachment
                    {
                        FilePath = uniqueFileName,
                        Title = file.FileName,
                        UploadedById = user.Id
                    };
                    await _attachmentsBiz.AddAsync(attachment);

                    supplier.Files.Add(new SupplierFile
                    {
                        AttachmentId = attachment.Id
                    });

                    model.FileVMs.Add(new SupplierFileViewModel
                    {
                        AttachmentPath = uniqueFileName,
                        AttachmentId = attachment.Id,
                        SupplierId = supplier.Id,
                        AttachmentTitle = file.FileName
                    });
                }
            }

            if (model.LogoFile != null)
            {
                string uniqueFileName = UploadFile(model.LogoFile, "Suppliers");
                if (model.LogoAttachmentId != 0)
                {
                    supplier.LogoAttachment = new Attachment
                    {
                        FilePath = uniqueFileName,
                        Title = model.LogoFile.FileName,
                        Id = (int)supplier.LogoAttachmentId,
                        UploadedById = user.Id
                    };
                }
                else
                {
                    supplier.LogoAttachment = new Attachment
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
                string uniqueFileName = UploadFile(model.LegalInfoVM.File, "Suppliers");
                if (model.LegalInfoVM.AttachmentId != null)
                {
                    supplier.LegalInfo.Attachment = new Attachment
                    {
                        FilePath = uniqueFileName,
                        Title = model.LegalInfoVM.File.FileName,
                        Id = (int)supplier.LegalInfo.AttachmentId,
                        UploadedById = user.Id
                    };
                }
                else
                {
                    supplier.LegalInfo.Attachment = new Attachment
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
                Supplier = supplier,
                ViewModel = model
            };
        }

        [Authorize(Permissions.Suppliers.View)]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var favourityType = await _favouriteTypesBiz.GetByEnumValue((int)FavouriteTypeEnum.Suppliers);
            var isFavourite = await _favouritesBiz.IsFavourite(favourityType.Id, user.Id, null);
            ViewData["IsFavourite"] = isFavourite;

            return View();
        }

        public async Task<IActionResult> GetSuppliers()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userDepartments = await _userDepartmentsBiz.GetByUserAsync(user.Id);

            List<SupplierViewModel> supplierVMs = await _userManager.IsSuperAdmin(user)
                ? _mapper.Map<List<SupplierViewModel>>(await _suppliersBiz.GetParents())
                : _mapper.Map<List<SupplierViewModel>>(await _suppliersBiz.GetParentsByUserAsync(user.Id, user.DepartmentId,
                     userDepartments.Select(x => x.DepartmentId).ToList()));

            var favourityType = await _favouriteTypesBiz.GetByEnumValue((int)FavouriteTypeEnum.Suppliers);
            foreach (var supplierVM in supplierVMs)
            {
                supplierVM.IsFavourite = await _favouritesBiz.IsFavourite(favourityType.Id, user.Id, supplierVM.Id);
            }

            ViewData["CompanyData"] = _mapper.Map<CompanyDataViewModel>(await _companyDataBiz.GetAsync());

            return PartialView("_ShowSuppliersPartial", supplierVMs);
        }

        public async Task<IActionResult> GetChildSuppliers(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var supplierVMs = _mapper.Map<List<SupplierViewModel>>(await _suppliersBiz.GetChildsAsync(id));

            var favourityType = await _favouriteTypesBiz.GetByEnumValue((int)FavouriteTypeEnum.Suppliers);
            foreach (var supplierVM in supplierVMs)
            {
                supplierVM.IsFavourite = await _favouritesBiz.IsFavourite(favourityType.Id, user.Id, supplierVM.Id);
            }

            ViewData["CompanyData"] = _mapper.Map<CompanyDataViewModel>(await _companyDataBiz.GetAsync());

            return PartialView("_ChildsPartial", supplierVMs);
        }

        [Authorize(Permissions.Suppliers.Create)]
        public async Task<IActionResult> Create()
        {
            await LoadViewData();

            return View(new SupplierViewModel());
        }

        [HttpPost]
        [Authorize(Permissions.Suppliers.Create)]
        public async Task<IActionResult> Create(SupplierViewModel model, string actionType = "Add")
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

                    //processResult.Supplier.CreatedById = userId;
                    var result = await _suppliersBiz.AddAsync(processResult.Supplier, userId);
                    if (result)
                    {
                        switch (actionType)
                        {
                            case "Add":
                                return RedirectToAction(nameof(Index));
                            case "Save":
                                return RedirectToAction(nameof(Details), new { id = processResult.Supplier.Id });
                        }
                    }
                }
            }

            await LoadViewData();

            return View(model);
        }

        [Authorize(Permissions.Suppliers.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var supplier = await _suppliersBiz.GetByIdAsync(id);
            var model = _mapper.Map<SupplierViewModel>(supplier);

            await LoadViewData(id);

            return View(model);
        }

        [Authorize(Permissions.Suppliers.Edit)]
        [HttpPost]
        public async Task<IActionResult> Edit(SupplierViewModel model)
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

                    _suppliersBiz.DeleteSupplierDepartments(model.Id);
                    _suppliersBiz.DeleteSupplierClassifications(model.Id);
                    _suppliersBiz.DeleteSupplierProducts(model.Id);
                    var result = await _suppliersBiz.UpdateAsync(processResult.Supplier, userId);
                    if (result)
                    {
                        return RedirectToAction(nameof(Details), new
                        {
                            id = processResult.Supplier.Id
                        });
                    }
                }
            }

            await LoadViewData(model.Id);

            return View(model);
        }

        [Authorize(Permissions.Suppliers.Details)]
        public async Task<IActionResult> Details(int id)
        {
            var supplier = await _suppliersBiz.GetByIdAsync(id);
            var model = _mapper.Map<SupplierViewModel>(supplier);

            ViewData["BankId"] = await _banksBiz.GetAllUnarchivedAsync();
            ViewData["BranchId"] = new List<BankBranch>();
            ViewData["CurrencyId"] = await _currenciesBiz.GetAllUnarchivedAsync();
            ViewData["CompanyData"] = _mapper.Map<CompanyDataViewModel>(await _companyDataBiz.GetAsync());
            return View(model);
        }

        [Authorize(Permissions.Suppliers.Delete)]
        public async Task<IActionResult> Archive(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            var result = await _suppliersBiz.ArchiveAsync(id, userId);

            return Json(result);
        }

        [Authorize(Permissions.Suppliers.Delete)]
        public async Task<IActionResult> Unarchive(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            var result = await _suppliersBiz.UnarchiveAsync(id, userId);

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
            bool result = await _suppliersBiz.UpdateBeneficiaryInfoAsync(model.Id, model.BeneficiaryName, model.BeneficiaryAddress, userId);

            return Json(result);
        }
    }
}

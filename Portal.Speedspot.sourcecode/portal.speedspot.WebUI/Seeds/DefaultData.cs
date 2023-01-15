using portal.speedspot.BizLayer.BizMethods;
using portal.speedspot.BizLayer.IdentityModule;
using portal.speedspot.Models.Concretes;
using portal.speedspot.Models.Concretes.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.WebUI.Seeds
{
    public static class DefaultData
    {
        public static async Task SeedDefaultData(ApplicationUserManager userManager,
            ApplicationRoleManager roleManager,
            EmployeeTypesBiz employeeTypesBiz,
            OpportunityTypesBiz opportunityTypesBiz,
            OpportunityStatusesBiz opportunityStatusesBiz,
            DepartmentsBiz departmentsBiz,
            QuotationStatusesBiz quotationStatusesBiz,
            FavouriteTypesBiz favouriteTypesBiz,
            CustomersBiz customersBiz,
            ProductsBiz productsBiz,
            SuppliersBiz suppliersBiz,
            BanksBiz banksBiz,
            CountriesBiz countriesBiz,
            CurrenciesBiz currenciesBiz,
            DeliveryTypesBiz deliveryTypesBiz,
            PaymentTypesBiz paymentTypesBiz,
            ProductCategoriesBiz productCategoriesBiz,
            ActivityTypesBiz activityTypesBiz,
            OrganizationTypesBiz organizationTypesBiz,
            ContactTypesBiz contactTypesBiz,
            ClassificationsBiz classificationsBiz,
            VisitReasonsBiz visitReasonsBiz,
            CompanyDataBiz companyDataBiz,
            OfferValiditiesBiz offerValiditiesBiz,
            DeliveryPlacesBiz deliveryPlacesBiz,
            OriginDocumentsBiz originDocumentsBiz,
            OfferCertificatesBiz offerCertificatesBiz,
            GuaranteeTermsBiz guaranteeTermsBiz,
            VATValuesBiz vatValuesBiz)
        {
            await SeedVisitReasons(visitReasonsBiz);

            await SeedContactTypes(contactTypesBiz);

            var defaultActivityType = await SeedActivityTypes(activityTypesBiz);

            var defaultOrganizationType = await SeedOrganizationTypes(organizationTypesBiz);

            await SeedDeliveryTypes(deliveryTypesBiz);

            await SeedPaymentTypes(paymentTypesBiz);

            await SeedCurrencies(currenciesBiz);

            var defaultDepartment = await SeedDepartments(departmentsBiz);

            await SeedOpportunityTypes(opportunityTypesBiz);

            await SeedQuotationStatuses(quotationStatusesBiz);

            await SeedOpportunityStatuses(opportunityStatusesBiz);

            await SeedEmployeeTypes(employeeTypesBiz);

            await SeedFavouriteTypes(favouriteTypesBiz);

            int defaultTypeId = (await employeeTypesBiz.GetAllAsync()).FirstOrDefault(t => t.EnumValue == 0).Id;
            int salesTypeId = (await employeeTypesBiz.GetAllAsync()).FirstOrDefault(t => t.EnumValue == 1).Id;

            var defaultRole = await SeedUserRoles(roleManager);

            var adminUser = await SeedUsers(userManager, defaultTypeId, defaultDepartment.Id, defaultRole, salesTypeId);

            await SeedCustomers(customersBiz, adminUser);

            await SeedProducts(productCategoriesBiz, productsBiz, defaultDepartment.Id);

            var defaultCountry = await SeedCountries(countriesBiz);

            var defaultBank = await SeedBanks(banksBiz, defaultCountry.Id);

            var defaultClassification = await SeedClassifications(classificationsBiz);

            await SeedSuppliers(suppliersBiz, defaultActivityType.Id, defaultOrganizationType.Id);

            await SeedCompanyData(companyDataBiz);

            await SeedOfferValidities(offerValiditiesBiz);

            await SeedDeliveryPlaces(deliveryPlacesBiz);

            await SeedOriginDocuments(originDocumentsBiz);

            await SeedOfferCertificates(offerCertificatesBiz);

            await SeedGuaranteeTerms(guaranteeTermsBiz);

            await SeedVATValues(vatValuesBiz);
        }

        public static async Task SeedVisitReasons(VisitReasonsBiz visitReasonsBiz)
        {
            int visitReasonCount = (await visitReasonsBiz.GetAllAsync()).Count;
            if (visitReasonCount == 0)
            {
                await visitReasonsBiz.AddAsync(new VisitReason
                {
                    Name = "Default Visit Reason",
                    NameAr = "سبب زيارة افتراضي"
                });
            }
        }

        public static async Task SeedContactTypes(ContactTypesBiz contactTypesBiz)
        {
            int contactTypeCount = (await contactTypesBiz.GetAllAsync()).Count;
            if (contactTypeCount == 0)
            {
                await contactTypesBiz.AddAsync(new ContactType
                {
                    EnumValue = 0,
                    Name = "Mobile",
                    NameAr = "محمول"
                });

                await contactTypesBiz.AddAsync(new ContactType
                {
                    EnumValue = 1,
                    Name = "Fax",
                    NameAr = "فاكس"
                });

                await contactTypesBiz.AddAsync(new ContactType
                {
                    EnumValue = 2,
                    Name = "Office",
                    NameAr = "مكتب"
                });
            }
        }

        public static async Task<ActivityType> SeedActivityTypes(ActivityTypesBiz activityTypesBiz)
        {
            int activityTypeCount = (await activityTypesBiz.GetAllAsync()).Count;
            if (activityTypeCount == 0)
            {
                await activityTypesBiz.AddAsync(new ActivityType
                {
                    Name = "Default Activity",
                    NameAr = "نشاط افتراضي"
                });
            }
            var defaultType = (await activityTypesBiz.GetAllAsync()).First();

            return defaultType;
        }

        public static async Task<Classification> SeedClassifications(ClassificationsBiz classificationsBiz)
        {
            int classificationCount = (await classificationsBiz.GetAllAsync()).Count;
            if (classificationCount == 0)
            {
                await classificationsBiz.AddAsync(new Classification
                {
                    Name = "Default Classification",
                    NameAr = "تصنيف افتراضي"
                });
            }
            var defaultClassification = (await classificationsBiz.GetAllAsync()).First();

            return defaultClassification;
        }

        public static async Task<OrganizationType> SeedOrganizationTypes(OrganizationTypesBiz organizationTypesBiz)
        {
            int organizationTypeCount = (await organizationTypesBiz.GetAllAsync()).Count;
            if (organizationTypeCount == 0)
            {
                await organizationTypesBiz.AddAsync(new OrganizationType
                {
                    Name = "Default Type",
                    NameAr = "نوع افتراضي"
                });
            }
            var defaultType = (await organizationTypesBiz.GetAllAsync()).First();

            return defaultType;
        }

        public static async Task SeedDeliveryTypes(DeliveryTypesBiz deliveryTypesBiz)
        {
            int deliveryTypeCount = (await deliveryTypesBiz.GetAllAsync()).Count;
            if (deliveryTypeCount == 0)
            {
                await deliveryTypesBiz.AddAsync(new DeliveryType
                {
                    Name = "CIF",
                    NameAr = "CIF"
                });

                await deliveryTypesBiz.AddAsync(new DeliveryType
                {
                    Name = "EXW",
                    NameAr = "EXW"
                });

                await deliveryTypesBiz.AddAsync(new DeliveryType
                {
                    Name = "FCA",
                    NameAr = "FCA"
                });

                await deliveryTypesBiz.AddAsync(new DeliveryType
                {
                    Name = "CPT",
                    NameAr = "CPT"
                });

                await deliveryTypesBiz.AddAsync(new DeliveryType
                {
                    Name = "CIP",
                    NameAr = "CIP"
                });

                await deliveryTypesBiz.AddAsync(new DeliveryType
                {
                    Name = "DPU",
                    NameAr = "DPU"
                });

                await deliveryTypesBiz.AddAsync(new DeliveryType
                {
                    Name = "DAP",
                    NameAr = "DAP"
                });

                await deliveryTypesBiz.AddAsync(new DeliveryType
                {
                    Name = "DDP",
                    NameAr = "DDP"
                });

                await deliveryTypesBiz.AddAsync(new DeliveryType
                {
                    Name = "FAS",
                    NameAr = "FAS"
                });

                await deliveryTypesBiz.AddAsync(new DeliveryType
                {
                    Name = "FOB",
                    NameAr = "FOB"
                });

                await deliveryTypesBiz.AddAsync(new DeliveryType
                {
                    Name = "CFR",
                    NameAr = "CFR"
                });
            }
        }

        public static async Task SeedPaymentTypes(PaymentTypesBiz paymentTypesBiz)
        {
            int paymentTypeCount = (await paymentTypesBiz.GetAllAsync()).Count;
            if (paymentTypeCount == 0)
            {
                await paymentTypesBiz.AddAsync(new PaymentType
                {
                    Name = "Cash",
                    NameAr = "كاش"
                });

                await paymentTypesBiz.AddAsync(new PaymentType
                {
                    Name = "Cheque",
                    NameAr = "شيك"
                });

                await paymentTypesBiz.AddAsync(new PaymentType
                {
                    Name = "100% in advance",
                    NameAr = "100 % مقدماً"
                });

                await paymentTypesBiz.AddAsync(new PaymentType
                {
                    Name = "50% down payment - 50% before dispatch",
                    NameAr = "50% دفعة مقدمة - 50% قبل الارسال"
                });
            }
        }
        public static async Task SeedCurrencies(CurrenciesBiz currenciesBiz)
        {
            int currencyCount = (await currenciesBiz.GetAllAsync()).Count;
            if (currencyCount == 0)
            {
                await currenciesBiz.AddAsync(new Currency
                {
                    ExchangeRate = 1,
                    Name = "Default Currency",
                    NameAr = "عملة افتراضية",
                    Symbol = "£"
                });
            }
        }

        public static async Task SeedCustomers(CustomersBiz customersBiz, ApplicationUser adminUser)
        {
            int customersCount = (await customersBiz.GetAllAsync()).Count;
            if (customersCount == 0)
            {
                await customersBiz.AddAsync(new Customer
                {
                    Name = "Default Customer",
                    NameAr = "عميل افتراضي",
                    CreatedById = adminUser.Id,
                    SubName = "DC"
                });
            }
        }

        public static async Task<Department> SeedDepartments(DepartmentsBiz departmentsBiz)
        {
            int departmentsCount = (await departmentsBiz.GetAllAsync()).Count;
            if (departmentsCount == 0)
            {
                await departmentsBiz.AddAsync(new Department
                {
                    Name = "Default Department",
                    NameAr = "قسم افتراضي",
                    Code = "DD",
                    DefaultOfferCoverLetter = "",
                    DefaultOfferCoverLetterAr = "",
                    Image = ""
                });
            }
            var defaultDepartment = (await departmentsBiz.GetAllAsync()).First();
            return defaultDepartment;
        }

        public static async Task SeedOpportunityStatuses(OpportunityStatusesBiz opportunityStatusesBiz)
        {
            int statusCount = (await opportunityStatusesBiz.GetAllAsync()).Count;
            if (statusCount == 0)
            {
                List<OpportunityStatus> opportunityStatuses = new List<OpportunityStatus>
                {
                    new OpportunityStatus
                    {
                        Name = "Sales Information",
                        NameAr = "معلومة بيع",
                        EnumValue = 0
                    },
                    new OpportunityStatus
                    {
                        Name = "Technical Specifications",
                        NameAr = "المواصفات الفنية",
                        EnumValue = 1
                    },
                    new OpportunityStatus
                    {
                        Name = "Supplier Offers",
                        NameAr = "عروض الموردين",
                        EnumValue = 2
                    }
                };
                foreach (var item in opportunityStatuses)
                {
                    await opportunityStatusesBiz.AddAsync(item);
                }
            }
        }

        public static async Task SeedQuotationStatuses(QuotationStatusesBiz quotationStatusesBiz)
        {
            int quotationStatusCount = (await quotationStatusesBiz.GetAllAsync()).Count;
            if (quotationStatusCount == 0)
            {
                List<QuotationStatus> quotationStatuses = new List<QuotationStatus>
                {
                    new QuotationStatus
                    {
                        Name = "New",
                        NameAr = "جديد",
                        EnumValue = 0
                    },
                    new QuotationStatus
                    {
                        Name = "Technical Session",
                        NameAr = "الجلسة الفنية",
                        EnumValue = 1
                    },
                    new QuotationStatus
                    {
                        Name = "Financial Session",
                        NameAr = "الجلسة المالية",
                        EnumValue = 2
                    },
                    new QuotationStatus
                    {
                        Name = "Approved",
                        NameAr = "تم اعنماده",
                        EnumValue = 3
                    }
                };
                foreach (var item in quotationStatuses)
                {
                    await quotationStatusesBiz.AddAsync(item);
                }
            }
        }

        public static async Task SeedOpportunityTypes(OpportunityTypesBiz opportunityTypesBiz)
        {
            int opportunityTypesCount = (await opportunityTypesBiz.GetAllAsync()).Count;
            if (opportunityTypesCount == 0)
            {
                List<OpportunityType> oportunityTypes = new List<OpportunityType>
                {
                    new OpportunityType
                    {
                        Name = "Direct Offer",
                        NameAr = "عرض مباشر",
                        EnumValue = 0
                    },
                    new OpportunityType
                    {
                        Name = "Envelope",
                        NameAr = "مظروف",
                        EnumValue = 1
                    },
                    new OpportunityType
                    {
                        Name = "Two Envelopes",
                        NameAr = "مظروفين",
                        EnumValue = 2
                    },
                    new OpportunityType
                    {
                        Name = "Auction",
                        NameAr = "مزاد علني",
                        EnumValue = 3
                    },
                    new OpportunityType
                    {
                        Name = "Contract",
                        NameAr = "عقد",
                        EnumValue = 4
                    }
                };
                foreach (var item in oportunityTypes)
                {
                    await opportunityTypesBiz.AddAsync(item);
                }
            }
        }

        public static async Task SeedEmployeeTypes(EmployeeTypesBiz employeeTypesBiz)
        {
            int employeeTypesCount = (await employeeTypesBiz.GetAllAsync()).Count;
            if (employeeTypesCount == 0)
            {
                List<EmployeeType> employeeTypes = new List<EmployeeType>
                {
                    new EmployeeType
                    {
                        Name = "Executive",
                        NameAr = "اداري",
                        EnumValue = 0
                    },
                    new EmployeeType
                    {
                        Name = "Sales Agent",
                        NameAr = "مندوب مبيعات",
                        EnumValue = 1
                    },
                    new EmployeeType
                    {
                        Name = "Manager",
                        NameAr = "مدير",
                        EnumValue = 2
                    },
                    new EmployeeType
                    {
                        Name = "CEO",
                        NameAr = "رئيس مجلس الادارة",
                        EnumValue = 3
                    },
                    new EmployeeType
                    {
                        Name = "Accountant",
                        NameAr = "محاسب",
                        EnumValue = 4
                    },
                    new EmployeeType
                    {
                        Name = "Outside Coordinator",
                        NameAr = "منسق خارجي",
                        EnumValue = 5
                    }
                };
                foreach (var item in employeeTypes)
                {
                    await employeeTypesBiz.AddAsync(item);
                }
            }
        }

        public static async Task SeedFavouriteTypes(FavouriteTypesBiz favouriteTypesBiz)
        {
            int favouriteTypesCount = (await favouriteTypesBiz.GetAllAsync()).Count;
            if (favouriteTypesCount == 0)
            {
                List<FavouriteType> favouriteTypes = new List<FavouriteType>
                {
                    new FavouriteType
                    {
                        Name = "Opportunities",
                        NameAr = "Opportunities",
                        EnumValue = 0
                    },
                    new FavouriteType
                    {
                        Name = "Quotations",
                        NameAr = "Quotations",
                        EnumValue = 1
                    },
                    new FavouriteType
                    {
                        Name = "Customers",
                        NameAr = "Customers",
                        EnumValue = 2
                    },
                    new FavouriteType
                    {
                        Name = "Suppliers",
                        NameAr = "Suppliers",
                        EnumValue = 3
                    },
                      new FavouriteType
                    {
                        Name = "Competitors",
                        NameAr = "Competitors",
                        EnumValue = 4
                    },
                        new FavouriteType
                    {
                        Name = "Logistics",
                        NameAr = "Logistics",
                        EnumValue = 5
                    },
                        new FavouriteType
                    {
                        Name = "Department",
                        NameAr = "Department",
                        EnumValue = 6
                    }
                };
                foreach (var item in favouriteTypes)
                {
                    await favouriteTypesBiz.AddAsync(item);
                }
            }
        }

        public static async Task<ApplicationRole> SeedUserRoles(ApplicationRoleManager roleManager)
        {
            var adminRole = new ApplicationRole
            {
                Name = "Super Admin",
                NameAr = "Super Admin"
            };
            var roleExists = await roleManager.RoleExistsAsync(adminRole.Name);
            if (!roleExists)
            {
                await roleManager.CreateAsync(adminRole);
            }

            var salesRole = new ApplicationRole
            {
                Name = "Sales Agent",
                NameAr = "وكيل مبيعات"
            };
            roleExists = await roleManager.RoleExistsAsync(salesRole.Name);
            if (!roleExists)
            {
                await roleManager.CreateAsync(salesRole);
            }

            var accountantRole = new ApplicationRole
            {
                Name = "Accountant",
                NameAr = "محاسب"
            };
            roleExists = await roleManager.RoleExistsAsync(accountantRole.Name);
            if (!roleExists)
            {
                await roleManager.CreateAsync(accountantRole);
            }

            var hrRole = new ApplicationRole
            {
                Name = "Human Resources",
                NameAr = "موارد بشرية"
            };
            roleExists = await roleManager.RoleExistsAsync(hrRole.Name);
            if (!roleExists)
            {
                await roleManager.CreateAsync(hrRole);
            }

            var managerRole = new ApplicationRole
            {
                Name = "Manager",
                NameAr = "مدير"
            };
            roleExists = await roleManager.RoleExistsAsync(managerRole.Name);
            if (!roleExists)
            {
                await roleManager.CreateAsync(managerRole);
            }

            return adminRole;
        }

        public static async Task<ApplicationUser> SeedUsers(ApplicationUserManager userManager, int defaultTypeId, int defaultDepartmentId, ApplicationRole defaultRole, int salesTypeId)
        {
            int usersCount = userManager.Users.Count();

            if (usersCount == 0)
            {
                var defaultUser = new ApplicationUser
                {
                    UserName = "admin@atcode.net",
                    Email = "admin@atcode.net",
                    PhoneNumber = "00001234567891",
                    EmailConfirmed = true,
                    FirstName = "Admin",
                    LastName = "User",
                    EmployeeTypeId = defaultTypeId,
                    IsArchived = false,
                    DepartmentId = defaultDepartmentId
                };
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Qazwsx123!@#");
                }

                var userInRole = await userManager.IsInRoleAsync(user ?? defaultUser, defaultRole.Name);
                if (!userInRole)
                {
                    await userManager.AddToRoleAsync(user ?? defaultUser, defaultRole.Name);
                }

                var defaultSalesUser = new ApplicationUser
                {
                    UserName = "sales@atcode.net",
                    Email = "sales@atcode.net",
                    PhoneNumber = "00201111111111",
                    EmailConfirmed = true,
                    FirstName = "Default",
                    LastName = "Sales",
                    EmployeeTypeId = salesTypeId,
                    IsArchived = false,
                    DepartmentId = defaultDepartmentId
                };
                var salesUser = await userManager.FindByEmailAsync(defaultSalesUser.Email);
                if (salesUser == null)
                {
                    await userManager.CreateAsync(defaultSalesUser, "Qazwsx123!@#");
                    await userManager.AddToRoleAsync(defaultSalesUser, "Sales Agent");
                }

                return user ?? defaultUser;
            }

            return userManager.Users.FirstOrDefault();
        }

        public static async Task SeedProducts(ProductCategoriesBiz productCategoriesBiz, ProductsBiz productsBiz, int defaultDepartmentId)
        {
            var defaultCategory = new ProductCategory
            {
                DepartmentId = defaultDepartmentId,
                Code = "C1",
                Name = "Default Category",
                NameAr = "فئة افتراضية",
                ParentId = null,
                AutoCode = "01"
            };
            int categoriesCount = (await productCategoriesBiz.GetAllAsync()).Count;
            if (categoriesCount == 0)
            {
                await productCategoriesBiz.AddAsync(defaultCategory);
            }

            int productsCount = (await productsBiz.GetAllAsync()).Count;
            if (productsCount == 0)
            {
                await productsBiz.AddAsync(new Product
                {
                    Code = "P1",
                    Name = "Default Product",
                    NameAr = "صنف افتراضي",
                    CategoryId = defaultCategory.Id,
                    AutoCode = "01"
                });
            }
        }

        public static async Task<Bank> SeedBanks(BanksBiz banksBiz, int defaultCountryId)
        {
            int banksCount = (await banksBiz.GetAllAsync()).Count;
            if (banksCount == 0)
            {
                await banksBiz.AddAsync(new Bank
                {
                    Name = "Default Bank",
                    NameAr = "بنك افتراضي",
                    CountryId = defaultCountryId
                });
            }
            var defaultBank = (await banksBiz.GetAllAsync()).First();

            return defaultBank;
        }

        public static async Task<Country> SeedCountries(CountriesBiz countriesBiz)
        {
            int countriesCount = (await countriesBiz.GetAllAsync()).Count;
            if (countriesCount == 0)
            {
                await countriesBiz.AddAsync(new Country
                {
                    Name = "Egypt",
                    NameAr = "مصر",
                    PhoneCode = "20",
                    Code2 = "EG",
                    Code3 = "EGY"
                });
            }
            var defaultCountry = (await countriesBiz.GetAllAsync()).First();

            return defaultCountry;
        }

        public static async Task SeedSuppliers(SuppliersBiz suppliersBiz, int defaultActivityTypeId, int defaultOrganizationTypeId)
        {
            int suppliersCount = (await suppliersBiz.GetAllAsync()).Count;
            if (suppliersCount == 0)
            {
                await suppliersBiz.AddAsync(new Supplier
                {
                    Email = "ahmed.samir.140391@gmail.com",
                    Name = "Default Supplier",
                    NameAr = "مورد افتراضي",
                    ActivityTypeId = defaultActivityTypeId,
                    OrganizationTypeId = defaultOrganizationTypeId,
                    Rank = 10,
                    SubName = "DS",
                    WebsiteUrl = "https://ds.com"
                });
            }
        }

        public static async Task SeedCompanyData(CompanyDataBiz companyDataBiz)
        {
            var companyData = await companyDataBiz.GetAsync();
            if (companyData == null)
            {
                await companyDataBiz.AddAsync(new CompanyData
                {
                    Name = "My Company",
                    NameAr = "شركتي",
                    Address = "",
                    DefaultOfferNotes = "",
                    DefaultOfferNotesAr = "",
                    DefaultOfferTechnicalNotes = "",
                    DefaultOfferTechnicalNotesAr = "",
                    Description = "",
                    DescriptionAr = ""
                });
            }
        }

        public static async Task SeedOfferValidities(OfferValiditiesBiz offerValiditiesBiz)
        {
            int validitiesCount = (await offerValiditiesBiz.GetAllAsync()).Count;
            if (validitiesCount == 0)
            {
                await offerValiditiesBiz.AddAsync(new OfferValidity
                {
                    Name = "Default Validity",
                    NameAr = "Default Validity"
                });
            }
        }

        public static async Task SeedDeliveryPlaces(DeliveryPlacesBiz deliveryPlacesBiz)
        {
            int placesCount = (await deliveryPlacesBiz.GetAllAsync()).Count;
            if (placesCount == 0)
            {
                await deliveryPlacesBiz.AddAsync(new DeliveryPlace
                {
                    Name = "Default Place",
                    NameAr = "Default Place"
                });
            }
        }

        public static async Task SeedOriginDocuments(OriginDocumentsBiz originDocumentsBiz)
        {
            int originsCount = (await originDocumentsBiz.GetAllAsync()).Count;
            if (originsCount == 0)
            {
                await originDocumentsBiz.AddAsync(new OriginDocument
                {
                    Name = "Default Origin",
                    NameAr = "Default Origin"
                });
            }
        }

        public static async Task SeedOfferCertificates(OfferCertificatesBiz offerCertificatesBiz)
        {
            int certificatesCount = (await offerCertificatesBiz.GetAllAsync()).Count;
            if (certificatesCount == 0)
            {
                await offerCertificatesBiz.AddAsync(new OfferCertificate
                {
                    Name = "Default Certificate",
                    NameAr = "Default Certificate"
                });
            }
        }

        public static async Task SeedGuaranteeTerms(GuaranteeTermsBiz guaranteeTermsBiz)
        {
            int termsCount = (await guaranteeTermsBiz.GetAllAsync()).Count;
            if (termsCount == 0)
            {
                await guaranteeTermsBiz.AddAsync(new GuaranteeTerm
                {
                    Name = "Default Term",
                    NameAr = "Default Term"
                });
            }
        }

        public static async Task SeedVATValues(VATValuesBiz vatValuesBiz)
        {
            int valuesCount = (await vatValuesBiz.GetAllAsync()).Count;
            if (valuesCount == 0)
            {
                await vatValuesBiz.AddAsync(new VATValue
                {
                    Title = "Default",
                    Value = 14
                });
            }
        }
    }
}

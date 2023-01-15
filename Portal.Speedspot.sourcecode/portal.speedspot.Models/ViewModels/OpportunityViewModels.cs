using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static portal.speedspot.Models.Constants.ApplicationEnums;

namespace portal.speedspot.Models.ViewModels
{
    public class OpportunityViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Name")]
        [Required(ErrorMessage = "RequiredField")]
        public string Name { get; set; }
        [Display(Name = "TypeId")]
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public string TypeNameAr { get; set; }
        [Display(Name = "CustomerName")]
        [Required(ErrorMessage = "RequiredField")]
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNameAr { get; set; }
        public string CustomerLogoPath { get; set; }
        [Display(Name = "SalesAgentId")]
        [Required(ErrorMessage = "RequiredField")]
        public string SalesAgentId { get; set; } // user
        public string SalesAgentName { get; set; }
        [Display(Name = "ProjectManagerId")]
        public string ProjectManagerId { get; set; } // user
        public string ProjectManagerName { get; set; }
        [Display(Name = "GuestId")]
        public string GuestId { get; set; } // user
        public string GuestName { get; set; }
        [Display(Name = "Budget")]
        [Required(ErrorMessage = "RequiredField")]
        [RegularExpression(@"^\d+(\.\d{1,})?$", ErrorMessage = "InvalidNumber")]
        public decimal Budget { get; set; }
        public bool IsTechnicalApproved { get; set; }
        public string TechnicalApprovedById { get; set; }
        public string ConvertedById { get; set; } 
        public bool IsConverted { get; set; }
        public int StatusId { get; set; }
        public OpportunityStatusEnum StatusEnum { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "DueDate")]
        public DateTime? DueDate { get; set; }
        [Display(Name = "CountryId")]
        public int? CountryId { get; set; }
        public string CountryName { get; set; }
        public string CountryNameAr { get; set; }
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Display(Name = "DepartmentId")]
        [Required(ErrorMessage = "RequiredField")]
        public int? DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentNameAr { get; set; }
        public string DepartmentCode { get; set; }
        [Display(Name = "Notes")]
        public string Notes { get; set; }
        [Display(Name = "CreatedById")]
        public string CreatedById { get; set; }
        public string CreatedByName { get; set; }
        public bool IsArchived { get; set; }
        public bool IsFavourite { get; set; }
        [Display(Name = "IsLimitedTenderType")]
        public bool IsLimitedTenderType { get; set; }
        [Display(Name = "IsLocalTenderType")]
        public bool IsLocalTenderType { get; set; }
        [Display(Name = "TenderNumber")]
        public string TenderNumber { get; set; }
        public int SerialNo { get; set; }

        public BidBondViewModel BidBondVM { get; set; }
        [Display(Name = "BidBond")]
        public bool IsBidBond { get; set; }

        public BookTenderViewModel BookTenderVM { get; set; }
        [Display(Name = "BookTender")]
        public bool IsBookTender { get; set; }

        public TechnicalSpecificationViewModel TechnicalSpecificationVM { get; set; }
        public IList<RequestOfferViewModel> RequestOfferVMs { get; set; }
        public IList<SupplierOfferViewModel> SupplierOfferVMs { get; set; }
        public int? DaysRemaining
        {
            get
            {
                if (DueDate.HasValue)
                    return (int)Math.Ceiling((DueDate.Value - DateTime.UtcNow).TotalDays);
                else
                    return 0;
            }
        }
        public int Progress
        {
            get
            {
                return StatusEnum switch
                {
                    OpportunityStatusEnum.SalesInformation => 25,
                    OpportunityStatusEnum.TechnicalSpecifications => 25 + 35,
                    OpportunityStatusEnum.SupplierOffers => 25 + 35 + 40,
                    _ => 0,
                };
            }
        }

        public string ProgressSymbol
        {
            get
            {
                return StatusEnum switch
                {
                    OpportunityStatusEnum.SalesInformation => "SI",
                    OpportunityStatusEnum.TechnicalSpecifications => "TS",
                    OpportunityStatusEnum.SupplierOffers => "SO",
                    _ => "",
                };
            }
        }

        public string ProgressSymbolAr
        {
            get
            {
                return StatusEnum switch
                {
                    OpportunityStatusEnum.SalesInformation => "م م",
                    OpportunityStatusEnum.TechnicalSpecifications => "م ف",
                    OpportunityStatusEnum.SupplierOffers => "ع م",
                    _ => "",
                };
            }
        }

        public int RequestOffersNo
        {
            get
            {
                return RequestOfferVMs.Count;
            }
        }

        public int SupplierOffersNo
        {
            get
            {
                return SupplierOfferVMs.Count;
            }
        }

        public int TotalOffersNo
        {
            get
            {
                return RequestOffersNo + SupplierOffersNo;
            }
        }

        public string SerialStr
        {
            get
            {
                return DueDate.HasValue ? DueDate.Value.ToString("yy") + DepartmentCode + SerialNo.ToString() : "";
            }
        }

        public string PendingStr1
        {
            get
            {
                bool isPendingBidbond = false, isPendingBookTender = false;
                if (BidBondVM != null && BidBondVM.RequestVM != null && !(BidBondVM.IsPrinted || BidBondVM.IsRejected))
                    isPendingBidbond = true;
                if (BookTenderVM != null && BookTenderVM.RequestVM != null && !(BookTenderVM.IsPrinted || BookTenderVM.IsRejected))
                    isPendingBookTender = true;
                string result;
                if (isPendingBidbond || isPendingBookTender)
                {
                    if (isPendingBidbond)
                        result = "Bidbond";
                    else
                        result = "Book Tender";
                }
                else
                {
                    if (!IsTechnicalApproved)
                    {
                        if (TechnicalSpecificationVM != null && TechnicalSpecificationVM.ProductVMs.Count > 0)
                            result = "TS Approval";
                        else
                            result = "Tech. Specs.";
                    }
                    else
                    {
                        if (RequestOffersNo == 0)
                            result = "Request Offers";
                        else
                        {
                            if (SupplierOffersNo == 0)
                                result = "Supplier Offers";
                            else
                                result = "Convert To Quotation";

                        }
                    }
                }
                return result;
            }
        }

        public string PendingStr2
        {
            get
            {
                bool isPendingBidbond = false, isPendingBookTender = false;
                if (BidBondVM != null && BidBondVM.RequestVM != null && !(BidBondVM.IsPrinted || BidBondVM.IsRejected))
                    isPendingBidbond = true;
                if (BookTenderVM != null && BookTenderVM.RequestVM != null && !(BookTenderVM.IsPrinted || BookTenderVM.IsRejected))
                    isPendingBookTender = true;
                string result;
                if (isPendingBidbond && isPendingBookTender)
                {
                    result = "Book Tender";
                }
                else
                {
                    result = "";
                }
                return result;
            }
        }

        public string PendingStrAr1
        {
            get
            {
                bool isPendingBidbond = false, isPendingBookTender = false;
                if (BidBondVM != null && BidBondVM.RequestVM != null && !(BidBondVM.IsPrinted || BidBondVM.IsRejected))
                    isPendingBidbond = true;
                if (BookTenderVM != null && BookTenderVM.RequestVM != null && !(BookTenderVM.IsPrinted || BookTenderVM.IsRejected))
                    isPendingBookTender = true;
                string result;
                if (isPendingBidbond || isPendingBookTender)
                {
                    if (isPendingBidbond)
                        result = "التأمين الابتدائي";
                    else
                        result = "كراسة الشروط";
                }
                else
                {
                    if (!IsTechnicalApproved)
                    {
                        if (TechnicalSpecificationVM != null && TechnicalSpecificationVM.ProductVMs.Count > 0)
                            result = "موافقة م. ف.";
                        else
                            result = "المواصفات الفنية";
                    }
                    else
                    {
                        if (RequestOffersNo == 0)
                            result = "طلبات العروض";
                        else
                        {
                            if (SupplierOffersNo == 0)
                                result = "عروض الموردين";
                            else
                                result = "التحويل الى عرض سعر";

                        }
                    }
                }
                return result;
            }
        }

        public string PendingStrAr2
        {
            get
            {
                bool isPendingBidbond = false, isPendingBookTender = false;
                if (BidBondVM != null && BidBondVM.RequestVM != null && !(BidBondVM.IsPrinted || BidBondVM.IsRejected))
                    isPendingBidbond = true;
                if (BookTenderVM != null && BookTenderVM.RequestVM != null && !(BookTenderVM.IsPrinted || BookTenderVM.IsRejected))
                    isPendingBookTender = true;
                string result;
                if (isPendingBidbond && isPendingBookTender)
                {
                    result = "كراسة الشروط";
                }
                else
                {
                    result = "";
                }
                return result;
            }
        }

        public IList<OpportunityAttachmentViewModel> OtherAttachmentVMs
        {
            get
            {
                var attachments = new List<OpportunityAttachmentViewModel>();
                if (BidBondVM != null && BidBondVM.BidBondAttachmentId != null)
                {
                    attachments.Add(new OpportunityAttachmentViewModel
                    {
                        AttachmentId = (int)BidBondVM.BidBondAttachmentId,
                        AttachmentPath = BidBondVM.BidBondAttachmentPath,
                        AttachmentTitle = BidBondVM.BidBondAttachmentTitle,
                        IsDeletable = false,
                        AttachmentUploadedByName = BidBondVM.BidBondAttachmentUploadedByName
                    });
                };
                if (BookTenderVM != null && BookTenderVM.ReceiptAttachmentId != null)
                {
                    attachments.Add(new OpportunityAttachmentViewModel
                    {
                        AttachmentId = (int)BookTenderVM.ReceiptAttachmentId,
                        AttachmentPath = BookTenderVM.ReceiptAttachmentPath,
                        AttachmentTitle = BookTenderVM.ReceiptAttachmentTitle,
                        IsDeletable = false,
                        AttachmentUploadedByName = BookTenderVM.ReceiptAttachmentUploadedByName
                    });
                }
                if (TechnicalSpecificationVM != null && TechnicalSpecificationVM.AttachmentFiles != null)
                {
                    foreach (var attachment in TechnicalSpecificationVM.AttachmentFiles)
                    {
                        if (attachment.AttachmentId != null)
                            attachments.Add(new OpportunityAttachmentViewModel
                            {
                                AttachmentId = (int)attachment.AttachmentId,
                                AttachmentPath = attachment.AttachmentPath,
                                AttachmentTitle = attachment.Title,
                                IsDeletable = !IsTechnicalApproved,
                                AttachmentUploadedByName = attachment.AttachmentUploadedByName
                            });
                    }
                }
                //foreach (var requestOffer in RequestOfferVMs)
                //{
                //    attachments.Add(new OpportunityAttachmentViewModel
                //    {
                //        AttachmentId = requestOffer.RequestOfferAttachmentId,
                //        AttachmentPath = requestOffer.RequestOfferAttachmentPath,
                //        AttachmentTitle = requestOffer.RequestOfferAttachmentTitle,
                //        IsDeletable = false,
                //        AttachmentUploadedByName = requestOffer.RequestOfferAttachmentUploadedByName
                //    });
                //}
                //foreach (var supplierOffer in SupplierOfferVMs)
                //{
                //    attachments.Add(new OpportunityAttachmentViewModel
                //    {
                //        AttachmentId = supplierOffer.OfferAttachmentId,
                //        AttachmentPath = supplierOffer.OfferAttachmentPath,
                //        AttachmentTitle = supplierOffer.OfferAttachmentTitle,
                //        IsDeletable = false,
                //        AttachmentUploadedByName = supplierOffer.OfferAttachmentUploadedByName
                //    });
                //}

                return attachments;
            }
        }

        public OpportunityViewModel()
        {
            if (BookTenderVM == null) BookTenderVM = new BookTenderViewModel();

            if (BidBondVM == null) BidBondVM = new BidBondViewModel();

            if (TechnicalSpecificationVM == null)
            {
                TechnicalSpecificationVM = new TechnicalSpecificationViewModel
                {
                    ProductVMs = new List<TechnicalSpecificationProductViewModel>()
                };
            }

            if (RequestOfferVMs == null) RequestOfferVMs = new List<RequestOfferViewModel>();
            if (SupplierOfferVMs == null) SupplierOfferVMs = new List<SupplierOfferViewModel>();
            IsLimitedTenderType = true;
            IsLocalTenderType = true;
        }
    }

    public class OpportunityInfoViewModel
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNameAr { get; set; }
    }

    public class OpportunityAttachmentViewModel
    {
        public int Id { get; set; }
        public int OpportunityId { get; set; }
        public int AttachmentId { get; set; }
        public string AttachmentPath { get; set; }
        public string AttachmentTitle { get; set; }
        public string AttachmentUploadedByName { get; set; }
        public bool IsDeletable { get; set; }
    }
}

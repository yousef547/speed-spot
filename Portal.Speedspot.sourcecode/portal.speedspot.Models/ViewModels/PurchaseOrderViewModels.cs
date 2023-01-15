using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static portal.speedspot.Models.Constants.ApplicationEnums;

namespace portal.speedspot.Models.ViewModels
{
    public class PurchaseOrderViewModel
    {
        public int Id { get; set; }
        public int OpportunityId { get; set; }
        public FundDetailsViewModel FundDetailsVM { get; set; }
        public List<SupplierOfferViewModel> OfferAccept { get; set; }
        public int DepartmentId { get; set; }
        public DepartmentViewModel DepartmentVM { get; set; }
        public int QuotationId { get; set; }
        public QuotationViewModel Quotation { get; set; }
        public string QuotationNo { get; set; }
        public string QuotationTitle { get; set; }
        public PurchaseOrderStatus Status { get; set; }
        public FundType? Type { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNameAr { get; set; }
        public string CustomerVatNo { get; set; }
        public string CreatedById { get; set; }

        public string LastUpdatedById { get; set; }
        public string LastUpdatedByName { get; set; }


        public int SelectedVersionId { get; set; }
        public QuotationOfferVersionViewModel SelectedVersionVM { get; set; }

        public IList<PurchaseOrderSupplierOfferViewModel> AcceptedOfferVMs { get; set; }

        public CustomerPOViewModel CustomerPOVM { get; set; }
        public IList<SupplierPOViewModel> SupplierPOVMs { get; set; }

        public PurchaseOrderViewModel()
        {
            if (CustomerPOVM is null) CustomerPOVM = new CustomerPOViewModel
            {
                StartDate = DateTime.UtcNow,
            };
        }

        public string SerialStr
        {
            get
            {
                if (Quotation != null)
                {

                    return Quotation.OpportunityVM.DueDate.HasValue ? Quotation.OpportunityVM.DueDate.Value.ToString("yy") + DepartmentVM.Code + Quotation.OpportunityVM.SerialNo.ToString() : "";
                }
                else
                {
                    return "";
                }
            }
        }
    }

    public class PurchaseOrderSupplierOfferViewModel
    {
        public int PurchaseOrderId { get; set; }
        public int SupplierOfferId { get; set; }
        public SupplierOfferViewModel SupplierOfferVM { get; set; }
    }

    public class CustomerPOViewModel
    {
        public int? Id { get; set; }
        public string Code { get; set; }
        public int PurchaseOrderId { get; set; }
        public PurchaseOrderViewModel PurchaseOrder { get; set; }

        public string CustomerPONumber { get; set; }


        [Display(Name = nameof(StartDate))]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        public int DeliveryPeriod { get; set; }

        [Display(Name = nameof(EndDate))]
        public DateTime? EndDate
        {
            get
            {
                return StartDate?.AddDays(DeliveryPeriod);
            }
        }
        public IList<CustomerPoAttachmentViewModel> PoAttachmentPath { get; set; }
        public IList<IFormFile> AllActtachment { get; set; }



    }

    public class CustomerPoAttachmentViewModel
    {
        public int CustomerPoId { get; set; }
        public int AttachmentId { get; set; }
        public string AttachmentPath { get; set; }
        public string TitleImage { get; set; }
    }

    public class SupplierPOViewModel
    {
        public int Id { get; set; }

        public int PurchaseOrderId { get; set; }
        public PurchaseOrderViewModel PurchaseOrder { get; set; }
        public string SupplierPONumber { get; set; }
        public SupplierPoType Type { get; set; }
        [Display(Name = nameof(StartDate))]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        public int SupplierId { get; set; }
        public int DeliveryPeriod { get; set; }

        [Display(Name = nameof(EndDate))]
        public DateTime? EndDate
        {
            get
            {
                return StartDate?.AddDays(DeliveryPeriod);
            }
        }
        public SupplierViewModel Supplier { get; set; }
        public IList<SupplierPoOfferViewModel> SupplierPoOffers { get; set; }

    }

    public class SupplierPoOfferViewModel
    {
        public int Id { get; set; }
        public int SupplierOfferId { get; set; }
        public SupplierOfferViewModel SupplierOfferVM { get; set; }
        public int SupplierPoId { get; set; }
    }

    public class PurchaseOrderBasicDataViewModel
    {
        public int Id { get; set; }

        public int DepartmentId { get; set; }
        public DepartmentViewModel DepartmentVM { get; set; }

        public string CustomerName { get; set; }
        public string CustomerNameAr { get; set; }
        public string CustomerVatNo { get; set; }
    }
}

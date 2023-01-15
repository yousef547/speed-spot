using Microsoft.AspNetCore.Http;
using portal.speedspot.Models.Concretes;
using portal.speedspot.Models.Concretes.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.ViewModels
{
    public class SupplierConversationViewModel
    {
        public int Id { get; set; }
        public int QuotationId { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "MassageDate")]
        public DateTime CreatedDate { get; set; }
        [Display(Name = nameof(DueDate))]
        public DateTime? DueDate { get; set; }
        public string CreatedById { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = nameof(Message))]
        public string Message { get; set; }
        public int? AttachmentId { get; set; }
        public string FilePath { get; set; }
        public string TitleImage { get; set; }
        public IFormFile ConversationFile { get; set; }
        public bool IsSupplier { get; set; }
        public int SupplierId { get; set; }
        public string SupplierLogoPath { get; set; }
    }

}

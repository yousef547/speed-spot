using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.ViewModels
{
    public class CustomerConversationViewModel
    {
        public int Id { get; set; }
        public int QuotationId { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "MassageDate")]
        public DateTime CreatedDate { get; set; }
        [Display(Name = nameof(DueDate))]
        public DateTime? DueDate { get; set; }
        public string CreatedById { get; set; }
        public bool IsCustomer { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = nameof(Message))]
        public string Message { get; set; }
        public string FilePath { get; set; }
        public int? AttachmentId { get; set; }
        public IFormFile ConversationFile { get; set; }
        public string TitleImage { get; set; }
        public string CustomerLogoPath { get; set; }
    }
}

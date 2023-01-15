using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.ViewModels
{
    public class LegalInfoViewModel
    {
        public int? Id { get; set; }

        [Display(Name = nameof(TaxCardNumber))]
        public string TaxCardNumber { get; set; }

        [Display(Name = nameof(CommercialRegister))]
        public string CommercialRegister { get; set; }

        [Display(Name = "VAT #")]
        public string VatNumber { get; set; }

        [Display(Name = nameof(CargoXNumber))]
        public string CargoXNumber { get; set; }

        [Display(Name = "Attachment")]
        public int? AttachmentId { get; set; }
        public string AttachmentPath { get; set; }
        public string AttachmentTitle { get; set; }
        public IFormFile File { get; set; }
    }
}

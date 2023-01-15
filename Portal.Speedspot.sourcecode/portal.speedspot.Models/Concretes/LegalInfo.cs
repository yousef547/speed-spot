using portal.speedspot.Models.BasesAndAbstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class LegalInfo : EntityBase
    {
        public string TaxCardNumber { get; set; }
        public string CommercialRegister { get; set; }
        //[Column(TypeName = "decimal(18,2)")]
        //public decimal? VAT { get; set; }
        public string VatNumber { get; set; }
        public string CargoXNumber { get; set; }
        public int? AttachmentId { get; set; }
        public Attachment Attachment { get; set; }
    }
}

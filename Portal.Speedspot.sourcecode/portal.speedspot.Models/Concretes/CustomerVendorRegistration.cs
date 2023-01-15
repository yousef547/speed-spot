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
    public class CustomerVendorRegistration : EntityBase
    {
        [Required]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        [Required]
        public string BookRegistrationNumber { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Value { get; set; }
        [Required]
        public DateTime RegistrationDate { get; set; }
        [Required]
        [Column(TypeName = "decimal(12,9)")]
        public decimal VAT { get; set; }
        [Required]
        public int AttachmentId { get; set; }
        public Attachment Attachment { get; set; }
    }
}

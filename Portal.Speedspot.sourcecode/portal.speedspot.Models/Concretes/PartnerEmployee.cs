using portal.speedspot.Models.BasesAndAbstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class PartnerEmployee : EntityBase
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Department { get; set; }
        [Required]
        public string Position { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public int PhoneCodeId { get; set; }
        public Country PhoneCode { get; set; }
        [Required]
        [Phone]
        public string Phone { get; set; }
    }
}

using portal.speedspot.Models.BasesAndAbstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class Contact : EntityBase
    {
        [Required]
        public int TypeId { get; set; }
        public ContactType Type { get; set; }
        [Required]
        public int PhoneCodeId { get; set; }
        public Country PhoneCode { get; set; }
        [Required]
        [Phone]
        public string Number { get; set; }
    }
}

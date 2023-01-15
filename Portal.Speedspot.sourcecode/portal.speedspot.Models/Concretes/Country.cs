using portal.speedspot.Models.BasesAndAbstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class Country : EntityBase
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string NameAr { get; set; }
        [Required]
        public string PhoneCode { get; set; }
        public string Code2 { get; set; }
        public string Code3 { get; set; }
    }
}

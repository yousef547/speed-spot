using portal.speedspot.Models.BasesAndAbstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class BankBranch : EntityBase
    {
        public int BankId { get; set; }
        public Bank Bank { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string NameAr { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string SwiftCode { get; set; }
    }
}

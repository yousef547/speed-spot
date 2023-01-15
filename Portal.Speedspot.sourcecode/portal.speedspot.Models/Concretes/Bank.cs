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
    public class Bank : EntityBase
    {
        public int CountryId { get; set; }
        public Country Country { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string NameAr { get; set; }
        public BankFees BankFees { get; set; }
        public virtual IList<BankBranch> Branches { get; set; }
    }
}

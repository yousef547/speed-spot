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
    public class Currency : EntityBase
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string NameAr { get; set; }
        [Required]
        public string Symbol { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal ExchangeRate { get; set; }
    }
}

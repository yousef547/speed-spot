using portal.speedspot.Models.BasesAndAbstracts;
using portal.speedspot.Models.Concretes.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class CustomerAgent : EntityBase
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Required]
        public string SalesAgentId { get; set; }
        public ApplicationUser SalesAgent { get; set; }
    }
}

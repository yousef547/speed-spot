using portal.speedspot.Models.BasesAndAbstracts;
using portal.speedspot.Models.Concretes.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class Visit : EntityBase
    {
        [Required]
        public string SalesAgentId { get; set; }
        public ApplicationUser SalesAgent { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int ReasonId { get; set; }
        public VisitReason Reason { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public int Duration { get; set; }
        [Required]
        public string Notes { get; set; }
    }
}

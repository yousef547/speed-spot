using portal.speedspot.Models.BasesAndAbstracts;
using System.ComponentModel.DataAnnotations;

namespace portal.speedspot.Models.Concretes
{
    public class LogisticEmployee : EntityBase
    {
        [Required]
        public int LogisticId { get; set; }
        public Logistic Logistic { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        public PartnerEmployee Employee { get; set; }
    }
}
using portal.speedspot.Models.BasesAndAbstracts;
using System.ComponentModel.DataAnnotations;

namespace portal.speedspot.Models.Concretes
{
    public class LogisticContact : EntityBase
    {
        [Required]
        public int LogisticId { get; set; }
        public Logistic Logistic { get; set; }
        [Required]
        public int ContactId { get; set; }
        public Contact Contact { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

using portal.speedspot.Models.BasesAndAbstracts;

namespace portal.speedspot.Models.Concretes
{
    public class PaymentTerm : EntityBase
    {
        [Required]
        public string Name { get; set; }

        public int DaysNo { get; set; }
    }
}

using portal.speedspot.Models.BasesAndAbstracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace portal.speedspot.Models.Concretes
{
    public class VATValue : EntityBase
    {
        [Required]
        public string Title { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Value { get; set; }
    }
}

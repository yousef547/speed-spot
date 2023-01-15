using System.ComponentModel.DataAnnotations;

namespace portal.speedspot.Models.BasesAndAbstracts
{
    public class EntityBase
    {
        [Key]
        public int Id { get; set; }
        public bool IsArchived { get; set; }
    }
}

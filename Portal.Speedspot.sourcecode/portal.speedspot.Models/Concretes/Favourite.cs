using Microsoft.EntityFrameworkCore;
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
    [Index(nameof(TypeId), nameof(UserId), nameof(ItemId), IsUnique = true)]
    public class Favourite : EntityBase
    {
        public int TypeId { get; set; }
        public FavouriteType Type { get; set; }
        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int? ItemId { get; set; }
    }
}

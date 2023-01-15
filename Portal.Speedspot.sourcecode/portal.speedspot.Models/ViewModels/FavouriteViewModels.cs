using portal.speedspot.Models.Concretes;
using portal.speedspot.Models.Concretes.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static portal.speedspot.Models.Constants.ApplicationEnums;

namespace portal.speedspot.Models.ViewModels
{
    public class FavouriteViewModel
    {
        public int TypeId { get; set; }
        public FavouriteType Type { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int? ItemId { get; set; }
        public String FavouriteName { get; set; }
        public FavouriteTypeEnum FavouriteEnum { get; set; }
        public string Description { get; set; } 

    }
}

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
    public class StickyNote : EntityBase
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string CreatedById { get; set; }
        public ApplicationUser CreatedBy { get; set; }

        public string PageUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public string PageAction { get; set; }
        public string PageController { get; set; }

    }
}

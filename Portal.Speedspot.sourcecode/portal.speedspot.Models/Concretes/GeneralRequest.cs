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
    public class GeneralRequest : PendingRequest
    {
        [Required]
        public string RequestFromId { get; set; }
        public ApplicationUser RequestFrom { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
    }
}

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
    public class Attachment : EntityBase
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string FilePath { get; set; }
        [Required]
        public string UploadedById { get; set; }
        public ApplicationUser UploadedBy { get; set; }
    }
}

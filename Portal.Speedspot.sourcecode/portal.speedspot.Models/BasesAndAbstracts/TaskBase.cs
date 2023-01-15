using portal.speedspot.Models.Concretes;
using portal.speedspot.Models.Concretes.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.BasesAndAbstracts
{
    public class TaskBase : EntityBase
    {
        [Required]
        public string Description { get; set; }
        //[Required]
        public string DescriptionAr { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsApproved { get; set; }
        public bool IsDone { get; set; }
        public DateTime? DoneDate { get; set; }
    }
}

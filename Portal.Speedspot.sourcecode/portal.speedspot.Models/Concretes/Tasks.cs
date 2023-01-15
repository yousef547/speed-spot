using portal.speedspot.Models.BasesAndAbstracts;
using portal.speedspot.Models.Concretes.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static portal.speedspot.Models.Constants.ApplicationEnums;

namespace portal.speedspot.Models.Concretes
{
    public class Tasks : EntityBase
    {
        [Required]
        public string AssignedToId { get; set; }
        public ApplicationUser AssignedTo { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public PriorityLevelEnum PriorityLevel { get; set; }
        [Required]
        [ForeignKey("ApplicationUser")]
        public string CreatedById { get; set; }

        public ApplicationUser CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsDone { get; set; }
        public DateTime? DoneDate { get; set; }
        public string PagePath { get; set; }
    }
}

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
    public class ToDoTask : TaskBase
    {

        public bool IsRejected { get; set; }
        public DateTime? ApprovalDate { get; set; }

        [Required]
        public string AssignedToId { get; set; }
        public ApplicationUser AssignedTo { get; set; }
        [Required]
        public string CreatedById { get; set; }
        public ApplicationUser CreatedBy { get; set; }

        public int? PendingOnTypeId { get; set; }
        public EmployeeType PendingOnType { get; set; }        

        public string ApprovedById { get; set; }
        public ApplicationUser ApprovedBy { get; set; } 

    }
}

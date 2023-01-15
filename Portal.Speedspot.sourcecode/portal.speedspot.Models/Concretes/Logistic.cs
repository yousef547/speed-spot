using portal.speedspot.Models.BasesAndAbstracts;
using portal.speedspot.Models.Concretes.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace portal.speedspot.Models.Concretes
{
    public class Logistic : Partner
    {
        public Logistic Parent { get; set; }
        [Required]
        public string CreatedById { get; set; }
        public ApplicationUser CreatedBy { get; set; }

        public virtual IList<Logistic> Childs { get; set; }
        public virtual IList<LogisticDepartment> Departments { get; set; }
        public virtual IList<LogisticFile> Files { get; set; }
        public virtual IList<LogisticContact> Contacts { get; set; }
        public virtual IList<LogisticEmployee> Employees { get; set; }
        public virtual IList<LogisticBank> Banks { get; set; }
    }
}

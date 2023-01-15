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
    public class Competitor : Partner
    {
        public Competitor Parent { get; set; }
        [Required]
        public string CreatedById { get; set; }
        public ApplicationUser CreatedBy { get; set; }

        public virtual IList<Competitor> Childs { get; set; }
        public virtual IList<CompetitorDepartment> Departments { get; set; }
        public virtual IList<CompetitorContact> Contacts { get; set; }
        public virtual IList<CompetitorProduct> Products { get; set; }
        public virtual IList<CompetitorFile> Files { get; set; }
        public virtual IList<CompetitorEmployee> Employees { get; set; }
        public virtual IList<CompetitorBank> Banks { get; set; }
    }
}

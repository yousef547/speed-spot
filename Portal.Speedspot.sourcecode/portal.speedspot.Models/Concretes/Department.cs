using portal.speedspot.Models.BasesAndAbstracts;
using portal.speedspot.Models.Concretes.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class Department : EntityBase
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string NameAr { get; set; }
        public string Description { get; set; }
        [Required]
        public string Image { get; set; }

        [Required]
        public string DefaultOfferCoverLetter { get; set; }
        [Required]
        public string DefaultOfferCoverLetterAr { get; set; }

        public string ManagingDirectorId { get; set; }
        public ApplicationUser ManagingDirector { get; set; }

        public string SalesDirectorId { get; set; }
        public ApplicationUser SalesDirector { get; set; }

        public DepartmentSettings Settings { get; set; }

        public Int32 ColorArgb
        {
            get
            {
                return Color.ToArgb();
            }
            set
            {
                Color = Color.FromArgb(value);
            }
        }
        [NotMapped]
        public Color Color { get; set; }

        public virtual IList<Opportunity> Opportunities { get; set; }
        public virtual IList<DepartmentDocument> DepartmentDocuments { get; set; }
        public virtual IList<DepartmentBank> DepartmentBanks { get; set; }
        public string BeneficiaryName { get; set; }
        public string BeneficiaryAddress { get; set; }

    }
}

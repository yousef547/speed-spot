using portal.speedspot.Models.BasesAndAbstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static portal.speedspot.Models.Constants.ApplicationEnums;

namespace portal.speedspot.Models.Concretes
{
    public class Project : EntityBase
    {
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int OpportunityId { get; set; }
        public Opportunity Opportunity { get; set; }
        public int? QuotationId { get; set; }
        public Quotation Quotation { get; set; }
        [Required]
        public ProjectStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}

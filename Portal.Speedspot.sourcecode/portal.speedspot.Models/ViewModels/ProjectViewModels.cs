using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static portal.speedspot.Models.Constants.ApplicationEnums;

namespace portal.speedspot.Models.ViewModels
{
    public class ProjectViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        public string CustomerName { get; set; }
        public int DepartmentId { get; set; }
        public int CustomerId { get; set; }
        public int OpportunityId { get; set; }
        public int? QuotationId { get; set; }
        [Required]
        public ProjectStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}

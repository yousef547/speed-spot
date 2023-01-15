using portal.speedspot.Models.BasesAndAbstracts;
using portal.speedspot.Models.Concretes.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class PerformanceLGRequest : PendingRequest
    {
        public string StatusById { get; set; }
        public ApplicationUser StatusBy { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public bool IsSent { get; set; }
        public string SentById { get; set; }
        public ApplicationUser SentBy { get; set; }
        public DateTime? SentDate { get; set; }
        public bool IsPrinted { get; set; }
        public string PrintedById { get; set; }
        public ApplicationUser PrintedBy { get; set; }
        public DateTime? PrintedDate { get; set; }
        public int PerformanceLGId { get; set; }
        public PerformanceLG PerformanceLG { get; set; }
    }
}

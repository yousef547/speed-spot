
//using Microsoft.EntityFrameworkCore.Infrastructure;
using portal.speedspot.Models.BasesAndAbstracts;
using portal.speedspot.Models.Concretes.Identity;
using System;

namespace portal.speedspot.Models.Concretes
{
    public class BidBondRequest : PendingRequest
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
        public int BidBondId { get; set; }
        public BidBond BidBond { get; set; }
    }
}

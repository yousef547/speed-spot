using portal.speedspot.Models.BasesAndAbstracts;
using portal.speedspot.Models.Concretes.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace portal.speedspot.Models.Concretes
{
    public class Customer : Partner
    {
        [Range(10, 100)]
        [Required]
        public int Rank { get; set; }
        public Customer Parent { get; set; }
        [Required]
        public string CreatedById { get; set; }
        public ApplicationUser CreatedBy { get; set; }

        public virtual IList<Customer> Childs { get; set; }
        public virtual IList<CustomerDepartment> Departments { get; set; }
        public virtual IList<CustomerContact> Contacts { get; set; }
        public virtual IList<CustomerVendorRegistration> VendorRegistrations { get; set; }
        public virtual IList<CustomerFile> Files { get; set; }
        public virtual IList<CustomerEmployee> Employees { get; set; }
        public virtual IList<CustomerAgent> SalesAgents { get; set; }
        public virtual IList<CustomerBank> Banks { get; set; }
        public virtual IList<Visit> Visits { get; set; }
    }
}

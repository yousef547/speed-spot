using portal.speedspot.Models.BasesAndAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class DepartmentDocument : EntityBase
    {
        public string Title { get; set; }
        public string Number { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int AttachmentId { get; set; }
        public Attachment Attachment { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}

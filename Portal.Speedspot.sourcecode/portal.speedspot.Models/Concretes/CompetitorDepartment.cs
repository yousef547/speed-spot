using portal.speedspot.Models.BasesAndAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class CompetitorDepartment : EntityBase
    {
        public int CompetitorId { get; set; }
        public Competitor Competitor { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}

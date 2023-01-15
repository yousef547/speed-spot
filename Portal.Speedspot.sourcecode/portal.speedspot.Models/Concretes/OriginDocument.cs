using portal.speedspot.Models.BasesAndAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class OriginDocument : EntityBase
    {
        public string Name { get; set; }
        public string NameAr { get; set; }
    }
}

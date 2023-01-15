using portal.speedspot.Models.BasesAndAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class FinalLG : LetterOfGuarantee
    {
        public virtual FinalLGRequest Request { get; set; }

        public int QuotationId { get; set; }
        public virtual Quotation Quotation { get; set; }
    }
}

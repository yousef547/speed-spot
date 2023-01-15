using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.ViewModels
{
    public class CertificateViewModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? AttachmentId { get; set; }
        public string AttachmentPath { get; set; }
        public string AttachmentTitle { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.ViewModels
{
    public class PartnerEmployeeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public int? PhoneCodeId { get; set; }
        public string PhoneCode { get; set; }
        public string CountryCode2 { get; set; }
        [Phone]
        public string Phone { get; set; }
    }
}

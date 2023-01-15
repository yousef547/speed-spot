using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.ViewModels
{
    public class ContactViewModel
    {
        public int Id { get; set; }
        public int? TypeId { get; set; }
        public string TypeName { get; set; }
        public string TypeNameAr { get; set; }
        public int? PhoneCodeId { get; set; }
        public string CountryPhoneCode { get; set; }
        public string CountryCode2 { get; set; }
        public string Number { get; set; }
    }
}

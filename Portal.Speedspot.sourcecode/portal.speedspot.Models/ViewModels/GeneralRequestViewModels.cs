using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static portal.speedspot.Models.Constants.ApplicationEnums;

namespace portal.speedspot.Models.ViewModels
{
    public class GeneralRequestViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "RequestFrom")]
        public string RequestFromId { get; set; }
        public string RequestFromName { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "Description")]
        public string Description { get; set; }
        public string RequestedById { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [DataType(DataType.Date)]
        [Display(Name = "Deadline")]
        public DateTime Deadline { get; set; }
        public string RequestedByName{ get; set; }
        public DateTime RequestedDate { get; set; }
        public RequestStatusEnum Status { get; set; }
        public DateTime? StatusDate { get; set; }
    }
}

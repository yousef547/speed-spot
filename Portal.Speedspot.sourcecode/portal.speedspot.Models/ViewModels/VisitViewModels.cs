using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.ViewModels
{
    public class VisitViewModel
    {
        public int Id { get; set; }
        [Display(Name = nameof(SalesAgentId))]
        public string SalesAgentId { get; set; }
        public string SalesAgentName { get; set; }

        [Display(Name = nameof(CustomerId))]
        [Required(ErrorMessage = "RequiredField")]
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNameAr { get; set; }
        public List<int> CustomerDepartmentIds { get; set; }

        [Display(Name = nameof(ReasonId))]
        [Required(ErrorMessage = "RequiredField")]
        public int ReasonId { get; set; }
        public string ReasonName { get; set; }
        public string ReasonNameAr { get; set; }

        [Display(Name = nameof(Date))]
        [Required(ErrorMessage = "RequiredField")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Display(Name = nameof(Time))]
        [Required(ErrorMessage = "RequiredField")]
        [DataType(DataType.Time)]
        public TimeSpan Time { get; set; }

        [Display(Name = nameof(DateTime))]
        public DateTime DateTime
        {
            get
            {
                return Date + Time;
            }
        }

        [Display(Name = "VisitDuration")]
        [Required(ErrorMessage = "RequiredField")]
        public int Duration { get; set; }

        [Display(Name = "BriefNotes")]
        [Required(ErrorMessage = "RequiredField")]
        public string Notes { get; set; }

        public bool IsArchived { get; set; }
    }

    public class VisitReasonViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "NameAr")]
        public string NameAr { get; set; }
        public bool IsArchived { get; set; }
    }
}

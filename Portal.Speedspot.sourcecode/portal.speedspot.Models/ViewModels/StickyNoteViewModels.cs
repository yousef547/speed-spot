using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.ViewModels
{
    public class StickyNoteViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = nameof(Title))]
        public string Title { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = nameof(Description))]
        public string Description { get; set; }
        public string CreatedById { get; set; }
        public string PageUrl { get; set; }
        [Display(Name = "SavePage")]
        public bool IsSaveUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public string PageAction { get; set; }
        public string PageController { get; set; }
    }
}

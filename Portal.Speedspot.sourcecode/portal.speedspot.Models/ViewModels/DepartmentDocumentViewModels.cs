using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.ViewModels
{
    public class DepartmentDocumentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = nameof(Title))]
        public string Title { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = nameof(Number))]
        public string Number { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = nameof(IssueDate))]
        public DateTime IssueDate { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = nameof(ExpiryDate))]
        public DateTime ExpiryDate { get; set; }
        public int AttachmentId { get; set; }
        public int DepartmentId { get; set; }
        public string FilePath { get; set; }
        public string TitleImage { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = nameof(DocumentFile))]
        public IFormFile DocumentFile { get; set; }
        public bool IsArchived { get; set; }

        public int? DaysRemaining
        {
            get
            {
                return (int)Math.Ceiling((ExpiryDate - DateTime.UtcNow).TotalDays);
            }
        }

        public string DaysRemainingClass
        {
            get
            {
                if (DaysRemaining.HasValue)
                {
                    switch (DaysRemaining)
                    {
                        case <= 3:
                            return "lessthanthree";
                        case > 3:
                            if (DaysRemaining <= 10)
                            {
                                return "threetoten";
                            }
                            else
                            {
                                return "largerthanten";
                            }
                        default:
                            return "largerthanten";
                    }
                }
                else
                {
                    return "largerthanten";
                }
            }
        }

    }
}

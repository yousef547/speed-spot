using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static portal.speedspot.Models.Constants.ApplicationEnums;

namespace portal.speedspot.Models.ViewModels
{
    public class TaskViewModel
    {
        public int Id { get; set; }
        public bool IsArchived { get; set; }
        [Display(Name = "Title")]
        [Required(ErrorMessage = "RequiredField")]
        public string Title { get; set; }
        [Display(Name = "Description")]
        [Required(ErrorMessage = "RequiredField")]
        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }
        [Display(Name = "Deadline")]
        [Required(ErrorMessage = "RequiredField")]
        public DateTime? Deadline { get; set; }
        public int? DaysRemaining
        {
            get
            {
                if (Deadline.HasValue)
                    return (int)Math.Ceiling((Deadline.Value - DateTime.UtcNow).TotalDays);
                else
                    return 0;
            }
        }

        public string DaysRemainingColor
        {
            get
            {
                if (DaysRemaining.HasValue)
                {
                    switch (DaysRemaining)
                    {
                        case <= 3:
                            return "#D70D0DCC";
                        case > 3:
                            if (DaysRemaining <= 10)
                            {
                                return "#F5A623";
                            }
                            else
                            {
                                return "#409F87";
                            }
                        default:
                            return "#409F87";
                    }
                }
                else
                {
                    return "#409F87";
                }
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

        public bool IsDone { get; set; }
        public DateTime? DoneDate { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "AssignedTo")]
        public string AssignedToId { get; set; }
        public string AssignedToName { get; set; }
        public string AssignedToProfilePicture { get; set; }
        public int AssignedToDepartmentId { get; set; }

        [Display(Name = "PriorityLevel")]
        [Required(ErrorMessage = "RequiredField")]
        public PriorityLevelEnum? PriorityLevel { get; set; }

        [Display(Name = "SavePage")]
        public bool IsSavePage
        {
            get
            {
                if (PagePath != null) return true; else return false;
            }
            set
            {

            }
        }

        public string PagePath { get; set; }

        public string CreatedById { get; set; }
        public string CreatedByName { get; set; }
        public string CreatedByProfilePicture { get; set; }
        public int CreatedByDepartmentId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static portal.speedspot.Models.Constants.ApplicationEnums;

namespace portal.speedspot.Models.ViewModels
{
    public class PendingRequestViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public PendingRequestSourceEnum Source { get; set; }
        public string Controller { get; set; }
        public string RequestedById { get; set; }
        public string RequestedByName { get; set; }
        public DateTime RequestedDate { get; set; }
        public string RequestedByProfilePicture { get; set; }
        public string AssignedToId { get; set; }
        public string AssignedToName { get; set; }
        public string AssignedToProfilePicture { get; set; }
        public RequestStatusEnum Status { get; set; }
        public string StatusByName { get; set; }
        public string StatusByProfilePicture { get; set; }
        public int DepartmentId { get; set; }
        public int ItemId { get; set; }
        public bool IsSent { get; set; }
        public string SentByName { get; set; }
        public string SentByProfilePicture { get; set; }

        public string PendingOnUserId { get; set; }
        public string PendingOnName { get; set; }
        public string PendingOnProfilePicture { get; set; }

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
    }
}

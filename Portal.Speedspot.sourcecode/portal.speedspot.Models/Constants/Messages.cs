using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Constants
{
    public static class Messages
    {
        public const string AccountLockedOut = "Account is in lockout, please try again after {0} minutes.";
        public const string CodeSent = "A verification code has been sent to you email.";
        public const string InvalidLogin = "Invalid login attempt.";
        public const string EmailNotFound = "No user associated with this email.";
        public const string PasswordResetSuccessfull = "Your password has been reset successfully.";
        public const string PasswordChangedSuccessfull = "Your password has been changed successfully.";
        public const string UpdateSuccessfull = "Updated successfully";
        public const string CreateSuccessfull = "Created successfully";
        public const string ArchiveSuccessfull = "Archived successfully";
        public const string UnarchiveSuccessfull = "Unarchived successfully";
    }
}

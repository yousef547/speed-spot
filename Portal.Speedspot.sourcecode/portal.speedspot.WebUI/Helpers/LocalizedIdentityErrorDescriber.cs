using Microsoft.AspNetCore.Identity;
using portal.speedspot.Resources.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.WebUI.Helpers
{
    public class LocalizedIdentityErrorDescriber : IdentityErrorDescriber
    {
        LocalizationService _localizationService;
        public LocalizedIdentityErrorDescriber(LocalizationService localizationService)
        {
            _localizationService = localizationService;
        }

        public override IdentityError PasswordRequiresDigit()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresDigit),
                Description = _localizationService["PasswordRequiresDigit"].Value
            };
        }

        public override IdentityError PasswordRequiresLower()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresDigit),
                Description = _localizationService["PasswordRequiresLower"].Value
            };
        }

        public override IdentityError PasswordRequiresUpper()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresDigit),
                Description = _localizationService["PasswordRequiresUpper"].Value
            };
        }

        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresDigit),
                Description = _localizationService["PasswordRequiresNonAlphaNumeric"].Value
            };
        }

        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresDigit),
                Description = string.Format(_localizationService["PasswordTooShort"].Value, length)
            };
        }

        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError
            {
                Code = nameof(DuplicateEmail),
                Description = string.Format(_localizationService["DuplicateEmail"].Value, email)
            };
        }

        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError
            {
                Code = nameof(DuplicateEmail),
                Description = string.Format(_localizationService["DuplicateUserName"].Value, userName)
            };
        }

        public override IdentityError PasswordMismatch()
        {
            return new IdentityError
            {
                Code = nameof(PasswordMismatch),
                Description = string.Format(_localizationService["PasswordMismatch"].Value)
            };
        }
    }
}

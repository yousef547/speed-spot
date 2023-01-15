using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Helpers
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RequiredIfAttribute : RequiredAttribute
    {
        private readonly string _comparisonProperty;
        public RequiredIfAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ErrorMessage = ErrorMessageString;
            var currentValue = value;

            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (property == null)
                throw new ArgumentException("Property with this name not found");

            var comparisonValue = property.GetValue(validationContext.ObjectInstance)?.ToString();

            if (comparisonValue != null)
            {
                if (currentValue == null)
                    return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}

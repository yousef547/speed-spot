using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Helpers
{
    public class RequiredIfAttributeAdapter : AttributeAdapterBase<RequiredIfAttribute>
    {
        public RequiredIfAttributeAdapter(RequiredIfAttribute attribute, IStringLocalizer stringLocalizer) : base(attribute, stringLocalizer) { }

        public override void AddValidation(ClientModelValidationContext context) { }

        public override string GetErrorMessage(ModelValidationContextBase validationContext)
        {
            return GetErrorMessage(validationContext.ModelMetadata, validationContext.ModelMetadata.GetDisplayName());
        }
    }
}

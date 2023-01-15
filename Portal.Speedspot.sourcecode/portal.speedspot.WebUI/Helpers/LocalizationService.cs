using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;
using portal.speedspot.Resources.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace portal.speedspot.WebUI.Helpers
{
    public class LocalizationService
    {
        private readonly IHtmlLocalizer _htmlLocalizer;

        public LocalizedHtmlString this[string name]
        {
            get
            {
                return _htmlLocalizer[name];
            }
        }

        public LocalizationService(IStringLocalizerFactory factory, IHtmlLocalizerFactory htmlFactory)
        {
            var type = typeof(SharedResources);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _htmlLocalizer = htmlFactory.Create("SharedResources", assemblyName.Name);
        }

        public bool IsRightToLeft()
        {
            return CultureInfo.CurrentCulture.TextInfo.IsRightToLeft;
        }
    }
}

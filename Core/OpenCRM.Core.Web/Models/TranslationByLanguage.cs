using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.Core.Web.Models
{
    public class TranslationByLanguage
    {
        public string LanguageCode { get; set; } = string.Empty;

        public string Translation { get;set; } = string.Empty;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.Core.Web.Models
{
    public class TranslationLanguageCodeModel
    {
        public Guid ID { get; set; }

        public string Translation { get;set; } = string.Empty;

        public string LanguageCode { get; set; } = string.Empty;

        public Guid LanguageId { get; set; }
    }
}

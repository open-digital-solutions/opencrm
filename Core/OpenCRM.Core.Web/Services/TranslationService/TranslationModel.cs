using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.Core.Web.Services.TranslationService
{
    public class TranslationModel
    {
        public required Guid ID { get; set; }

        public required string Key { get; set; }

        public required string Translation { get; set; }

        public required Guid LanguageId { get; set; }
    }
}
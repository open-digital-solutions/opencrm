using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.Core.Web.Services
{
    public class LanguageModel<LanguageEntity>
    {      
        public required Guid ID { get; set; }
        public required string Code { get; set; }
        public required string Name { get; set; }        
    }
}

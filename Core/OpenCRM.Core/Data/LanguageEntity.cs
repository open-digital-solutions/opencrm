using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDHS.Shared.Data
{
    public class LanguageEntity : BaseEntity
    {
        public required string Code { get; set; }
        public required string Name { get; set; }

    }
}

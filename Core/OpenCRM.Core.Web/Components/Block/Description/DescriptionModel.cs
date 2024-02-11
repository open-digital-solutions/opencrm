using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.Core.Web.Components.Block.Description
{
    public class DescriptionModel
    {
        public string? Text { get; set; }

        public List<DescriptionModel>? ListItems { get; set; }
    }
}

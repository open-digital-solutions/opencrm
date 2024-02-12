using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.Core.Web.Models
{
    public class DescriptionItem
    {
        public string Text { get; set; } = string.Empty;

        public List<DescriptionItem>? Items { get; set; }
    }

    public class DescriptionModel
    {
        public string? Text { get; set; }

        public List<DescriptionItem>? ListItems { get; set; }
    }
}

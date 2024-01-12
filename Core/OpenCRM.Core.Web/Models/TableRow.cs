using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.Core.Web.Models
{
    public class TRowData
    {
        public string Label { get; set; } = string.Empty;

        public bool IsButton { get; set; } = false;

        public string? Url { get; set; }

        public string? IconName { get; set; }

    }

    public class TableRow<TRowData>
    {
        public Guid ID { get; set; }

        public List<TRowData> Datas { get; set;} = new List<TRowData>();
    }
}

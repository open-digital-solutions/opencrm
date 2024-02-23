using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.Core.Web.Models
{
    public class BreadCrumbLinkModel {
        public string Name { get; set; } = string.Empty;
        public string Area { get; set; } = string.Empty;
        public string Page { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }
}

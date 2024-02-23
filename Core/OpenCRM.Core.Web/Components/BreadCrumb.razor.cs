using Microsoft.AspNetCore.Components;
using OpenCRM.Core.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.Core.Web.Components
{
    public partial class BreadCrumb : ComponentBase
    {
        [Parameter]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();
    }
}

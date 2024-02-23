using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.Web.Models;

namespace OpenCRM.Core.Web.Pages
{
    public class _BreadCrumbPartialModel : PageModel
    {

        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        public _BreadCrumbPartialModel()
        {

        }
    }
}

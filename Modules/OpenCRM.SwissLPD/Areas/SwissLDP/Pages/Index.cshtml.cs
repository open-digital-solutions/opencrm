using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.Web.Models;

namespace OpenCRM.SwissLPD.Areas.SwissLDP.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        public void OnGet()
        {
            Links.Add(new BreadCrumbLinkModel()
            {
                Area = "",
                IsActive = true,
                Name = "SwissLDP",
                Page = "",
                Url = "/SwissLDP/Index"
            });
        }
    }
}

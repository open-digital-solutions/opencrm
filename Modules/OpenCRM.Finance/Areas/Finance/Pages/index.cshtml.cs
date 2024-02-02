using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.Web.Models;

namespace OpenCRM.Finance.MyFeature.Pages
{
    public class Page1Model : PageModel
    {
        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        public void OnGet()
        {
            var linkManageHome = new BreadCrumbLinkModel()
            {
                Area = "",
                IsActive = true,
                Name = "Finance",
                Page = "",
                Url = "/Finance/Index"
            };

            Links.Add(linkManageHome);

            var linkAccountings = new BreadCrumbLinkModel()
            {
                Area = "",
                IsActive = true,
                Name = "Accounting",
                Page = "",
                Url = "/Finance/Accounting"
            };
            Links.Add(linkAccountings);
        }
    }
}
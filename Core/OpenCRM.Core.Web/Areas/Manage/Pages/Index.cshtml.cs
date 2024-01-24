using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.Web.Models;

namespace OpenCRM.Core.Web.Areas.Manage.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        public void OnGet()
        {
            var link = new BreadCrumbLinkModel()
            {
                Area = "",
                IsActive = true,
                Name = "Home",
                Page = "",
                Url = "/"
            };

            Links.Add(link);

            var link22 = new BreadCrumbLinkModel()
            {
                Area = "",
                IsActive = true,
                Name = "Manage",
                Page = "",
                Url = "/Manage/Index"
            };

            Links.Add(link22);

            var link2 = new BreadCrumbLinkModel()
            {
                Area = "",
                IsActive = true,
                Name = "Languages",
                Page = "",
                Url = "/Manage/Languages"
            };

            Links.Add(link2);

            /*var link3 = new BreadCrumbLinkModel()
            {
                Area = "",
                IsActive = true,
                Name = "Translations",
                Page = "",
                Url = "/Manage/Translations/Index"
            };

            Links.Add(link3);*/
        }
    }
}

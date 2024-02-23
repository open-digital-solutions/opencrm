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
            var linkManageHome = new BreadCrumbLinkModel()
            {
                Area = "",
                IsActive = true,
                Name = "Manage",
                Page = "",
                Url = "/Manage/Index"
            };

            Links.Add(linkManageHome);

            var linkLanguages = new BreadCrumbLinkModel()
            {
                Area = "",
                IsActive = true,
                Name = "Languages",
                Page = "",
                Url = "/Manage/Languages"
            };

            Links.Add(linkLanguages);

            var linkTranslations = new BreadCrumbLinkModel()
            {
                Area = "",
                IsActive = true,
                Name = "Translations",
                Page = "",
                Url = "/Manage/Translations"
            };

            Links.Add(linkTranslations);
            var linkMedia = new BreadCrumbLinkModel()
            {
                Area = "",
                IsActive = true,
                Name = "Media",
                Page = "",
                Url = "/Manage/Media"
            };
            Links.Add(linkMedia);
        }
    }
}

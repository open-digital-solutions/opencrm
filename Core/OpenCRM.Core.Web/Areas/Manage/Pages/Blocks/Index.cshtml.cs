using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.Web.Components.Table;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Table;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.DataBlock
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        [BindProperty]
        public TableModel Table { get; set; } = new TableModel();

        public IndexModel() 
        {
            Links.Add(new BreadCrumbLinkModel()
            {
                Area = "",
                IsActive = true,
                Name = "Home",
                Page = "",
                Url = "/"
            });

            Links.Add(new BreadCrumbLinkModel()
            {
                Area = "",
                IsActive = true,
                Name = "Manage",
                Page = "",
                Url = "/Manage/Index"
            });

            Links.Add(new BreadCrumbLinkModel()
            {
                Area = "Manage",
                IsActive = true,
                Name = "Blocks List",
                Page = "",
                Url = "/Manage/Blocks"
            });
        }

        public void OnGet()
        {
		
		}
    }
}

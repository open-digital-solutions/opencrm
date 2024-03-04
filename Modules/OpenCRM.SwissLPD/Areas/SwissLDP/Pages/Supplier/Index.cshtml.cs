using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.Web.Components.Table;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Table;
using OpenCRM.SwissLPD.Services.EventService;

namespace OpenCRM.SwissLPD.Areas.SwissLDP.Pages.Supplier
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        public IndexModel() 
        {
            Links.Add(new BreadCrumbLinkModel()
            {
                Area = "SwissLDP",
                IsActive = true,
                Name = "Suppliers",
                Page = "Supplier",
                Url = "/SwissLDP/Supplier"
            });
        }

        public void OnGet()
        {
            
        }
    }
}

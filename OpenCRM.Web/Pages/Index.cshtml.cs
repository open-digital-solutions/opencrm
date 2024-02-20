using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Services.IdentityService;

namespace OpenCRM.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IIdentityService _identityService;

        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        public string? Lang { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IIdentityService identityService)
        {
            _logger = logger;
            _identityService = identityService;
            var link = new BreadCrumbLinkModel()
            {
                Area = "",
                IsActive = true,
                Name = "Home",
                Page = ""
            };

            Links.Add(link);
        }

        public void OnGet()
        {
            var dataSesison = _identityService.GetSession();
            if (dataSesison == null) Lang = "IT";
            Lang = dataSesison != null ? dataSesison.Lang : "Default dal browser";
        }
    }
}
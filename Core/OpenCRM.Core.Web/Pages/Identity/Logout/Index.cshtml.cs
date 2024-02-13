using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.Web.Services.IdentityService;

namespace OpenCRM.Core.Web.Pages.Identity.Logout
{
    public class IndexModel : PageModel
    {
        private readonly IIdentityService _identityService;
        public IndexModel(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task OnGetAsync()
        {
           await _identityService.Logout();
        }
    }
}

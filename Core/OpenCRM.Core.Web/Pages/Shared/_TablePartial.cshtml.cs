using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OpenCRM.Core.Web.Pages.Shared
{
    public class _TablePartialModel : PageModel
    {
        [BindProperty]
        public List<string> Headers { get; set; } = new List<string>();

        [BindProperty]
        public List<List<string>> Lines { get; set; } = new List<List<string>>();

        public _TablePartialModel() { }

        public void OnGet()
        {
        }
    }
}

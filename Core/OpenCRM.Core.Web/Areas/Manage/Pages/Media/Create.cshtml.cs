using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core;
using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Models;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.Media
{
    public class CreateModel : PageModel
    {
        private readonly IMediaService _mediaService;
        [BindProperty]
        public MediaModel Model { get; set; } = default!;


        public CreateModel(IMediaService mediaService)
        {
            _mediaService = mediaService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                await _mediaService.PostFileAsync(Model);
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}


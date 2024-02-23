using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Services;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.Media
{
    public class CreateModel : PageModel
    {
        private readonly IMediaService _mediaService;
        [BindProperty]
        public MediaModel Model { get; set; } = default!;
        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();


        public CreateModel(IMediaService mediaService)
        {
            _mediaService = mediaService;
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
                Area = "Manage",
                IsActive = true,
                Name = "Medias List",
                Page = "Media",
                Url = "/Manage"
            });

            Links.Add(new BreadCrumbLinkModel()
            {
                Area = "Manage",
                IsActive = true,
                Name = "Create Media",
                Page = "Media",
                Url = "/Manage/Media/Create"
            });
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


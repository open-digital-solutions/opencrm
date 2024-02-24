using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Services;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.Media
{
    public class DeleteModel : PageModel
    {
        private readonly IMediaService _mediaService;

        [BindProperty]
        public MediaEntity Model { get; set; } = default!;

        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        public DeleteModel(IMediaService mediaService)
        {
            _mediaService = mediaService;

            Links.Add( new BreadCrumbLinkModel
            {
                Area = "",
                IsActive = true,
                Name = "Home",
                Page = "",
                Url = "/"
            });

            Links.Add(new BreadCrumbLinkModel
            {
                Area = "Manage",
                IsActive = true,
                Name = "Media List",
                Page = "Media",
                Url = "/Manage"
            });
        }

        public IActionResult OnGet(Guid id)
        {
            var dataBlockModel = _mediaService.GetMedia(id);

            if (dataBlockModel == null)
            {
                return NotFound();
            }

            Model = dataBlockModel;
            return Page();
        }

        public  IActionResult OnPost(Guid id)
        {
            var dataBlockModel = _mediaService.GetMedia(id);

            if (dataBlockModel == null)
            {
                return NotFound();
            }
             _mediaService.RemoveMedia(id);
            return RedirectToPage("./Index");
        }
       
    }
}

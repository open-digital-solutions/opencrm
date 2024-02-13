using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Models;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.Media
{
    public class DetailsModel : PageModel
    {
        private readonly IMediaService _mediaService;

        [BindProperty]
        public MediaEntity Model { get; set; } = default!;

        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        public DetailsModel(IMediaService mediaService)
        {
            _mediaService = mediaService;

            var link = new BreadCrumbLinkModel()
            {
                Area = "",
                IsActive = true,
                Name = "Home",
                Page = "",
                Url = "/"
            };

            Links.Add(link);

            var link2 = new BreadCrumbLinkModel()
            {
                Area = "",
                IsActive = true,
                Name = "Media",
                Page = "",
                Url = "/Manage/Media"
            };

            Links.Add(link2);
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
        public IActionResult OnGetDownload(Guid id)
        {
            var media = _mediaService.GetMedia(id);
            if (media == null)
            {
                return NotFound();
            }
            return File(media.FileData, "application/octet-stream", media.FileName);

        }

    }
}

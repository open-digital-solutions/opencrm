using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Models;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.Media
{
    public class EditModel : PageModel
    {
        private readonly IMediaService _mediaService;

        [BindProperty]
        public MediaEntity Model { get; set; } = default!;

        [BindProperty]
        public IFormFile UploadedFile { get; set; } 


        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        public EditModel(IMediaService mediaService)
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

         public async Task<IActionResult> OnPost(Guid id)
            {
                if (ModelState.IsValid)
                {
                   if (UploadedFile != null)
                   {
                        using (var memoryStream = new MemoryStream())
                {
                            UploadedFile.CopyTo(memoryStream);
                            Model.FileData = memoryStream.ToArray();
                        }
                        string extension = Path.GetExtension(UploadedFile.FileName).ToLower();
                        Model.FileType = extension == ".pdf" ? MediaType.PDF : extension == ".docx" ? MediaType.DOCX : MediaType.GENERIC;
                  
                        await _mediaService.EditFileAsync(id,Model);
                        return RedirectToPage("./Index");
                    }
                }

            return Page();
        }
    }
}

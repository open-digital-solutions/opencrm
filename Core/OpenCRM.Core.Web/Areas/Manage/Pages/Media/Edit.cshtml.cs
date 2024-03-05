using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Services;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.Media
{
    public class EditModel : PageModel
    {
        private readonly IMediaService _mediaService;

        [BindProperty]
        public MediaEntity Model { get; set; } = default!;

        [BindProperty]
        public IFormFile? UploadedFile { get; set; }

        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        public EditModel(IMediaService mediaService)
        {
            _mediaService = mediaService;

            Links.Add(new BreadCrumbLinkModel()
            {
                Area = "Manage",
                IsActive = true,
                Name = "Medias",
                Page = "Media",
                Url = "/Manage/Media"
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
                    //var filename = UploadedFile.FileName ?? "UnknowFileName.generic";
                    //var extension = Path.GetExtension(filename);
                    var filename = UploadedFile.FileName;
                    string extension = Path.GetExtension(UploadedFile.FileName).ToLower();

                    Model.FileType = _mediaService.GetMediaType(extension);
                    Model.Extension= extension; 

                }
                await _mediaService.EditFileAsync(id, Model);
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}

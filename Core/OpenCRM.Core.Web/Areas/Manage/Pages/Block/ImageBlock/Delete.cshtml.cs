using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Models.BlockModels;
using OpenCRM.Core.Web.Services.ImageBlockService;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.Block.ImageBlock
{
    public class DeleteModel : PageModel
    {
		private IImageBlockService _imageBlockService;

		[BindProperty]
		public DataBlockModel<ImageBlockModel> ImageModel { get; set; } = default!;

        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        public DeleteModel(IImageBlockService imageBlockService) 
		{ 
			_imageBlockService = imageBlockService;

            Links.Add(new BreadCrumbLinkModel()
            {
                Area = "Manage",
                IsActive = true,
                Name = "Image Blocks",
                Page = "",
                Url = "/Manage/Block/ImageBlock"
            });
        }

		public async Task<IActionResult> OnGetAsync(Guid id)
		{
			var dataBlockModel = await _imageBlockService.GetBlock(id);

			if (dataBlockModel == null)
			{
				return NotFound();
			}

			ImageModel = dataBlockModel;
			return Page();
		}

		public async Task<IActionResult> OnPostAsync(Guid id)
		{
			var dataBlockModel = _imageBlockService.GetBlock(id);

			if (dataBlockModel == null)
			{
				return NotFound();
			}

			await _imageBlockService.RemoveBlock(id);
			return RedirectToPage("./Index");
		}
	}
}

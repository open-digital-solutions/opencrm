using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Services.CardBlockService;
using OpenCRM.Core.Web.Services;
using OpenCRM.Core.Web.Models.BlockModels;
using OpenCRM.Core.Web.Services.ImageBlockService;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.Block.ImageBlock
{
    public class CreateModel : PageModel
    {
		private IImageBlockService _imageBlockService;

		private readonly IMediaService _mediaService;

		public string ValidateError { get; set; } = string.Empty;

		public string ImageUrlSelected { get; set; } = string.Empty;

		[BindProperty]
		public ImageBlockModel ImageModel { get; set; } = default!;

		[BindProperty]
		public string ImageIdSelected { get; set; } = string.Empty;

		[BindProperty]
		public List<MediaBlockModel> Images { get; set; } = new List<MediaBlockModel>();

		[BindProperty]
		public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

		public CreateModel(IImageBlockService imageBlockService, IMediaService mediaService)
		{
			_imageBlockService = imageBlockService;
			_mediaService = mediaService;

			Links.Add(new BreadCrumbLinkModel()
			{
				Area = "Manage",
				IsActive = true,
				Name = "Image Blocks",
				Page = "",
				Url = "/Manage/Block/ImageBlock"
			});

			Images = _mediaService.GetImageMedias();
		}

		public IActionResult OnGet()
		{
			return Page();
		}

		public async Task<IActionResult> OnPost()
		{
			if (ModelState.IsValid)
			{
				var imageBlockModel = _imageBlockService.CreateBlockModel(ImageModel.Code, ImageIdSelected, ImageModel.With, ImageModel.Height);

				if (imageBlockModel != null)
				{
					var dataBlockModel = new DataBlockModel<ImageBlockModel>()
					{
						Code = imageBlockModel.Code,
						Description = imageBlockModel.Code,
						Type = typeof(ImageBlockModel).Name,
						Data = imageBlockModel  
					};

                    if (!string.IsNullOrEmpty(imageBlockModel.ImageUrl))
                    {
                        ImageUrlSelected = imageBlockModel.ImageUrl;
                    }

                    if (await _imageBlockService.AddBlock(dataBlockModel) == null)
                    {
                        ValidateError = $"Error to create new block: {ImageModel.Code}";
                        return Page();
                    }

                    return RedirectToPage("./Index");
				}
			}
			return Page();
		}
	}
}

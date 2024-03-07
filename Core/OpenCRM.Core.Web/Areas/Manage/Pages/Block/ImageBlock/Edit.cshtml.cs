using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Models.BlockModels;
using OpenCRM.Core.Web.Services;
using OpenCRM.Core.Web.Services.ImageBlockService;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.Block.ImageBlock
{
    public class EditModel : PageModel
    {
		private readonly IImageBlockService _imageBlockService;

		private readonly IMediaService _mediaService;

        public string ImageUrlSelected { get; set; } = string.Empty;

        [BindProperty]
		public ImageBlockModel ImageModel { get; set; } = default!;

        [BindProperty]
        public string? ImageIdSelected { get; set; }

        [BindProperty]
        public List<MediaBlockModel> Images { get; set; } = new List<MediaBlockModel>();

        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        public EditModel(IImageBlockService imageBlockService, IMediaService mediaService)
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

        public async Task<IActionResult> OnGetAsync(Guid id)
		{
			var dataBlockModel = await _imageBlockService.GetBlock(id);

			if (dataBlockModel == null)
			{
				return NotFound();
			}

			var showModel = new ImageBlockModel
			{
				Code = dataBlockModel.Data.Code,
				With = dataBlockModel.Data.With,
				Height = dataBlockModel.Data.Height,
				ImageUrl = dataBlockModel.Data.ImageUrl,
			};

			ImageModel = showModel;
			ImageUrlSelected = showModel.ImageUrl;  

			return Page();
		}

		public async Task<IActionResult> OnPost(Guid id)
		{
			if (ModelState.IsValid)
			{
				var blockModel = _imageBlockService.CreateBlockModel(ImageModel.Code, ImageIdSelected, ImageModel.With, ImageModel.Height);

				if (blockModel != null)
				{
					var dataBlockModelEdit = new DataBlockModel<ImageBlockModel>()
					{
						ID = id,
						Code = blockModel.Code,
						Description = blockModel.Code,
						Type = typeof(ImageBlockModel).Name,
						Data = blockModel
					};

					if (!string.IsNullOrEmpty(blockModel.ImageUrl))
					{
						ImageUrlSelected = blockModel.ImageUrl;
					}

					await _imageBlockService.EditBlock(dataBlockModelEdit);
					return RedirectToPage("./Index");
				}
			}
			return Page();
		}
	}
}

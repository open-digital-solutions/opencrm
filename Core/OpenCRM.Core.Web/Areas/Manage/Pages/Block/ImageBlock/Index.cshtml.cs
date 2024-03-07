using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Components.Table;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Models.BlockModels;
using OpenCRM.Core.Web.Services.ImageBlockService;
using OpenCRM.Core.Web.Table;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.Block.ImageBlock
{
    public class IndexModel : PageModel
    {
        private readonly IImageBlockService _imageBlockService;

        private readonly TableService<ImageBlockModel> _tableService = new TableService<ImageBlockModel>();

        [BindProperty]
        public DataBlockModel<ImageBlockModel> ImageModel { get; set; } = default!;

        [BindProperty]
        public List<DataBlockModel<ImageBlockModel>> BlocksList { get; set; } = new List<DataBlockModel<ImageBlockModel>>();

        [BindProperty]
        public TableModel Table { get; set; } = new TableModel("Image Blocks", "/Manage/Block/ImageBlock");

        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        public IndexModel(IImageBlockService imageBlockService) 
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

		public async Task OnGet()
		{
			var blocks = await _imageBlockService.GetImageBlocks();

			if (blocks != null)
			{
				BlocksList = blocks;

				var result = _tableService.BuildTable(BlocksList);
				Table.Headers = result.Item1;
				Table.Rows = result.Item2;
			}
		}
	}
}

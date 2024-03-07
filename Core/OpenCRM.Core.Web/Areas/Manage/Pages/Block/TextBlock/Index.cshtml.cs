using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Components.Table;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Services.BlockServices.TextBlockService;
using OpenCRM.Core.Web.Services.CardBlockService;
using OpenCRM.Core.Web.Table;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.Block.TextBlock
{
    public class IndexModel : PageModel
    {
		private ITextBlockService _textBlockService;

		private readonly TableService<TextBlockModel> _tableService = new TableService<TextBlockModel>();

		[BindProperty]
		public List<DataBlockModel<TextBlockModel>> BlocksList { get; set; } = new List<DataBlockModel<TextBlockModel>>();

		[BindProperty]
		public DataBlockModel<TextBlockModel> TextModel { get; set; } = default!;

		[BindProperty]
		public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

		[BindProperty]
		public TableModel Table { get; set; } = new TableModel("Text Blocks", "/Manage/Block/TextBlock");

		public IndexModel(ITextBlockService textBlockService)
		{
			_textBlockService = textBlockService;

            Links.Add(new BreadCrumbLinkModel()
            {
                Area = "Manage",
                IsActive = true,
                Name = "Text Blocks",
                Page = "",
                Url = "/Manage/Block/TextBlock"
            });
        }

		public async Task OnGet()
		{
			var blocks = await _textBlockService.GetTextBlocks();

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

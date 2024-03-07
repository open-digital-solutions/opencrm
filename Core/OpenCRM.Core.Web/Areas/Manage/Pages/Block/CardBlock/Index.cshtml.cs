using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Components.Table;
using OpenCRM.Core.Web.Models.BlockModels;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Services.BlockServices.BlockService;
using OpenCRM.Core.Web.Table;
using OpenCRM.Core.Web.Services.CardBlockService;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.Block.CardBlock
{
    public class IndexModel : PageModel
    {
		private readonly ICardBlockService _cardBlockService;

		private readonly TableService<CardBlockModel> _tableService = new TableService<CardBlockModel>();

		[BindProperty]
		public List<DataBlockModel<CardBlockModel>> BlocksList { get; set; } = new List<DataBlockModel<CardBlockModel>>();

		[BindProperty]
		public DataBlockModel<CardBlockModel> Model { get; set; } = default!;

		[BindProperty]
		public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

		[BindProperty]
		public TableModel Table { get; set; } = new TableModel("Card Blocks", "/Manage/Block/CardBlock");

		public IndexModel(ICardBlockService cardBlockService)
		{
			_cardBlockService = cardBlockService;

			Links.Add(new BreadCrumbLinkModel()
			{
				Area = "Manage",
				IsActive = true,
				Name = "Card Blocks",
				Page = "",
				Url = "/Manage/Block/CardBlock"
			});
		}

		public async Task OnGet()
		{
			var blocks = await _cardBlockService.GetCardBlocks();

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

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Components.Table;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Services.CardBlockService;
using OpenCRM.Core.Web.Table;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.DataBlock
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ICardBlockService _blockService;

        private readonly TableService<CardBlockModel> _tableService = new TableService<CardBlockModel>();

        [BindProperty]
        public List<DataBlockModel<CardBlockModel>> BlocksList { get; set; } = new List<DataBlockModel<CardBlockModel>>();
 
        [BindProperty]
        public DataBlockModel<CardBlockModel> Model { get; set; } = default!;

        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        [BindProperty]
        public TableModel Table { get; set; } = new TableModel("Blocks", "Block");

        public IndexModel(ICardBlockService blockService) 
        {
            _blockService = blockService;

            Links.Add(new BreadCrumbLinkModel()
            {
                Area = "",
                IsActive = true,
                Name = "Home",
                Page = "",
                Url = "/"
            });

            Links.Add(new BreadCrumbLinkModel()
            {
                Area = "",
                IsActive = true,
                Name = "Manage",
                Page = "",
                Url = "/Manage/Index"
            });

            Links.Add(new BreadCrumbLinkModel()
            {
                Area = "Manage",
                IsActive = true,
                Name = "Blocks",
                Page = "",
                Url = "/Manage/Block"
            });
        }

        public async Task OnGet()
        {
            var blocks = await _blockService.GetBlocks();

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

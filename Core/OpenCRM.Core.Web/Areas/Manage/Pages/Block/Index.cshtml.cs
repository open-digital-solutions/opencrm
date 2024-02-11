using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.DataBlock;
<<<<<<<< HEAD:Core/OpenCRM.Core.Web/Areas/Manage/Pages/Block/Index.cshtml.cs
using OpenCRM.Core.Web.Components.Table;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Services.BlockService;
using OpenCRM.Core.Web.Table;
========
using OpenCRM.Core.Web.Components.Block;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Services.BlockService;
>>>>>>>> b470273 (BlockModel, Block Component & CRUD):Core/OpenCRM.Core.Web/Areas/Manage/Pages/Block/Details.cshtml.cs

namespace OpenCRM.Core.Web.Areas.Manage.Pages.DataBlock
{
   [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IBlockService _blockService;

<<<<<<<< HEAD:Core/OpenCRM.Core.Web/Areas/Manage/Pages/Block/Index.cshtml.cs
        private readonly TableService<BlockModel> _tableService = new TableService<BlockModel>();

        [BindProperty]
        public List<DataBlockModel<BlockModel>> BlocksList { get; set; } = new List<DataBlockModel<BlockModel>>();
 
        [BindProperty]
        public DataBlockModel<BlockModel> Model { get; set; } = default!;

        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

<<<<<<<< HEAD:Core/OpenCRM.Core.Web/Areas/Manage/Pages/Block/Index.cshtml.cs
        [BindProperty]
        public TableModel Table { get; set; } = new TableModel("Blocks", "Block");

        public IndexModel(IBlockService blockService) 
========
        public DetailsModel(IBlockService blockService)
>>>>>>>> b470273 (BlockModel, Block Component & CRUD):Core/OpenCRM.Core.Web/Areas/Manage/Pages/Block/Details.cshtml.cs
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
<<<<<<<< HEAD:Core/OpenCRM.Core.Web/Areas/Manage/Pages/Block/Index.cshtml.cs
                Name = "Block",
========
                Name = "Blocks",
>>>>>>>> b470273 (BlockModel, Block Component & CRUD):Core/OpenCRM.Core.Web/Areas/Manage/Pages/Block/Details.cshtml.cs
                Page = "",
                Url = "/Manage/Block"
            });
        }

<<<<<<<< HEAD:Core/OpenCRM.Core.Web/Areas/Manage/Pages/Block/Index.cshtml.cs
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
========
        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var dataBlockModel = await _blockService.GetBlock(id);

            if (dataBlockModel == null)
            {
                return NotFound();
            }

            Model = dataBlockModel;
            return Page();
        }
>>>>>>>> b470273 (BlockModel, Block Component & CRUD):Core/OpenCRM.Core.Web/Areas/Manage/Pages/Block/Details.cshtml.cs
    }
}

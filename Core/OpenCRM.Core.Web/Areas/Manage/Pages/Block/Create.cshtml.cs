using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Components.Block;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Services.BlockService;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.DataBlock
{
    public class CreateModel : PageModel
    {
        private readonly IBlockService _blockService;

        [BindProperty]
        public BlockModel Model { get; set; } = default!;

        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        public CreateModel(IBlockService blockService)
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
                Url = "/Manage/Blocks"
            });


            Links.Add(new BreadCrumbLinkModel()
            {
                Area = "Manage",
                IsActive = true,
                Name = "Create Block",
                Page = "",
                Url = "/Manage/Blocks/Create"
            });
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var modelType = (Model.Image != null)? BlockType.Card : BlockType.Text;

                var blockModel = new BlockModel()
                {
                    Title = Model.Title,
                    SubTitle = Model.SubTitle,
                    Type = modelType,
                    Description = Model.Description,
                    Image = Model.Image,
                };

                var dataBlockModel = new DataBlockModel<BlockModel>()
                {
                    Name = blockModel.Title,
                    Description = blockModel.Title,
                    Type = typeof(BlockModel).Name,
                    Data = blockModel
                };

                _blockService.AddBlock(dataBlockModel);
                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Services.BlockService;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.DataBlock
{
    public class CreateModel : PageModel
    {
        private readonly IBlockService _blockService;

        [BindProperty]
        public string ImageName { get;set; } = string.Empty;

        [BindProperty]
        public BlockModel Model { get; set; } = default!;

        [BindProperty]
        [Column(TypeName = "jsonb")]
        public string? Description { get; set; }

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
                Name = "Block",
                Page = "",
                Url = "/Manage/Block"
            });


            Links.Add(new BreadCrumbLinkModel()
            {
                Area = "Manage",
                IsActive = true,
                Name = "Create Block",
                Page = "",
                Url = "/Manage/Block/Create"
            });
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public List<DescriptionItem> ToListItem()
        {
            var list = new List<DescriptionItem>();
            return list;
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var modelType = (ImageName != "")? BlockType.Card : BlockType.Text;

                var blockModel = new BlockModel()
                {
                    Title = Model.Title,
                    SubTitle = Model.SubTitle,
                    Type = modelType,
                    ImageId = Model.ImageId,
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

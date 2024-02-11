using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Components.Block;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Services.BlockService;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.DataBlock
{
    public class EditModel : PageModel
    {
        private readonly IBlockService _blockService;

        [BindProperty]
        public BlockModel Model { get; set; } = default!;

        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        public EditModel(IBlockService blockService)
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
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var dataBlockModel = await _blockService.GetBlock(id);

            if (dataBlockModel == null)
            {
                return NotFound();
            }

            Model = dataBlockModel.Data;
            return Page();
        }

        public IActionResult OnPost(Guid id)
        {
            if (ModelState.IsValid)
            {
                var dataBlockModel = _blockService.GetBlock(id);

                if (dataBlockModel == null)
                {
                    return NotFound();
                }

                if(Model.Image != null)
                {
                    Model.Type = BlockType.Card;
                }

                var dataBlockModelEdit = new DataBlockModel<BlockModel>()
                {
                    ID = id,
                    Name = Model.Title,
                    Description = Model.Title,
                    Type = typeof(BlockModel).Name,
                    Data = Model
                };

                _blockService.EditBlock(dataBlockModelEdit);
                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}

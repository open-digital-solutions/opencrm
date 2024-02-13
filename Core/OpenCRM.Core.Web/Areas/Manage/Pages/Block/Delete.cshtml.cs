using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Services.BlockService;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.DataBlock
{
    public class DeleteModel : PageModel
    {
        private readonly IBlockService _blockService;

        [BindProperty]
        public DataBlockModel<BlockModel> Model { get; set; } = default!;

        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        public DeleteModel(IBlockService blockService)
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

            var showModel = new BlockModel
            {
                Code = dataBlockModel.Data.Code,
                Title = dataBlockModel.Data.Title,
                SubTitle = dataBlockModel.Data.SubTitle,
                Description = dataBlockModel.Data.Description,
                ImageUrl = dataBlockModel.Data.ImageUrl,
            };

            Model.Data = showModel;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            var dataBlockModel = _blockService.GetBlock(id);

            if (dataBlockModel == null)
            {
                return NotFound();
            }

            await _blockService.RemoveBlock(id);
            return RedirectToPage("./Index");
        }
    }
}

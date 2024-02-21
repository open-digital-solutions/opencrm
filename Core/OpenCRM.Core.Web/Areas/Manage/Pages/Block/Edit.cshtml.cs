using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Extensions;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Services;
using OpenCRM.Core.Web.Services.BlockService;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.DataBlock
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly IBlockService _blockService;

        private readonly IMediaService _mediaService;

        public string ValidateError { get; set; } = string.Empty;

        public string ImageUrlSelected { get; set; } = string.Empty;

        [BindProperty]
        public string ImageIdSelected { get; set; } = string.Empty;

        [BindProperty]
        public List<MediaBlockModel> Images { get; set; } = new List<MediaBlockModel>();

        [BindProperty]
        public BlockModel Model { get; set; } = default!;

        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        public EditModel(IBlockService blockService, IMediaService mediaService)
        {
            _blockService = blockService;
            _mediaService = mediaService;

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

            Images = _mediaService.GetImageMedias();
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

            Model = showModel;

            if(showModel.ImageUrl != null)
            {
                ImageUrlSelected = showModel.ImageUrl;
            }


            if(showModel.ImageUrl != null)
            {
                ImageUrlSelected = showModel.ImageUrl;
            }

            return Page();
        }

        public async Task<IActionResult> OnPost(Guid id)
        {
            if (ModelState.IsValid)
            {
                var blockModel = _blockService.CreateBlockModel(Model.Code, Model.Title, Model.SubTitle, Model.Description, ImageIdSelected);

                var dataBlockModelEdit = new DataBlockModel<BlockModel>()
                {
                    Name = blockModel.Title,
                    Description = blockModel.Title,
                    Type = typeof(BlockModel).Name,
                    Data = blockModel
                };

                if (!string.IsNullOrEmpty(blockModel.ImageUrl))
                {
                    ImageUrlSelected = blockModel.ImageUrl;
                }

                await _blockService.EditBlock(dataBlockModelEdit);
                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}

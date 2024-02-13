using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Extensions;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Services;
using OpenCRM.Core.Web.Services.BlockService;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.DataBlock
{
    public class EditModel : PageModel
    {
        private readonly IBlockService _blockService;

        private readonly IMediaService _mediaService;

        public string MediaPublicDirPath { get; set; } = Path.Combine(OpenCRMEnv.GetWebRoot(), "media");

        [BindProperty]
        public IFormFile FileData { get; set; } = default!;

        [BindProperty]
        public bool IsPublic { get; set; } = false;

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

            Model = showModel;
            return Page();
        }

        public async Task<IActionResult> OnPost(Guid id)
        {
            if (ModelState.IsValid)
            {
                Guid imageID = default!;
                string imageUrl = "";
                var modelType = BlockType.Text;

                if (FileData != null)
                {
                    modelType = BlockType.Card;
                    var file = await _mediaService.PostFileAsync(FileData, IsPublic);
                    imageID = file.ID;

                    if (IsPublic)
                    {
                        imageUrl = _mediaService.GetMediaUrl(imageID.ToString());
                    }
                }

                var blockModel = new BlockModel()
                {
                    Code = Model.Code,
                    Title = Model.Title,
                    SubTitle = Model.SubTitle,
                    Type = modelType,
                    ImageId = imageID,
                    ImageUrl = imageUrl
                };

                var dataBlockModelEdit = new DataBlockModel<BlockModel>()
                {
                    ID = id,
                    Name = Model.Title,
                    Description = Model.Title,
                    Type = typeof(BlockModel).Name,
                    Data = blockModel
                };

                await _blockService.EditBlock(dataBlockModelEdit);
                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}

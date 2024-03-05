using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Services;
using OpenCRM.Core.Web.Services.CardBlockService;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.DataBlock
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ICardBlockService _blockService;

        private readonly IMediaService _mediaService;

        public string ValidateError { get; set; } = string.Empty;

        public string ImageUrlSelected { get; set; } = string.Empty;

        [BindProperty]
        public CardBlockModel Model { get; set; } = default!;

        [BindProperty]
        public string? ImageIdSelected { get; set; }

        [BindProperty]
        public List<MediaBlockModel> Images { get; set; } = new List<MediaBlockModel>();

        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        public CreateModel(ICardBlockService blockService, IMediaService mediaService)
        {
            _blockService = blockService;
            _mediaService = mediaService;

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

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var blockModel = _blockService.CreateBlockModel(Model.Code, Model.Title, Model.SubTitle, Model.Description, ImageIdSelected);
             
                var dataBlockModel = new DataBlockModel<CardBlockModel>()
                {
                    Code = blockModel.Title,
                    Description = blockModel.Title,
                    Type = typeof(CardBlockModel).Name,
                    Data = blockModel
                };

                if (!string.IsNullOrEmpty(blockModel.ImageUrl))
                {
                    ImageUrlSelected = blockModel.ImageUrl;
                }

                if (await _blockService.AddBlock(dataBlockModel) == null)
                {
                    ValidateError = "Block with code " + Model.Code + " already exists";
                    return Page();
                }
                
                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}

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
        private readonly ICardBlockService _cardBlockService;

        private readonly IMediaService _mediaService;

        public string ValidateError { get; set; } = string.Empty;

        public string ImageUrlSelected { get; set; } = string.Empty;

        [BindProperty]
        public CardBlockModel CardModel { get; set; } = default!;

        [BindProperty]
        public string? ImageIdSelected { get; set; }

        [BindProperty]
        public List<MediaBlockModel> Images { get; set; } = new List<MediaBlockModel>();

        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        public CreateModel(ICardBlockService cardBlockService, IMediaService mediaService)
        {
            _cardBlockService = cardBlockService;
            _mediaService = mediaService;

            Links.Add(new BreadCrumbLinkModel()
            {
                Area = "Manage",
                IsActive = true,
                Name = "Card Blocks",
                Page = "",
                Url = "/Manage/Block/CardBlock"
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
                var blockModel = _cardBlockService.CreateBlockModel(CardModel.Code, CardModel.Title, CardModel.SubTitle, CardModel.Description, ImageIdSelected);

                if (blockModel != null)
                {
                    var dataBlockModel = new DataBlockModel<CardBlockModel>()
                    {
                        Code = blockModel.Title,
                        Description = blockModel.Title,
                        Type = typeof(CardBlockModel).Name,
                        Data = blockModel
                    };

                    if (await _cardBlockService.AddBlock(dataBlockModel) == null)
                    {
                        ValidateError = "Block with code " + CardModel.Code + " already exists";
                        return Page();
                    }

                    return RedirectToPage("./Index");
                }
            }
            return Page();
        }
    }
}

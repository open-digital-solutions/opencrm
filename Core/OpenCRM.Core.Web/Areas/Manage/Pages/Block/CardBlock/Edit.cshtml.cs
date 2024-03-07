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
    public class EditModel : PageModel
    {
        private readonly ICardBlockService _cardBlockService;

        private readonly IMediaService _mediaService;

        public string ValidateError { get; set; } = string.Empty;

        public string ImageUrlSelected { get; set; } = string.Empty;

        [BindProperty]
        public string? ImageIdSelected { get; set; }

        [BindProperty]
        public List<MediaBlockModel> Images { get; set; } = new List<MediaBlockModel>();

        [BindProperty]
        public CardBlockModel Model { get; set; } = default!;

        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        public EditModel(ICardBlockService cardBlockService, IMediaService mediaService)
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

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var dataBlockModel = await _cardBlockService.GetBlock(id);

            if (dataBlockModel == null)
            {
                return NotFound();
            }

            var showModel = new CardBlockModel
            {
                Code = dataBlockModel.Data.Code,
                Title = dataBlockModel.Data.Title,
                SubTitle = dataBlockModel.Data.SubTitle,
                Description = dataBlockModel.Data.Description,
                ImageUrl = dataBlockModel.Data.ImageUrl,
            };

            Model = showModel;

            if (showModel.ImageUrl != null)
            {
                ImageUrlSelected = showModel.ImageUrl;
            }

            return Page();
        }

        public async Task<IActionResult> OnPost(Guid id)
        {
            if (ModelState.IsValid)
            {
                var blockModel = _cardBlockService.CreateBlockModel(Model.Code, Model.Title, Model.SubTitle, Model.Description, ImageIdSelected);

                if (blockModel != null)
                {
                    var dataBlockModelEdit = new DataBlockModel<CardBlockModel>()
                    {
                        ID = id,
                        Code = blockModel.Code,
                        Description = blockModel.Title,
                        Type = typeof(CardBlockModel).Name,
                        Data = blockModel
                    };

                    if (!string.IsNullOrEmpty(blockModel.ImageUrl))
                    {
                        ImageUrlSelected = blockModel.ImageUrl;
                    }

                    await _cardBlockService.EditBlock(dataBlockModelEdit);
                    return RedirectToPage("./Index");
                }
            }
            return Page();
        }
    }
}

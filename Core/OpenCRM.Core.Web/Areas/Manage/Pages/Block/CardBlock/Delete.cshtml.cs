using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Services.CardBlockService;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.DataBlock
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly ICardBlockService _cardBlockService;

        [BindProperty]
        public string ImageName { get; set; } = string.Empty;

        [BindProperty]
        public DataBlockModel<CardBlockModel> CardModel { get; set; } = default!;

        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        public DeleteModel(ICardBlockService cardBlockService)
        {
            _cardBlockService = cardBlockService;

			Links.Add(new BreadCrumbLinkModel()
			{
				Area = "Manage",
				IsActive = true,
				Name = "Card Blocks",
				Page = "",
				Url = "/Manage/Block/CardBlock"
			});
		}

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var dataBlockModel = await _cardBlockService.GetBlock(id);

            if (dataBlockModel == null)
            {
                return NotFound();
            }

            CardModel = dataBlockModel;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            var dataBlockModel = _cardBlockService.GetBlock(id);

            if (dataBlockModel == null)
            {
                return NotFound();
            }

            await _cardBlockService.RemoveBlock(id);
            return RedirectToPage("./Index");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Services.BlockServices.TextBlockService;
using OpenCRM.Core.Web.Services.CardBlockService;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.Block.TextBlock
{
    public class DetailsModel : PageModel
    {
		private ITextBlockService _textBlockService;

        [BindProperty]
        public DataBlockModel<TextBlockModel> TextModel { get; set; } = default!;

        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        public DetailsModel(ITextBlockService textBlockService)
        {
            _textBlockService = textBlockService;

            Links.Add(new BreadCrumbLinkModel()
            {
                Area = "Manage",
                IsActive = true,
                Name = "Blocks",
                Page = "",
                Url = "/Manage/Block"
            });
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var dataBlockModel = await _textBlockService.GetBlock(id);

            if (dataBlockModel == null)
            {
                return NotFound();
            }

            TextModel = dataBlockModel;
            return Page();
        }
    }
}

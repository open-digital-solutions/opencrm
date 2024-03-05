using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Services.TextBlockService;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.Block.TextBlock
{
	public class EditModel : PageModel
    {
		private ITextBlockService _textBlockService;

        public string ValidateError { get; set; } = string.Empty;

        [BindProperty]
        public TextBlockModel TextModel { get; set; } = default!;

        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        public EditModel(ITextBlockService textBlockService)
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

			TextModel = dataBlockModel.Data;
            return Page();
        }

        public async Task<IActionResult> OnPost(Guid id)
        {
            if (ModelState.IsValid)
            {
                var textBlockModel = new TextBlockModel()
                {
                    Code = TextModel.Code,
                    Description = TextModel.Description
                };

                var dataBlockModel = new DataBlockModel<TextBlockModel>()
                {
                    ID = id,
                    Code = textBlockModel.Code,
                    Description = textBlockModel.Code,
                    Type = typeof(TextBlockModel).Name,
                    Data = textBlockModel
                };

                await _textBlockService.EditBlock(dataBlockModel);
                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}

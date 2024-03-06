using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Services.BlockServices.TextBlockService;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.Block.TextBlock
{
    public class CreateModel : PageModel
    {
        private ITextBlockService _textBlockService;

        public string ValidateError { get; set; } = string.Empty;

        [BindProperty]
        public TextBlockModel TextModel { get; set; } = default!;

        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        public CreateModel(ITextBlockService textBlockService) 
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

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPost()
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
                    Code = textBlockModel.Code,
                    Description = textBlockModel.Code,
                    Type = typeof(TextBlockModel).Name,
                    Data = textBlockModel
                };

                if (await _textBlockService.AddBlock(dataBlockModel) == null)
                {
                    ValidateError = $"Error to create new block: {TextModel.Code}, {TextModel.Description}";
                    return Page();
                }

                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}

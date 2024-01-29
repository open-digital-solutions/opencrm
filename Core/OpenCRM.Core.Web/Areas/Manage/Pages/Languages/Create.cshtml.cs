using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OpenCRM.Core.Web;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Data;
using OpenCRM.Core.Web.Services.LanguageService;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.Languages
{
    public class CreateModel : PageModel
    {
        private readonly ILanguageService _languageService;

        public CreateModel(ILanguageService languageService)
        {
            _languageService = languageService;        
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public LanguageModel<LanguageEntity> Language { get; set; } = default!;

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var languageModel = new LanguageModel<LanguageEntity>()
                {
                    ID = Guid.NewGuid(),
                    Code = Language.Code,
                    Name = Language.Name,
                };
                await _languageService.AddLanguage(languageModel);
                return RedirectToAction("./Index");
            }
            return Page();
        }

    }
}

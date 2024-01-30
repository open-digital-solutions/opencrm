using Microsoft.AspNetCore.Mvc;
using OpenCRM.Core.Data;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Services.LanguageService;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.Languages
{
    public class DeleteModel : CorePageModel
    {
        private readonly ILanguageService _languageService;

        public DeleteModel(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        [BindProperty]
        public LanguageModel<LanguageEntity> Language { get; set; } = default!;

        public async Task<IActionResult> OnGet(Guid id)
        {            
            var languageModel = await _languageService.GetLanguageAsync<LanguageEntity>(id);
            if (languageModel == null) 
            {
                return NotFound();
            }
            Language = languageModel;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            var language = await _languageService.GetLanguageAsync<LanguageEntity>(id);
            if (language == null)
            {
                return NotFound();
            }
            await _languageService.DeleteLanguage<LanguageEntity>(id);
            return RedirectToPage("./Index");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.Data;
using OpenCRM.Core.Web.Services.LanguageService;
using OpenCRM.Core.Web.Services.TranslationService;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.Translations
{
    public class DetailsModel : PageModel
    {
        
            private readonly ITranslationService  _translationService;

            public DetailsModel(ITranslationService translationService)
            {
                _translationService = translationService;
            }

            public TranslationModel<TranslationEntity> Translation { get; set; } = default!;

            public async Task<IActionResult> OnGet(Guid id)
            {
                var translationModel = await _translationService.GetTranslationAsync<TranslationEntity>(id);
                if (translationModel == null)
                {
                    return NotFound();
                }
                Translation = translationModel;
                await _translationService.GetTranslationAsync<TranslationEntity>(id);
                return Page();
            }
       
    }
}

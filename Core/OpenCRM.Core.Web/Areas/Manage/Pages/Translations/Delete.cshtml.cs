using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.Data;
using OpenCRM.Core.Web.Services.TranslationService;
using Microsoft.EntityFrameworkCore;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Pages;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.Translations
{
    public class DeleteModel : PageModel
    {
        private readonly ITranslationService _translationService;

        public DeleteModel(ITranslationService translationService)
        {
            _translationService = translationService;
        }

        [BindProperty]
        public TranslationModel<TranslationEntity> Translation { get; set; } = default!;

        public async Task<IActionResult> OnGet(Guid id)
        {
            var translationModel = await _translationService.GetTranslationAsync<TranslationEntity>(id);
            if (translationModel == null)
            {
                return NotFound();
            }
            Translation = translationModel;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            var translation = _translationService.GetTranslationAsync<TranslationEntity>(id);
            if (translation == null)
            {
                return NotFound();
            }
            await _translationService.DeleteTranslation<TranslationEntity>(id);
            return RedirectToPage("./Index");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.Data;
using OpenCRM.Core.Web.Services.TranslationService;
using Microsoft.EntityFrameworkCore;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Pages;
using OpenCRM.Core.Web.Services.LanguageService;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.Translations
{
    public class DeleteModel : PageModel
    {
        private readonly ITranslationService _translationService;

        [BindProperty]
        public string Key { get; set; } = string.Empty;

        [BindProperty]
        public List<TranslationLanguageCodeModel> Translations { get; set; } = new List<TranslationLanguageCodeModel>();

        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        public DeleteModel(ITranslationService translationService)
        {
            _translationService = translationService;

            Links.Add(new BreadCrumbLinkModel()
            {
                Area = "Manage",
                IsActive = true,
                Name = "Translations",
                Page = "Translations",
                Url = "/Manage/Translations"
            });
        }        

        public async Task<IActionResult> OnGet(Guid id)
        {
            var transModel = await _translationService.GetTranslationByIdAsync<TranslationEntity>(id);

            if (transModel == null)
            {
                return NotFound();
            }

            var translations = _translationService.GetKeyTranslations<TranslationEntity>(transModel.Key);

            if (translations == null)
            {
                return NotFound();
            }

            Key = transModel.Key;
            Translations = translations;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            var translation = await _translationService.GetTranslationByIdAsync<TranslationEntity>(id);
            
            if (translation == null)
            {
                return NotFound();
            }
            
            await _translationService.DeleteTranslation<TranslationEntity>(id);
            return RedirectToPage("./Index");
        }
    }
}

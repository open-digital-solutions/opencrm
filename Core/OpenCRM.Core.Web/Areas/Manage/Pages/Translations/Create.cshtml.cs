using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.Data;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Services.LanguageService;
using OpenCRM.Core.Web.Services.TranslationService;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.Translations
{
    public class CreateModel : PageModel
    {
        private readonly ITranslationService _translationService;

        private readonly ILanguageService _languageService;

        [BindProperty]
        public string Key { get; set; } = string.Empty;

        [BindProperty]
        public List<TranslationLanguageCodeModel> Translations { get; set; } = new List<TranslationLanguageCodeModel>();


        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        public CreateModel(ITranslationService translationService, ILanguageService languageService)
        {
            _translationService = translationService;
            _languageService = languageService;

            Links.Add(new BreadCrumbLinkModel()
            {
                Area = "Manage",
                IsActive = true,
                Name = "Translations",
                Page = "Translations",
                Url = "/Manage/Translations"
            });
            _languageService = languageService;
        }

        public IActionResult OnGet()
        {
            var languages = _languageService.GetLanguageListAsync();

            if(languages == null)
            {
                return NotFound();
            }

            var translations = new List<TranslationLanguageCodeModel>();

            foreach(var language in languages)
            {
                translations.Add(new TranslationLanguageCodeModel()
                {
                    LanguageCode = language.Code,
                    LanguageId = language.ID
                });
            }

            Translations = translations;
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                await _translationService.AddKeysTranslations(Key, Translations);
                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}

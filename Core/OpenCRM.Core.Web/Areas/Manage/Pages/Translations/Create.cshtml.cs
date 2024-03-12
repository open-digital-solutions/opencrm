using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
        public List<TranslationByLanguage> Translations { get; set; } = default!;

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

            var translations = new List<TranslationByLanguage>();

            foreach(var language in languages)
            {
                translations.Add(new TranslationByLanguage()
                {
                    ID = Guid.Empty,
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
                var model = new TranslationModel()
                {
                    Key = Key,
                    Translations = Translations
                };
                await _translationService.AddTranslations(model);
                return RedirectToPage("./Index");
            }
            return Page();
		}
	}
}

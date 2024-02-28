using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.Data;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Services.LanguageService;
using OpenCRM.Core.Web.Services.TranslationService;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.Translations
{
    public class DetailsModel : PageModel
    {        
        private readonly ITranslationService  _translationService;

        private readonly ILanguageService _languageService;

        [BindProperty]
        public string Key { get; set; } = string.Empty;

        [BindProperty]
        public TranslationLanguageCodeModel Translation { get; set; } = default!;

        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        public DetailsModel(ITranslationService translationService, ILanguageService languageService)
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
        }

        public async Task<IActionResult> OnGet(Guid id)
        {
            var translationModel = await _translationService.GetTranslationByIdAsync<TranslationEntity>(id);

            if (translationModel == null)
            {
                return NotFound();
            }

            var language = await _languageService.GetLanguage(translationModel.LanguageId);
            Translation = new TranslationLanguageCodeModel();
            
            if (language != null)
            {
                Translation.LanguageCode = language.Code;
            }

            Key = translationModel.Key;
            Translation.Translation = translationModel.Translation;

            await _translationService.GetTranslationByIdAsync<TranslationEntity>(id);
            return Page();
        }         
    }
}

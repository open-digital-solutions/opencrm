using OpenCRM.Core.Web.Services.LanguageService;
using OpenCRM.Core.Web.Services.TranslationService;

namespace OpenCRM.Web.Services
{
    public class MyTranslationsSeederService
    {
        private readonly ITranslationService _translationService;
        private readonly ILanguageService _languageService;
        public MyTranslationsSeederService(ITranslationService translationService, ILanguageService languageService)
        {
            _translationService  = translationService;
            _languageService = languageService; 
        }
        public async Task SeedTranslationsAsync() {
            var languages = _languageService.GetLanguageListAsync();
            var italialLanguage = languages?.FirstOrDefault(l => l.Code == "IT");

            if (italialLanguage != null)
            {
                ItalianTranslations italianTranslations = new();
                var translations = italianTranslations.Translations;

                foreach (var translation in translations)
                {
                    await _translationService.AddTranslations(translation.Key);
                }
            }
        }
    }
}

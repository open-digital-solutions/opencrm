using OpenCRM.Core.Web.Models;
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

        public async Task SeedTranslationsAsync() 
        {
            var languages = _languageService.GetLanguageListAsync();
            var italialLanguage = languages?.FirstOrDefault(l => l.Code == "IT");

            if (italialLanguage != null)
            {
                ItalianTranslations italianTranslations = new();
                var translations = italianTranslations.Translations;

                foreach (var translation in translations)
                {
                    if (translation == null || string.IsNullOrEmpty(translation.Key)) continue;
                    //TODO: Modificar este metodo en el core. Tiene que permitir como lista de traducciones para la llave: 1 = null, Algunas o Todas las tradcciones por idiomas.
                    //El metodo debe de utilizar las traducciones por idioma que se le pasen si una traduccion para un idioma no se lasa tiene que utilizar la misma key como valor y NO romperse
                    await _translationService.AddTranslations(new TranslationModel()
                    {
                        Key = translation.Key,
                    });
                }
            }
        }
    }
}

using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Models;

namespace OpenCRM.Core.Web.Services.TranslationService
{
    public interface ITranslationService
    {
        Task<List<TranslationModel>?> AddTranslations(string key);
		Task<List<TranslationModel>?> AddTranslations(string key, List<TranslationLanguageCodeModel> keyTranslations);
		Task<List<TranslationModel>?> EditTranslations(string key, List<TranslationLanguageCodeModel> keyTranslations);
		Task DeleteTranslation(string key, List<TranslationLanguageCodeModel> keyTranslations);
        Task<TranslationModel?> GetTranslationById(Guid id);
        List<TranslationModel>? GetTranslationsByKey(string key);
		List<TranslationModel>? GetTranslationsList();
		List<TranslationLanguageCodeModel>? GetTranslationsWithLanguagesCode(string key);
		Dictionary<string, List<TranslationLanguageCodeModel>>? GetTranslationsToDictionary();
		List<DataBlockModel<TranslationLanguageCodeModel>> ToListDataBlockModel(List<TranslationModel> translations);
        Task<string?> GetTranslationValue(string key);
        Task Seed();
    }
}
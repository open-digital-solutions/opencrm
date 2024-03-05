using OpenCRM.Core.Data;
using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Models;

namespace OpenCRM.Core.Web.Services.TranslationService
{
    public interface ITranslationService
    {
		Task<List<TranslationModel>?> AddKeysTranslations(string key, List<TranslationLanguageCodeModel> keyTranslations);

		Task<List<TranslationModel>?> EditKeysTranslations(string key, List<TranslationLanguageCodeModel> keyTranslations);
		Task DeleteKeysTranslation(string key, List<TranslationLanguageCodeModel> keyTranslations);
		Dictionary<string, List<TranslationLanguageCodeModel>>? GetKeysTranslations();
        List<TranslationLanguageCodeModel>? GetKeyTranslations(string key);
        Task<TranslationModel?> GetTranslationByIdAsync(Guid id);
        List<TranslationModel>? GetTranslationListAsync();
        List<TranslationModel>? GetTranslationsByKey(string key);
        List<DataBlockModel<TranslationLanguageCodeModel>> ToListDataBlockModel(List<TranslationModel> translations);
        Task<string?> GetTranslationValueAsync(string key);
        Task Seed();
    }
}
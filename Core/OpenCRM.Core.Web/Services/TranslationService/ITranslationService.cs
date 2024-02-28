using OpenCRM.Core.Web.Models;

namespace OpenCRM.Core.Web.Services.TranslationService
{
    public interface ITranslationService
    {
        Task<TranslationModel<TDataModel>?> AddTranslation<TDataModel>(TranslationModel<TDataModel> model);
        Task DeleteKeysTranslation<TDataModel>(string key, List<TranslationLanguageCodeModel> keyTranslations);
        Task DeleteTranslation<TDataModel>(Guid Id);
        Task EditKeysTranslations<TDataModel>(string key, List<TranslationLanguageCodeModel> keyTranslations);
        Task<TranslationModel<TDataModel>?> EditTranslation<TDataModel>(TranslationModel<TDataModel> model);
        Dictionary<string, List<TranslationLanguageCodeModel>>? GetKeysTranslations<TDataModel>();
        List<TranslationLanguageCodeModel>? GetKeyTranslations<TDataModel>(string key);
        Task<TranslationModel<TranslationEntity>?> GetTranslationByIdAsync<TranslationEntity>(Guid id);
        List<TranslationModel<TDataModel>>? GetTranslationListAsync<TDataModel>();
        List<TranslationModel<TDataModel>>? GetTranslationsByKey<TDataModel>(string key);
        Task<string?> GetTranslationValueAsync(string key);
        Dictionary<string, string> KeyTranslationsValueToString(Dictionary<string, List<TranslationLanguageCodeModel>> keyTranslations);
        Task Seed();
    }
}
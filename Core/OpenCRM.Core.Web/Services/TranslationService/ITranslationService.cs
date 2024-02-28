using OpenCRM.Core.Web.Models;

namespace OpenCRM.Core.Web.Services.TranslationService
{
    public interface ITranslationService
    {
        Task<TranslationModel<TDataModel>?> AddTranslation<TDataModel>(TranslationModel<TDataModel> model);
        Task<TranslationModel<TDataModel>?> EditTranslation<TDataModel>(TranslationModel<TDataModel> model);
        Task EditKeyTranslations<TDataModel>(string key, List<TranslationLanguageCodeModel> keyTranslations);
        Task DeleteTranslation<TDataModel>(Guid Id);
        Task<TranslationModel<TranslationEntity>?> GetTranslationByIdAsync<TranslationEntity>(Guid id);
        List<TranslationModel<TDataModel>>? GetTranslationsByKey<TDataModel>(string key);
        List<TranslationModel<TDataModel>>? GetTranslationListAsync<TDataModel>();
        List<TranslationLanguageCodeModel>? GetKeyTranslations<TDataModel>(string key);
        Dictionary<string, List<TranslationLanguageCodeModel>>? GetKeysTranslations<TDataModel>();
        Dictionary<string, string> KeyTranslationsValueToString(Dictionary<string, List<TranslationLanguageCodeModel>> keyTranslations);
        string? GetTranslationValue(string key);
        Task Seed();
    }
}
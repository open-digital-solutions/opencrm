using OpenCRM.Core.Data;
using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Models;

namespace OpenCRM.Core.Web.Services.TranslationService
{
    public interface ITranslationService
    {
        Task<TranslationModel<TDataModel>?> AddTranslation<TDataModel>(TranslationModel<TDataModel> model);
        Task<TranslationModel<TDataModel>?> EditTranslation<TDataModel>(TranslationModel<TDataModel> model);
        Task DeleteTranslation<TDataModel>(Guid Id);
        Task AddKeysTranslations<TDataModel>(string key, List<TranslationLanguageCodeModel> keyTranslations);
        Task DeleteKeysTranslation<TDataModel>(string key, List<TranslationLanguageCodeModel> keyTranslations);
        Task EditKeysTranslations<TDataModel>(string key, List<TranslationLanguageCodeModel> keyTranslations);
        Dictionary<string, List<TranslationLanguageCodeModel>>? GetKeysTranslations<TDataModel>();
        List<TranslationLanguageCodeModel>? GetKeyTranslations<TDataModel>(string key);
        Task<TranslationModel<TranslationEntity>?> GetTranslationByIdAsync<TranslationEntity>(Guid id);
        List<TranslationModel<TDataModel>>? GetTranslationListAsync<TDataModel>();
        List<TranslationModel<TDataModel>>? GetTranslationsByKey<TDataModel>(string key);
        List<DataBlockModel<TranslationLanguageCodeModel>> ToListDataBlockModel(List<TranslationModel<TranslationEntity>> translations);
        Task<string?> GetTranslationValueAsync(string key);
        Task Seed();
    }
}
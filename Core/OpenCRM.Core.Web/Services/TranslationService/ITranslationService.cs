using OpenCRM.Core.Web.Models;

namespace OpenCRM.Core.Web.Services.TranslationService
{
    public interface ITranslationService
    {
        Task<TranslationModel<TDataModel>?> AddTranslation<TDataModel>(TranslationModel<TDataModel> model);
        Task<TranslationModel<TDataModel>?> EditTranslation<TDataModel>(TranslationModel<TDataModel> model);
        Task DeleteTranslation<TDataModel>(Guid Id);
        Task<TranslationModel<TranslationEntity>?> GetTranslationAsync<TranslationEntity>(Guid id);
        List<TranslationModel<TDataModel>>? GetTranslationListAsync<TDataModel>();
        Task<Dictionary<string, List<TranslationByLanguage>>?> GetKeyTranslationsByLanguage<TDataModel>();
        string? GetTranslationValue(string key);
        Task Seed();
    }
}
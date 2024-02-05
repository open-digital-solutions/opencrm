//using OpenDHS.Shared;
using OpenCRM.Core.Web.Models;

namespace OpenCRM.Core.Web.Services.LanguageService
{
    public interface ILanguageService
    {
        Task<LanguageModel<TTranslationModel>?> AddLanguage<TTranslationModel>(LanguageModel<TTranslationModel> model) where TTranslationModel : TranslationModel, new();
        Task addLanguageSeedAsync(string Code, string Name);
        Task DeleteLanguage<TDataModel>(Guid Id);
        Task<LanguageModel<TTranslationModel>?> EditLanguage<TTranslationModel>(LanguageModel<TTranslationModel> model) where TTranslationModel : TranslationModel, new();
        Task<LanguageModel<TTranslationModel>?> GetLanguage<TTranslationModel>(Guid id) where TTranslationModel : TranslationModel, new();
        Task<LanguageModel<TranslationModel>?> GetLanguageAsync<LanguageEntity>(Guid id);
        List<LanguageModel<TTranslationModel>> GetLanguageListAsync<TTranslationModel>() where TTranslationModel : TranslationModel, new();
        Task SeedAsync();
    }
}
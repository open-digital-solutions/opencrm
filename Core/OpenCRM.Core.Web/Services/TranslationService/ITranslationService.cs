namespace OpenCRM.Core.Web.Services.TranslationService
{
    public interface ITranslationService
    {
        Task<TranslationModel<TDataModel>?> AddTranslation<TDataModel>(TranslationModel<TDataModel> model);
        Task DeleteTranslation<TDataModel>(Guid Id);
        Task<TranslationModel<TDataModel>?> EditTranslation<TDataModel>(TranslationModel<TDataModel> model);
        Task<TranslationModel<TranslationEntity>?> GetTranslationAsync<TranslationEntity>(Guid id);
        List<TranslationModel<TDataModel>> GetTranslationListAsync<TDataModel>();
        string? GetTranslationValue(string key);
        Task Seed();
    }
}
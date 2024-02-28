using OpenCRM.Core.Data;

namespace OpenCRM.Core.Web.Services.LanguageService
{
    public interface ILanguageService
    {
        Task<LanguageModel?> AddLanguage(LanguageModel model);
        Task DeleteLanguage(Guid Id);
        Task<LanguageModel?> EditLanguage(LanguageModel model);
        Task<LanguageEntity?> GetCurrentLanguage();
        Task<LanguageModel?> GetLanguage(Guid id);
        List<LanguageModel>? GetLanguageListAsync();
        Task Seed();
    }
}
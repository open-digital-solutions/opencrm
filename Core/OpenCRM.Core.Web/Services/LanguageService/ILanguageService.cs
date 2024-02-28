using OpenCRM.Core.Data;

namespace OpenCRM.Core.Web.Services.LanguageService
{
    public interface ILanguageService
    {
        Task<LanguageService.LanguageModel?> AddLanguage(LanguageService.LanguageModel model);
        Task DeleteLanguage(Guid Id);
        Task<LanguageService.LanguageModel?> EditLanguage(LanguageService.LanguageModel model);
        Task<LanguageEntity?> GetCurrentLanguage();
        Task<LanguageService.LanguageModel?> GetLanguage(Guid id);
        List<LanguageService.LanguageModel>? GetLanguageListAsync();
        Task Seed();
    }
}
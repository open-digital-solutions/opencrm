//using OpenDHS.Shared;
using OpenCRM.Core.Web.Models;

namespace OpenCRM.Core.Web.Services.LanguageService
{
    public interface ILanguageService
    {
        Task<LanguageModel?> AddLanguage(LanguageModel model);
        Task<LanguageModel?> EditLanguage(LanguageModel model);
        Task DeleteLanguage(Guid Id);
        Task<LanguageModel?> GetLanguage(Guid id);
        List<LanguageModel>? GetLanguageListAsync();
        Task SeedAsync();
    }
}
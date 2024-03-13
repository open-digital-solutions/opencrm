using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Models;

namespace OpenCRM.Core.Web.Services.TranslationService
{
    public interface ITranslationService
    {
        Task<TranslationModel?> AddTranslations(TranslationModel model);
        Task DeleteTranslation(string key);
        Task<TranslationModel?> EditTranslations(TranslationModel model);
        Task<string?> GetTranslationKey(Guid id);
        List<TranslationByLanguage>? GetTranslationsByKey(string key);
        List<TranslationByLanguage>? GetTranslationsList();
        Dictionary<string, List<TranslationByLanguage>>? GetTranslationsToDictionary();
        Task<string?> GetTranslationValue(string key);
        Task Seed();
        List<DataBlockModel<TranslationByLanguage>> ToListDataBlockModel(List<TranslationByLanguage> keyTranslations);
    }
}
using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Models;

namespace OpenCRM.Core.Web.Services.TranslationService
{
    public interface ITranslationService
    {
		Task<TranslationModel?> AddTranslations(TranslationModel model);
		Task<TranslationModel?> EditTranslations(TranslationModel model);
		Task DeleteTranslation(string key);
        Task<string?> GetTranslationKey(Guid id);
        List<TranslationByLanguage>? GetTranslationsByKey(string key);
		List<TranslationByLanguage>? GetTranslationsList();
		Dictionary<string, List<TranslationByLanguage>>? GetTranslationsToDictionary();
		List<DataBlockModel<TranslationByLanguage>> ToListDataBlockModel(List<TranslationByLanguage> keyTranslations);
		Task<string?> GetTranslationValue(string key); //esto deberia devolver una lista de traducciones
        Task Seed();
    }
}
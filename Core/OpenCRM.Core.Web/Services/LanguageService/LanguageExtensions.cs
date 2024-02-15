using OpenCRM.Core.Data;
using OpenCRM.Core.Web.Models;
using System.Text.Json;

namespace OpenCRM.Core.Web.Services.LanguageService
{
    public static class LanguageExtensions
    {
        public static LanguageModel<TTranslationModel>? ToDataModel<TTranslationModel>(this LanguageEntity entity) where TTranslationModel : TranslationModel, new()
        {
         
            if (entity == null || string.IsNullOrWhiteSpace(entity.ID.ToString())) 
                return null;

            if (entity.Translations == null)
            {
                return new LanguageModel<TTranslationModel>
                {
                    ID = entity.ID,
                    Code = entity.Code,
                    Name = entity.Name
                };
            }

            return new LanguageModel<TTranslationModel>
            {
                ID = entity.ID,
                Code = entity.Code,
                Name = entity.Name,
                Translations = JsonSerializer.Deserialize<TTranslationModel>(entity.Translations)
            };
        }

    }
}
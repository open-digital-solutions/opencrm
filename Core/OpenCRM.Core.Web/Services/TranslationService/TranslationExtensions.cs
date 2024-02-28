using Azure;
using OpenCRM.Core.Data;
using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Models;

namespace OpenCRM.Core.Web.Services.TranslationService
{
    public static class TranslationExtensions
    {
        public static TranslationModel<TDataModel>? ToDataModel<TDataModel>(this TranslationEntity entity)
        {
            if (entity == null || string.IsNullOrWhiteSpace(entity.ID.ToString()))
                return null;

            return new TranslationModel<TDataModel>
            {
                ID = entity.ID,
                Key = entity.Key,
                Translation = entity.Translation,
                LanguageId= entity.LanguageId,
            };
        }
    }
}
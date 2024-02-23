using OpenCRM.Core.Web.Models;

namespace OpenCRM.Core.Web.Services.LanguageService
{
    public class LanguageModel<TTranslationModel> where TTranslationModel : TranslationModel, new()
    {      
        public required Guid ID { get; set; }
        public required string Code { get; set; }
        public required string Name { get; set; }

        public TTranslationModel? Translations { get; set; }


        public static LanguageModel<TTranslationModel> GetNewInstance(string Code, string Name) { 
          return new LanguageModel<TTranslationModel>() { ID = Guid.Empty, Code = Code, Name = Name };     
        }
    }
}
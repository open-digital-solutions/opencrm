using OpenCRM.Core.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

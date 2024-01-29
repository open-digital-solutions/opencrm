using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using OpenDHS.Shared.Data;
//using OpenDHS.Shared;
using OpenCRM.Core.Data;
using System.Text.Json;


namespace OpenCRM.Core.Web.Services.LanguageService
{
    public static class LanguageExtensions
    {
        public static LanguageModel<TDataModel>? ToDataModel<TDataModel>(this LanguageEntity entity) {
         
            if (entity == null || string.IsNullOrWhiteSpace(entity.ID.ToString())) 
                return null;
            return new LanguageModel<TDataModel>
            {
                ID = entity.ID,
                Code = entity.Code,
                Name = entity.Name,

            };
        }
    }
}

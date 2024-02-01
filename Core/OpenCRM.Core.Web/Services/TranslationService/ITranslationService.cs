using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.Core.Web.Services.TranslationService
{
    public interface ITranslationService
    {        
        /// <summary>
        /// TODO:
        /// </summary>
        /// <typeparam name="TDataModel"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns
        Task<TranslationModel<TDataModel>?> GetTranslationAsync<TDataModel>(Guid id);
        /// <summary>
        /// TODO: Async
        /// </summary>
        /// <typeparam name="TDataModel"></typeparam>
        /// <returns></returns>
        List<TranslationModel<TDataModel>> GetTranslationListAsync<TDataModel>();
        /// <summary>
        /// TODO:
        /// </summary>
        /// <typeparam name="TDataModel"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<TranslationModel<TDataModel>?> AddTranslation<TDataModel>(TranslationModel<TDataModel> model);
        /// <summary>
        /// TODO: 
        /// </summary>
        /// <typeparam name="TDataModel"></typeparam>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task DeleteTranslation<TDataModel>(Guid Id);
        /// <summary>
        /// TODO: 
        /// </summary>
        /// <typeparam name="TDataModel"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<TranslationModel<TDataModel>?> EditTranslation<TDataModel>(TranslationModel<TDataModel> model);
        Task Seed();
    }
}

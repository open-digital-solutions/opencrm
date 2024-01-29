//using OpenDHS.Shared.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.Core.Web.Services.LanguageService
{
    public interface ILanguageService
    {
        /// <summary>
        /// TODO:
        /// </summary>
        /// <typeparam name="TDataModel"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns
        Task<LanguageModel<TDataModel>?> GetLanguageAsync<TDataModel>(Guid id);
        /// <summary>
        /// TODO: Async
        /// </summary>
        /// <typeparam name="TDataModel"></typeparam>
        /// <returns></returns>
        List<LanguageModel<TDataModel>> GetLanguageListAsync<TDataModel>();
        /// <summary>
        /// TODO:
        /// </summary>
        /// <typeparam name="TDataModel"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<LanguageModel<TDataModel>?> AddLanguage<TDataModel>(LanguageModel<TDataModel> model);
        /// <summary>
        /// TODO: 
        /// </summary>
        /// <typeparam name="TDataModel"></typeparam>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task DeleteLanguage<TDataModel>(Guid Id);
        /// <summary>
        /// TODO: 
        /// </summary>
        /// <typeparam name="TDataModel"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<LanguageModel<TDataModel>?> EditLanguage<TDataModel>(LanguageModel<TDataModel> model);
        Task Seed();       
    }

}
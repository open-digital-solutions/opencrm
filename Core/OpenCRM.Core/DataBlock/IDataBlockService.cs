using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.Core.DataBlock
{
    public interface IDataBlockService
    {
        /// <summary>
        /// TODO:
        /// </summary>
        /// <typeparam name="TDataModel"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<DataBlockModel<TDataModel>?> GetDataBlockAsync<TDataModel>(Guid id);
        /// <summary>
        /// TODO: Async
        /// </summary>
        /// <typeparam name="TDataModel"></typeparam>
        /// <returns></returns>
        List<DataBlockModel<TDataModel>> GetDataBlockListAsync<TDataModel>();
        /// <summary>
        /// TODO:
        /// </summary>
        /// <typeparam name="TDataModel"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<DataBlockModel<TDataModel>?> AddBlock<TDataModel>(DataBlockModel<TDataModel> model);
        /// <summary>
        /// TODO: 
        /// </summary>
        /// <typeparam name="TDataModel"></typeparam>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task DeleteBlock<TDataModel>(Guid Id);
        /// <summary>
        /// TODO: 
        /// </summary>
        /// <typeparam name="TDataModel"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<DataBlockModel<TDataModel>?> EditBlock<TDataModel>(DataBlockModel<TDataModel> model);
    }
}

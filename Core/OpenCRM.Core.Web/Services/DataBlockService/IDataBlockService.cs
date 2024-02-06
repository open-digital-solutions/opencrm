namespace OpenCRM.Core.DataBlock
{
    public interface IDataBlockService
    {
        Task<DataBlockModel<TDataModel>?> AddBlock<TDataModel>(DataBlockModel<TDataModel> model);
        Task DeleteBlock<TDataModel>(Guid Id);
        Task<DataBlockModel<TDataModel>?> EditBlock<TDataModel>(DataBlockModel<TDataModel> model);
        Task<DataBlockModel<TDataModel>?> GetDataBlockAsync<TDataModel>(Guid id);
        Task<List<DataBlockModel<TDataModel>>> GetDataBlockListAsync<TDataModel>();
    }
}
using OpenCRM.Core.DataBlock;

namespace OpenCRM.Finance.Services
{
    public interface IAccountingService
    {
        Task<DataBlockModel<AccountingModel>> AddAccounting(DataBlockModel<AccountingModel> model);
        Task<DataBlockModel<AccountingModel>> EditAccounting(DataBlockModel<AccountingModel> model);
        Task<DataBlockModel<AccountingModel>> GetAccounting(Guid id);
        Task<List<DataBlockModel<AccountingModel>>> GetAccountings();
        Task RemoveAccounting(Guid Id);
        Task Seed();
    }
}
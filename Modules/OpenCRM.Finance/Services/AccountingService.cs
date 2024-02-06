using OpenCRM.Core.DataBlock;
using OpenCRM.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCRM.Core.Data;
using System.Text.Json;

namespace OpenCRM.Finance.Services
{
    public class AccountingService<TDBContext> : IAccountingService where TDBContext : DataContext
    {
        public readonly IDataBlockService _dataBlockService;
        public AccountingService(IDataBlockService dataBlockService)
        {
            _dataBlockService = dataBlockService;
        }

        public async Task<DataBlockModel<AccountingModel>> AddAccounting(DataBlockModel<AccountingModel> model)
        {
            return await _dataBlockService.AddBlock(model);
        }

        public async Task<DataBlockModel<AccountingModel>> EditAccounting(DataBlockModel<AccountingModel> model)
        {
            return await _dataBlockService.EditBlock(model);
        }

        public async Task<DataBlockModel<AccountingModel>> GetAccounting(Guid id)
        {
            return await _dataBlockService.GetDataBlockAsync<AccountingModel>(id);
        }


        public async Task<List<DataBlockModel<AccountingModel>>> GetAccountings()
        {
            var result = await _dataBlockService.GetDataBlockListAsync<AccountingModel>();
            if (result == null) return new List<DataBlockModel<AccountingModel>>();
            return result;
        }

        public async Task RemoveAccounting(Guid Id)
        {
            await _dataBlockService.DeleteBlock<AccountingModel>(Id);
        }

        public async Task Seed()
        {
            var dataModel = new AccountingModel()
            {
                AccountingType = AccountingType.CREDIT,
                Ammount = 333.544M,
                Description = $"Seeded at {DateTime.UtcNow}"
            };

            DataBlockModel<AccountingModel> dataBlockModel = new()
            {
                Name = dataModel.Description,
                Description = dataModel.Description,
                Type = typeof(AccountingModel).Name,
                Data = dataModel
            };

            var result = await _dataBlockService.AddBlock(dataBlockModel);
            Console.WriteLine(result);
        }

    }
}

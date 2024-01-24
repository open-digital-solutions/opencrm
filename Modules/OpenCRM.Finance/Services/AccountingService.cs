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

        public DataBlockModel<AccountingModel> AddAccounting(DataBlockModel<AccountingModel> model)
        {
            var blockRsult = _dataBlockService.AddBlock(model).Result;
            return blockRsult;
        }

        public DataBlockModel<AccountingModel> EditAccounting(DataBlockModel<AccountingModel> model)
        {
            var blockResult = _dataBlockService.EditBlock(model).Result;
            return blockResult;
        }

        public DataBlockModel<AccountingModel> GetAccounting(Guid id)
        {
            var blockResult = _dataBlockService.GetDataBlockAsync<AccountingModel>(id).Result;
            return blockResult;
        }


        public List<DataBlockModel<AccountingModel>> GetAccountings()
        {
            var blocksResult = _dataBlockService.GetDataBlockListAsync<AccountingModel>();
            return blocksResult;
        }

        public async Task RemoveAccounting(Guid Id)
        {
            await _dataBlockService.DeleteBlock<AccountingModel>(Id);
        }

        public async Task SeedAsync()
        {
            var dataModel = new AccountingModel()
            {
                AccountingType = AccountingType.CREDIT,
                Ammount = 333.544M,
                Description = $"Seeded at {DateTime.UtcNow}"
            };

            DataBlockModel<AccountingModel> dataBlockModel = new ()
            {
                Name = dataModel.Description,
                Description = dataModel.Description,
                Type = typeof(AccountingModel).Name,
                Data = dataModel
            };

           var result =  await _dataBlockService.AddBlock(dataBlockModel);
           Console.WriteLine(result);
        }

        public Task Seed()
        {
            throw new NotImplementedException();
        }
    }
}

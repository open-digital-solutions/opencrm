using OpenCRM.Core.DataBlock;
using OpenDHS.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.Finance.Services
{
    public interface IAccountingService
    {
        List<DataBlockModel<AccountingModel>> GetAccountings();
        DataBlockModel<AccountingModel> GetAccounting(Guid Id);
        DataBlockModel<AccountingModel> AddAccounting(DataBlockModel<AccountingModel> model);
        DataBlockModel<AccountingModel> EditAccounting(DataBlockModel<AccountingModel> model);
        Task RemoveAccounting(Guid Id);
        Task Seed();        
    }
}
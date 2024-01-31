using OpenCRM.Core.DataBlock;
using OpenCRM.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.SwissLPD.Services.EventService
{
    public class EventService<TDBContext> : IEventService where TDBContext : DataContext
    {
        public readonly IDataBlockService _dataBlockService;
        public EventService(IDataBlockService dataBlockService)
        {
            _dataBlockService = dataBlockService;
        }
        public DataBlockModel<EventModel> AddEvent(DataBlockModel<EventModel> model)
        {
            var blockResult = _dataBlockService.AddBlock(model).Result;
            return blockResult;
        }

        public DataBlockModel<EventModel> EditEvent(DataBlockModel<EventModel> model)
        {
            var blockResult = _dataBlockService.EditBlock(model).Result;
            return blockResult;
        }

        public DataBlockModel<EventModel> GetEvent(Guid Id)
        {
            var blockResult = _dataBlockService.GetDataBlockAsync<EventModel>(Id).Result;
            return blockResult;
        }

        public List<DataBlockModel<EventModel>> GetEvents()
        {
            var blocksResult = _dataBlockService.GetDataBlockListAsync<EventModel>();
            return blocksResult;
        }

        public async Task RemoveEvent(Guid Id)
        {
            await _dataBlockService.DeleteBlock<EventModel>(Id);
        }

        public Task Seed()
        {
            throw new NotImplementedException();
        }
    }
}

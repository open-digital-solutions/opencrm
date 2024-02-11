using OpenCRM.Core.DataBlock;
using OpenCRM.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.SwissLPD.Services.EventService
{
    public class EventService<TDBContext> :  IEventService where TDBContext : DataContext
    {
        public readonly IDataBlockService _dataBlockService;

        public EventService(IDataBlockService dataBlockService)
        {
            _dataBlockService = dataBlockService;
        }
        public async Task<DataBlockModel<EventModel>?> AddEvent(DataBlockModel<EventModel> model)
        {
            return await _dataBlockService.AddBlock(model);
        }

        public async Task<DataBlockModel<EventModel>?> EditEvent(DataBlockModel<EventModel> model)
        {
            return await _dataBlockService.EditBlock(model);
        }

        public async Task<DataBlockModel<EventModel>?> GetEvent(Guid Id)
        {
            return await _dataBlockService.GetDataBlockAsync<EventModel>(Id);
        }

        public async Task<List<DataBlockModel<EventModel>>> GetEvents()
        {
            var result = await _dataBlockService.GetDataBlockListAsync<EventModel>();
            return result;
        }

        public async Task RemoveEvent(Guid Id)
        {
            await _dataBlockService.DeleteBlock<EventModel>(Id);
        }

        public Task Seed()
        {
            //TODO: Implement if needed
            return Task.CompletedTask;
        }
    }
}

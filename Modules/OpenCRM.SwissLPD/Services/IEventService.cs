using OpenCRM.Core.DataBlock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.SwissLPD.Services
{
    public interface IEventService
    {
        List<DataBlockModel<EventModel>> GetEvents();
        DataBlockModel<EventModel> GetEvent(Guid Id);
        DataBlockModel<EventModel> AddEvent(DataBlockModel<EventModel> model);
        DataBlockModel<EventModel> EditEvent(DataBlockModel<EventModel> model);
        Task RemoveEvent(Guid Id);
        Task Seed();
    }
}

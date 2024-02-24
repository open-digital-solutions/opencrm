using OpenCRM.Core.DataBlock;

namespace OpenCRM.SwissLPD.Services.EventService
{
    public interface IEventService
    {
        Task<DataBlockModel<EventModel>?> AddEvent(DataBlockModel<EventModel> model);

        Task<DataBlockModel<EventModel>?> EditEvent(DataBlockModel<EventModel> model);

        Task<DataBlockModel<EventModel>?> GetEvent(Guid Id);

        Task<List<DataBlockModel<EventModel>>> GetEvents();

        Task RemoveEvent(Guid Id);

        Task Seed();
    }
}
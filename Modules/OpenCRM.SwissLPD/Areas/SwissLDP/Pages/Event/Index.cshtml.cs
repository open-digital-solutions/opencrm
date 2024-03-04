using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Components.Table;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Pages;
using OpenCRM.Core.Web.Pages.Shared;
using OpenCRM.Core.Web.Table;
using OpenCRM.SwissLPD.Services.EventService;
using System.Reflection;

namespace OpenCRM.SwissLPD.Areas.SwissLDP.Pages.Event
{
    public class IndexModel : PageModel
    {
        private readonly IEventService _eventService;

		private TableService<EventModel> _tableService;

		[BindProperty]
        public List<DataBlockModel<EventModel>> EventList { get; set; } = new List<DataBlockModel<EventModel>>();

        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        [BindProperty]
        public TableModel Table { get; set; } = new TableModel("Events", "Event");

        public IndexModel(IEventService eventService)
        {
            _eventService = eventService;
            _tableService = new TableService<EventModel>();

            Links.Add(new BreadCrumbLinkModel()
            {
                Area = "SwissLDP",
                IsActive = true,
                Name = "Events",
                Page = "Event",
                Url = "/SwissLDP/Event"
            });
        }

        public async Task OnGetAsync()
        {
            var events = await _eventService.GetEvents();
            if (events != null)
            {
                EventList = events;

                var result = _tableService.BuildTable(EventList);
                Table.Headers = result.Item1;
                Table.Rows = result.Item2;
            }
        }
    }
}

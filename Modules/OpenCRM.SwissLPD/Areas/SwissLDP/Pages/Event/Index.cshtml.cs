using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Pages;
using OpenCRM.Core.Web.Pages.Shared;
using OpenCRM.SwissLPD.Services;
using System.Reflection;

namespace OpenCRM.SwissLPD.Areas.SwissLDP.Pages.Event
{
    public class IndexModel : PageModel
    {
        private readonly IEventService _eventService;

        [BindProperty]
        public List<DataBlockModel<EventModel>> EventList { get; set; } = new List<DataBlockModel<EventModel>>();

        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        [BindProperty]
        public List<string> TableHeaders { get; set; } = new List<string>();

        [BindProperty]
        public List<TableRow<TRowData>> TableRows { get; set; } = new List<TableRow<TRowData>>();

        [BindProperty]
        public _TablePartialModel TablePartialModel { get; set; } = new _TablePartialModel();

        public IndexModel(IEventService eventService)
        {
            _eventService = eventService;

            Links.Add(new BreadCrumbLinkModel()
            {
                Area = "",
                IsActive = true,
                Name = "Home ...",
                Page = "",
                Url = "/"
            });

            Links.Add(new BreadCrumbLinkModel()
            {
                Area = "SwissLDP",
                IsActive = true,
                Name = "Event List",
                Page = "Event",
                Url = "/SwissLDP"
            });
        }

        private void BuildButton(TableRow<TRowData> row, string label, string url, string iconName)
        {
            row.Datas.Add(new TRowData()
            {
                Label = label,
                Url = url,
                IsButton = true,
                IconName = iconName,
            });
        }

        private void BuildTable()
        {
            var properties = typeof(EventModel).GetProperties();

            foreach (var prop in properties)
                TableHeaders.Add(prop.Name);

            foreach (var item in EventList)
            {
                TableRow<TRowData> row = new TableRow<TRowData>();
                row.ID = item.ID;

                foreach (var prop in TableHeaders)
                {
                    var data = item.Data;
                    var propValue = data.GetType().GetProperty(prop)?.GetValue(data)?.ToString();

                    if (propValue != null)
                    {
                        TRowData rowData = new TRowData()
                        {
                            Label = propValue
                        };
                        row.Datas.Add(rowData);
                    }
                }

                BuildButton(row, "Edit", "/Event/Edit", "fas fa-pen");
                BuildButton(row, "Details", "/Event/Details", "fas fa-info-circle");
                BuildButton(row, "Delete", "/Event/Delete", "fas fa-trash");

                TableRows.Add(row);
            }
        }

        public void OnGet()
        {
            var events = _eventService.GetEvents();
            if (events != null)
            {
                EventList = events;
                BuildTable();
            }
        }
    }
}

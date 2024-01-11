using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Pages;
using OpenCRM.SwissLPD.Services;
using OpenDHS.Shared.Data;

namespace OpenCRM.SwissLPD.Areas.SwissLDP.Pages.Event
{
    public class DetailsModel : PageModel
    {
        private readonly IEventService _eventService;

        public DetailsModel(IEventService eventService)
        {
            _eventService = eventService;

            BreadCrumbPartialModel.Links[0].IsActive = false;

            BreadCrumbPartialModel.Links.Add(new BreadCrumbLinkModel()
            {
                Area = "",
                IsActive = false,
                Name = "Event",
                Page = "Event"
            });

            BreadCrumbPartialModel.Links.Add(new BreadCrumbLinkModel()
            {
                Area = "",
                IsActive = true,
                Name = "Details",
                Page = "Details"
            });
        }

        [BindProperty]
        public DataBlockModel<EventModel> Model { get; set; } = default!;

        [BindProperty]
        public _BreadCrumbPartialModel BreadCrumbPartialModel { get; set; } = new _BreadCrumbPartialModel();

        public IActionResult OnGet(Guid id)
        {
            var dataBlockModel = _eventService.GetEvent(id);
            
            if (dataBlockModel == null)
            {
               return NotFound();
            }
         
            Model = dataBlockModel;
            return Page();
        }
    }
}

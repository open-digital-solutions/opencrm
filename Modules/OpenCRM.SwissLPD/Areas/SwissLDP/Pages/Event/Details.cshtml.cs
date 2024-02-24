using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Models;
using OpenCRM.SwissLPD.Services.EventService;

namespace OpenCRM.SwissLPD.Areas.SwissLDP.Pages.Event
{
    public class DetailsModel : PageModel
    {
        private readonly IEventService _eventService;

        [BindProperty]
        public DataBlockModel<EventModel> Model { get; set; } = default!;

        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        public DetailsModel(IEventService eventService)
        {
            _eventService = eventService;

            Links.Add(new BreadCrumbLinkModel()
            {
                Area = "",
                IsActive = true,
                Name = "Home",
                Page = "",
                Url = "/"
            });

            Links.Add(new BreadCrumbLinkModel()
            {
                Area = "SwissLDP",
                IsActive = true,
                Name = "Events",
                Page = "Event",
                Url = "/SwissLDP"
            });
        }
      
        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var dataBlockModel = await _eventService.GetEvent(id);
            
            if (dataBlockModel == null)
            {
               return NotFound();
            }
         
            Model = dataBlockModel;
            return Page();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Pages;
using OpenCRM.SwissLPD.Services.EventService;
using OpenCRM.Core.Data;

namespace OpenCRM.SwissLPD.Areas.SwissLDP.Pages.Event
{
    public class DetailsModel : PageModel
    {
        private readonly IEventService _eventService;

        public DetailsModel(IEventService eventService)
        {
            _eventService = eventService;
        }

        [BindProperty]
        public DataBlockModel<EventModel> Model { get; set; } = default!;

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

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Pages;
using OpenCRM.SwissLPD.Services.EventService;
using OpenDHS.Shared.Data;

namespace OpenCRM.SwissLPD.Areas.SwissLDP.Pages.Event
{
    public class DeleteModel : PageModel
    {
        private readonly IEventService _eventService;

        public DeleteModel(IEventService eventService)
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

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            var dataBlockModel = _eventService.GetEvent(id);
            
            if (dataBlockModel == null)
            {
                return NotFound();
            }
            
            await _eventService.RemoveEvent(id);
            return RedirectToPage("./Index");
        }
    }
}

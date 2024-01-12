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
    public class EditModel : PageModel
    {
        private readonly IEventService _eventService;

        public EditModel(IEventService eventService)
        {
            _eventService = eventService;
        }

        [BindProperty]
        public EventModel Model { get; set; } = default!;

        public IActionResult OnGet(Guid id)
        {
            var dataBlockModel = _eventService.GetEvent(id);

            if (dataBlockModel == null)
            {
                return NotFound();
            }
            
            Model = dataBlockModel.Data;
            return Page();
        }

        public IActionResult OnPost(Guid id)
        {
            if(ModelState.IsValid)
            {
                var dataBlockModel = _eventService.GetEvent(id);

                if (dataBlockModel == null)
                {
                    return NotFound();
                }

                var dataBlockModelEdit = new DataBlockModel<EventModel>()
                {
                    ID = id,
                    Name = Model.Description,
                    Description = Model.Description,
                    Type = typeof(EventModel).Name,
                    Data = Model
                };

                _eventService.EditEvent(dataBlockModelEdit);
                 return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}

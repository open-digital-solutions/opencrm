using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Models;
using OpenCRM.SwissLPD.Services.EventService;

namespace OpenCRM.SwissLPD.Areas.SwissLDP.Pages.Event
{
    public class EditModel : PageModel
    {
        private readonly IEventService _eventService;

        [BindProperty]
        public EventModel Model { get; set; } = default!;

        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        public EditModel(IEventService eventService)
        {
            _eventService = eventService;

            Links.Add(new BreadCrumbLinkModel()
            {
                Area = "SwissLDP",
                IsActive = true,
                Name = "Events",
                Page = "Event",
                Url = "/SwissLDP/Event"
            });
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var dataBlockModel = await _eventService.GetEvent(id);

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
                    Code = Model.Description,
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

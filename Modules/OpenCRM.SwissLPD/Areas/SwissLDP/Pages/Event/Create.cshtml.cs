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
    public class CreateModel : PageModel
    {
		private readonly IEventService _eventService;

		public CreateModel(IEventService eventService)
		{
			_eventService = eventService;
        }

		public IActionResult OnGet()
		{
			return Page();
		}

		[BindProperty]
		public EventModel Model { get; set; } = default!;

        public IActionResult OnPost()
		{
			if (ModelState.IsValid)
			{
				var eventModel = new EventModel()
				{
					Description = Model.Description,
					StartDate = Model.StartDate,
					EndDate = Model.EndDate,
				};

				var dataBlockModel = new DataBlockModel<EventModel>()
				{
					Name = eventModel.Description,
					Description = eventModel.Description,
					Type = typeof(EventModel).Name,
					Data = eventModel
				};

				_eventService.AddEvent(dataBlockModel);
				return RedirectToPage("./Index");
			}

            return Page();
        }
    }
}

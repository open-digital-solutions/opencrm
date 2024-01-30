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
    public class CreateModel : PageModel
    {
		private readonly IEventService _eventService;


        [BindProperty]
        public EventModel Model { get; set; } = default!;

        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        public CreateModel(IEventService eventService)
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
                Name = "Event List",
                Page = "Event",
                Url = "/SwissLDP"
            });

            Links.Add(new BreadCrumbLinkModel()
            {
                Area = "SwissLDP",
                IsActive = true,
                Name = "Create Event",
                Page = "Event",
                Url = "/SwissLDP/Event/Create"
            });
        }

		public IActionResult OnGet()
		{
			return Page();
		}

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

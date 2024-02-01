using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OpenCRM.Core.DataBlock;
using OpenCRM.Finance.Services;
using OpenCRM.Core.Data;
using OpenCRM.Core.Web.Models;

namespace OpenCRM.Finance.Areas.Finance.Pages.Accounting
{
    public class DetailsModel : PageModel
    {
        private readonly IAccountingService _accountingService;

        [BindProperty]
        public DataBlockModel<AccountingModel> Model { get; set; } = default!;

        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        public DetailsModel(IAccountingService accountingService)
        {
            _accountingService = accountingService;
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
                Area = "Finance",
                IsActive = true,
                Name = "Accounting List",
                Page = "Accounting",
                Url = "/Finance"
            });
        }

        public IActionResult OnGet(Guid id) 
        {
            var dataModel = _accountingService.GetAccounting(id);

            if (dataModel == null) 
            {
                return NotFound();            
            }
                       
            Model = dataModel;
            return Page();
        }
    }
}

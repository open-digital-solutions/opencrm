using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Models;
using OpenCRM.Finance.Services;
using OpenCRM.Core.Data;

namespace OpenCRM.Finance.Areas.Finance.Pages.Accounting
{
    public class CreateModel : PageModel
    {
        private readonly IAccountingService _accountingService;

        [BindProperty]
        public AccountingModel Model { get; set; } = default!;

        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        public CreateModel(IAccountingService accountingService)
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

            Links.Add(new BreadCrumbLinkModel()
            {
                Area = "Finance",
                IsActive = true,
                Name = "Create Accounting",
                Page = "Accounting",
                Url = "/Finance/Accounting/Create"
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
                var dataModel = new AccountingModel()
                {
                    AccountingType = Model.AccountingType,
                    Ammount = Model.Ammount,
                    Description = Model.Description,                                        
                };

                var dataBlockModel = new DataBlockModel<AccountingModel>()
                {
                    Code = dataModel.Description,
                    Description = dataModel.Description,
                    Type = typeof(AccountingModel).Name,
                    Data = dataModel
                };

                _accountingService.AddAccounting(dataBlockModel);
                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}

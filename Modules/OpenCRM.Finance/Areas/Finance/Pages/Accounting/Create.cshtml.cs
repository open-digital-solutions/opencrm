using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OpenCRM.Core.DataBlock;
using OpenCRM.Finance.Services;
using OpenCRM.Core.Data;

namespace OpenCRM.Finance.Areas.Finance.Pages.Accounting
{
    public class CreateModel : PageModel
    {
        private readonly IAccountingService _accountingService;

        public CreateModel(IAccountingService accountingService)
        {
            _accountingService = accountingService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public AccountingModel Model { get; set; } = default!;

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
                    Name = dataModel.Description,
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

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OpenCRM.Core.DataBlock;
using OpenCRM.Finance.Services;
using OpenDHS.Shared.Data;

namespace OpenCRM.Finance.Areas.Finance.Pages.Accounting
{
    public class EditModel : PageModel
    {
        private readonly IAccountingService _accountingService;
        public EditModel(IAccountingService accountingService)
        {
            _accountingService = accountingService;
        }

        [BindProperty]
        public AccountingModel Model { get; set; } = default!;

        public IActionResult OnGet(Guid id)
        {
            var dataBlockModel = _accountingService.GetAccounting(id);

            if (dataBlockModel == null)
            {
                return NotFound();
            }

            Model = dataBlockModel.Data;
            return Page();
        }

        public IActionResult OnPost(Guid id)
        {
            if (ModelState.IsValid)
            {
                var dataBlockModel = _accountingService.GetAccounting(id);

                if (dataBlockModel == null)
                {
                    return NotFound();
                }

                var dataBlockModelEdit = new DataBlockModel<AccountingModel>()
                {   
                    ID = id,
                    Type = typeof(AccountingModel).Name,
                    Name = Model.Ammount.ToString(),
                    Description = Model.Description,                    
                    Data = Model
                };

                _accountingService.EditAccounting(dataBlockModelEdit);
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}

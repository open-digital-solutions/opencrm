using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OpenCRM.Core.DataBlock;
using OpenCRM.Finance.Services;
using OpenDHS.Shared.Data;

namespace OpenCRM.Finance.Areas.Finance.Pages.Accounting
{
    public class DeleteModel : PageModel
    {
        private readonly IAccountingService _accountingService;

        public DeleteModel(IAccountingService accountingService)
        {
            _accountingService = accountingService;
        }

        [BindProperty]
        public DataBlockModel<AccountingModel> Model { get; set; } = default!;

        public IActionResult OnGet(Guid id)
        {
            var dataBlockModel = _accountingService.GetAccounting(id);

            if (dataBlockModel == null)
            {
                return NotFound();
            }

            Model = dataBlockModel;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            var dataBlockModel = _accountingService.GetAccounting(id); 

            if (dataBlockModel == null)
            {
                return NotFound();
            }

            await _accountingService.RemoveAccounting(id);
            return RedirectToPage("./Index");
        }
    }
}

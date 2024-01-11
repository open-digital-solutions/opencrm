using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OpenCRM.Core.DataBlock;
using OpenCRM.Finance.Services;
using OpenDHS.Shared.Data;

namespace OpenCRM.Finance.Areas.Finance.Pages.Accounting
{
    public class DetailsModel : PageModel
    {
        private readonly IAccountingService _accountingService;

        public DetailsModel(IAccountingService accountingService)
        {
            _accountingService = accountingService;        
        }

        public DataBlockModel<AccountingModel> Model { get; set; } = default!;


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

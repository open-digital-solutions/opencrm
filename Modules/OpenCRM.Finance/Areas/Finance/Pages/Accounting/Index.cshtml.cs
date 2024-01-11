using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.DataBlock;
using OpenCRM.Finance.Services;
using OpenDHS.Shared;

namespace OpenCRM.Finance.Areas.Finance.Pages.Accounting
{
    public class IndexModel : PageModel
    {
        private readonly IAccountingService _accountingDataService;
        public IndexModel(IAccountingService accountingDataService)
        {
            _accountingDataService = accountingDataService;
        }

        [BindProperty]
        public List<DataBlockModel<AccountingModel>> AccountingList { get; set; } = new List<DataBlockModel<AccountingModel>>();

        public void OnGet()
        {
            var data = _accountingDataService.GetAccountings();
            if (data != null)
            {
                AccountingList = data;
            }
        }
    }
}

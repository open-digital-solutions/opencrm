using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.Finance.Services
{
    public enum AccountingType {
        /// <summary>
        /// Debit is positive to the accounting (+).  IT Dare
        /// </summary>
        DEBIT = 0, 
        /// <summary>
        /// Credit is negative to the accounting (-). IT Avere
        /// </summary>
        CREDIT = 1
    }
    public class AccountingModel
    {
        public required AccountingType AccountingType { get; set; }
        public required decimal Ammount { get; set; }
        public required string Description { get; set; }

    }
}

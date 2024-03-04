using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.SwissLPD.Services.SupplierService
{
    public class SupplierModel
    {
        [Required]
        [Display(Name = "CHE IVA Code")]
        public string CHECode { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string Mobile { get; set; } = string.Empty;

    }
}

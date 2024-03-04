using Microsoft.AspNetCore.Identity;
using OpenCRM.Core.Data;
using OpenCRM.Core.Web.Areas.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.SwissLPD.Services.SupplierService
{
    public interface ISupplierService
    {
        Task<Tuple<bool, string>> ValidateUserByCHECode(InputRegisterModel input, UserManager<UserEntity> _userManager);
    }
}

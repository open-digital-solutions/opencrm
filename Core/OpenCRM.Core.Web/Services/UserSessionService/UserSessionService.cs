using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using OpenCRM.Core.Data;
using OpenCRM.Core.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.Core.Web.Services.UserSessionService
{
    public class UserSessionService : IUserSessionService
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly ILogger<UserSessionService> _logger;
        public UserSessionService(UserManager<UserEntity> userManager, ILogger<UserSessionService> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public void GetUserSession(string UserId)
        {

        }
        public void SetUserSession(UserSessionModel userSessionModel)
        {
        }
    }
}

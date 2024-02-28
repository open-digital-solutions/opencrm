﻿using OpenCRM.Core.Data;

namespace OpenCRM.Core.Web.Services
{
    public class UserSessionService<TDBContext> : IUserSessionService where TDBContext : DataContext
    {

        private readonly TDBContext _dbContext;
        public UserSessionService(TDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public void GetUserSession(string UserId)
        {

        }
        public async Task SetUserSessionAsync(UserEntity user)
        {
            if (user == null) return;
            var instance = Activator.CreateInstance<UserSessionEntity>();
            instance.UserId = user.Id;
            instance.UserName = user.UserName ?? string.Empty;
            instance.IssuedDate = DateTime.UtcNow;
            instance.ExpirationDate = instance.IssuedDate.AddHours(1);
            //TODO: Setup CypherKey from user rsa
            instance.CypherToken = "";
            await _dbContext.UserSessions.AddAsync(instance);
        }
    }
}

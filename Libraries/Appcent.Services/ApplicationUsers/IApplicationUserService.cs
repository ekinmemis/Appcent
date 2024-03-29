﻿using System.Collections.Generic;

using Appcent.Core.Domain;

using Microsoft.AspNetCore.Http;

namespace Appcent.Services.ApplicationUsers
{
    public interface IApplicationUserService
    {
        ApplicationUser GetApplicationUserById(int applicationUserId);

        IEnumerable<ApplicationUser> GetAll();

        IList<ApplicationUser> GetApplicationUsersByIds(int[] applicationUserIds);

        void InsertApplicationUser(ApplicationUser applicationUser);

        void UpdateApplicationUser(ApplicationUser applicationUser);

        void DeleteApplicationUser(ApplicationUser applicationUser);

        ApplicationUser GetApplicationUserByUsernameAndPassword(string username, string password);

        ApplicationUser GetApplicationUserByUsername(string username);

        string GenerateJwtToken(ApplicationUser applicationUser, string secret);

        void AttachUserToContext(HttpContext context, string token);
    }
}
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

using Appcent.Core.Domain;
using Appcent.Data;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Appcent.Services.ApplicationUserService
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IRepository<ApplicationUser> _applicationUserRepository;
        private readonly IConfiguration _config;

        public ApplicationUserService(IRepository<ApplicationUser> applicationUserRepository, IConfiguration config)
        {
            _applicationUserRepository = applicationUserRepository;
            _config = config;
        }

        public ApplicationUser GetApplicationUserById(int applicationUserId)
        {
            if (applicationUserId == 0)
                return null;

            return _applicationUserRepository.GetById(applicationUserId);
        }

        public IList<ApplicationUser> GetApplicationUsersByIds(int[] applicationUserIds)
        {
            if (applicationUserIds == null || applicationUserIds.Length == 0)
                return new List<ApplicationUser>();

            var query = from c in _applicationUserRepository.Table
                        where applicationUserIds.Contains(c.Id) && !c.Deleted
                        select c;
            var applicationUsers = query.ToList();
            var sortedApplicationUsers = new List<ApplicationUser>();
            foreach (var id in applicationUserIds)
            {
                var applicationUser = applicationUsers.Find(x => x.Id == id);
                if (applicationUser != null)
                    sortedApplicationUsers.Add(applicationUser);
            }

            return sortedApplicationUsers;
        }

        public void InsertApplicationUser(ApplicationUser applicationUser)
        {
            if (applicationUser == null)
                throw new ArgumentNullException(nameof(applicationUser));

            _applicationUserRepository.Insert(applicationUser);
        }

        public void UpdateApplicationUser(ApplicationUser applicationUser)
        {
            if (applicationUser == null)
                throw new ArgumentNullException(nameof(applicationUser));

            _applicationUserRepository.Update(applicationUser);
        }

        public void DeleteApplicationUser(ApplicationUser applicationUser)
        {
            if (applicationUser == null)
                throw new ArgumentNullException(nameof(applicationUser));

            applicationUser.Deleted = true;
            applicationUser.Username += "-DELETED";

            UpdateApplicationUser(applicationUser);
        }

        public ApplicationUser GetApplicationUserByUsernameAndPassword(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return null;

            var query = from c in _applicationUserRepository.Table
                        orderby c.Id
                        where c.Username == username && c.Password == password
                        select c;
            var applicationUser = query.FirstOrDefault();
            return applicationUser;
        }

        public string GenerateJwtToken(ApplicationUser applicationUser, string secret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", applicationUser.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return _applicationUserRepository.GetAll();
        }

        public void AttachUserToContext(HttpContext context, string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.GetSection("Secret").ToString());
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

            context.Items["User"] = this.GetApplicationUserById(userId);
        }
    }
}
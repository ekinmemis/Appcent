using Appcent.Core.Domain;

namespace Appcent.Api.Models.ApplicationUserModels
{
    public class LoginResponseModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }

        public LoginResponseModel(ApplicationUser applicationUser, string token)
        {
            Id = applicationUser.Id;
            FirstName = applicationUser.FirstName;
            LastName = applicationUser.LastName;
            Username = applicationUser.Username;
            Token = token;
        }
    }
}
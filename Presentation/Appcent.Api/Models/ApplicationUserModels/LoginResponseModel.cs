using Appcent.Core.Domain;

namespace Appcent.Api.Models.ApplicationUserModels
{
    public class LoginResponseModel
    {
        public ApplicationUser ApplicationUser { get; set; } 
        public string Token { get; set; } 
    }
}
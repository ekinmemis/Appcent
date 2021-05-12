using System.ComponentModel.DataAnnotations;

namespace Appcent.Api.Models.ApplicationUserModels
{
    public class LoginRequestModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
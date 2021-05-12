using System.Text.Json.Serialization;

namespace Appcent.Core.Domain
{
    public class ApplicationUser : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
    }
}
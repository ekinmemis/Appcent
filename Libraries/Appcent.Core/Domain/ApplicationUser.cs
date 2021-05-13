using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Appcent.Core.Domain
{
    public class ApplicationUser : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public bool Deleted { get; set; }
        public string Password { get; set; }

        [JsonIgnore]
        public virtual ICollection<Job> Jobs { get; set; }
    }
}
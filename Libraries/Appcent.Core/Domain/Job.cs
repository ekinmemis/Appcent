using System;
using System.Text.Json.Serialization;

namespace Appcent.Core.Domain
{
    public class Job : BaseEntity, ICloneable
    {
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Deleted { get; set; }

        public int ApplicationUserId { get; set; }
        
        [JsonIgnore]
        public virtual ApplicationUser ApplicationUser { get; set; }

        public object Clone()
        {
            var newJob = new Job()
            {
                Description = Description,
                ApplicationUser = ApplicationUser,
                ApplicationUserId = ApplicationUserId,
                CreatedDate = CreatedDate,
                Deleted = Deleted,
                Id = Id,
                UpdatedDate = UpdatedDate
            };
            return newJob;
        }
    }
}

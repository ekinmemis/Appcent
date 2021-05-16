using System.ComponentModel.DataAnnotations;

namespace Appcent.Api.Models.Jobs
{
    public class JobModel
    {
        [Required]
        public string Description { get; set; }
        public int Id { get; set; }
        public int ApplicationUserId { get; set; }
    }
}

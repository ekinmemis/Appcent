using System.Collections.Generic;

namespace Appcent.Api.Models.Errors
{
    public class ErrorModel
    {
        public ErrorModel()
        {
            Errors = new List<string>();
        }

        public List<string> Errors { get; set; }
        public int Status { get; set; }
    }
}
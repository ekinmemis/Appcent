namespace Appcent.Api.Models.ApplicationUserModels
{
    public class ApplicationUserModel
    {
        public int Id { get; set; }
        public bool Deleted { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
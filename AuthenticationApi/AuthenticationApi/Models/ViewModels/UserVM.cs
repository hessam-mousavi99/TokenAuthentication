namespace AuthenticationApi.Models.ViewModels
{
    public class UserVM
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool IsActive { get; set; }=false;
    }
}

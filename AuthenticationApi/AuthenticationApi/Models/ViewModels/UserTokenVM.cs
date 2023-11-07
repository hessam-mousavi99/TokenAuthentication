namespace AuthenticationApi.Models.ViewModels
{
    public class UserTokenVM
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public bool IsValid { get; set; }
        public DateTime GenerateDate { get; set; }
    }
}

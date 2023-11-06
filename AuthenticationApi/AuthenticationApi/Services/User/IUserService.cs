using AuthenticationApi.Models.ViewModels;

namespace AuthenticationApi.Services.User
{
    public interface IUserService
    {
        Task<UserVM> GetUserByIdAsync(Guid userid);
        Task<UserVM> GetUserByEmailAsync(string email);
        Task InsertUserAsync(UserVM user);
    }
}

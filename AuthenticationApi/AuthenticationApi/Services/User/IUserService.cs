using AuthenticationApi.Models.ViewModels;

namespace AuthenticationApi.Services.User
{
    public interface IUserService
    {
        Task<UserVM> GetUserByIdAsync(Guid userid);
        Task<UserVM> GetUserByEmailAsync(string email);
        Task InsertUserAsync(UserVM user);
        Task UpdateUserAsync(Guid id, UpdateUserVM model);
        Task<Guid> GetUserIdbyEmailAsync(string email);
        Task DeleteUserAsync(Guid id);
    }
}

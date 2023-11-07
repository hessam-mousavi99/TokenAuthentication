using AuthenticationApi.Models.ViewModels;

namespace AuthenticationApi.Services.Authenticate
{
    public interface IAuthenticateService
    {
        Task<UserTokenVM> GetTokenByUserIdAsync(Guid userId);
        Task InsertTokenAsync(UserTokenVM model);
        Task UpdateTokenAsync(UserTokenVM model);
    }
}

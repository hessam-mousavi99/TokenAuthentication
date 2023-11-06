using AuthenticationApi.Models.ViewModels;
using AuthenticationApi.Utils;
using Dapper;

namespace AuthenticationApi.Services.User
{
    public class UserService : IUserService
    {
        private readonly DapperUtility _dapperUtility;

        public UserService(DapperUtility dapperUtility)
        {
            _dapperUtility = dapperUtility;
        }
        public async Task<UserVM> GetUserByEmailAsync(string email)
        {
            var sql = "GetUserByEmail_SP";
            using (var connection = _dapperUtility.DapperConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<UserVM>(sql, new { Email = email }, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<UserVM> GetUserByIdAsync(Guid userid)
        {
            var sql = "GetUserById_SP";
            using (var connection = _dapperUtility.DapperConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<UserVM>(sql, new { Id = userid }, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public Task InsertUserAsync(UserVM user)
        {
            throw new NotImplementedException();
        }
    }
}

using AuthenticationApi.Models.ViewModels;
using AuthenticationApi.Utils;
using Dapper;
using System.Data;

namespace AuthenticationApi.Services.User
{
    public class UserService : IUserService
    {
        private readonly DapperUtility _dapperUtility;

        public UserService(DapperUtility dapperUtility)
        {
            _dapperUtility = dapperUtility;
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var sql = "DeleteUser_SP";
            using (var connection = _dapperUtility.DapperConnection())
            {
                await connection.ExecuteAsync(sql, new {  Id = id }, commandType: CommandType.StoredProcedure);
            }
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

        public async Task<Guid> GetUserIdbyEmailAsync(string email)
        {
            var sql = "GetUserIdByEmail_SP";
            using (var connection = _dapperUtility.DapperConnection())
            {
                Guid ID= await connection.QuerySingleOrDefaultAsync<Guid>(sql, new { Email = email }, commandType: System.Data.CommandType.StoredProcedure);
                return ID;
            }
        }

        public async Task InsertUserAsync(UserVM user)
        {
            var sql = "InsertUser_SP";
            using (var connection = _dapperUtility.DapperConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("Id", user.Id, dbType: DbType.Guid);
                parameters.Add("Email", user.Email);
                parameters.Add("FirstName", user.FirstName);
                parameters.Add("LastName", user.LastName);
                parameters.Add("Password", user.Password);
                parameters.Add("IsActive", user.IsActive);
                await connection.ExecuteAsync(sql, parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public async Task UpdateUserAsync(Guid id,UpdateUserVM model)
        {
            var sql = "UpdateUser_SP";
            using (var connection = _dapperUtility.DapperConnection())
            {              
                await connection.ExecuteAsync(sql,new { Id = id, Email=model.Email,FirstName=model.FirstName,
                    LastName=model.LastName,Password=model.Password,IsActive=model.IsActive }, commandType: CommandType.StoredProcedure);
            }
        }
    }
}

using AuthenticationApi.Models.ViewModels;
using AuthenticationApi.Utils;
using Dapper;
using System.Data;

namespace AuthenticationApi.Services.Authenticate
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly DapperUtility _dapperUtility;

        public AuthenticateService(DapperUtility dapperUtility)
        {
            _dapperUtility = dapperUtility;
        }
        public Task<UserTokenVM> GetTokenByUserIdAsync(Guid userId)
        {
            var sql = "GetToken_SP";
            using (var connection=_dapperUtility.DapperConnection())
            {
                var resault = connection.QuerySingleOrDefaultAsync<UserTokenVM>(sql, new { UserId = userId }, commandType: CommandType.StoredProcedure);
                return resault;
            }
        }

        public async Task InsertTokenAsync(UserTokenVM model)
        {
            var sql = "InsertToken_SP";
            using (var connection=_dapperUtility.DapperConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("UserId", model.UserId ,DbType.Guid);
                parameters.Add("RefreshToken", model.RefreshToken);
                parameters.Add("GenerateDate", model.GenerateDate);
                parameters.Add("IsValid", model.IsValid); 
                await connection.ExecuteAsync(sql, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateTokenAsync(UserTokenVM model)
        {
            var sql = "UpdateToken_SP";
            using (var connection = _dapperUtility.DapperConnection())
            {
                await connection.ExecuteAsync(sql, new
                {
                    UserId = model.UserId,
                    RefreshToken = model.RefreshToken,
                    GenerateDate = model.GenerateDate,
                    IsValid = model.IsValid    
                }, commandType: CommandType.StoredProcedure);
            }
        }
    }
}

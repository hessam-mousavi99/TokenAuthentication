using System.Data.SqlClient;

namespace AuthenticationApi.Utils
{
    public class DapperUtility
    {
        private readonly IConfiguration _configuration;
        public DapperUtility(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public SqlConnection DapperConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("ConnStrDapper"));
        }
    }
}

using AuthenticationApi.Entities;
using AuthenticationApi.Utils;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationApi.Services.Permission
{
    public class PermissionService : IPermissionService
    {
        private readonly DapperUtility _dapperUtility;

        public PermissionService(DapperUtility dapperUtility)
        {
            _dapperUtility = dapperUtility;
        }

        public async Task<List<int>> GetPermissionsByRoleIdAsync(int roleId)
        {
            var sql = @"Select PermissionId from RolePermissions  where RoleId=@RoleId";

            using (var connection = _dapperUtility.DapperConnection())
            {
                 var permissions=await connection.QuerySingleOrDefaultAsync(sql, new {RoleId=roleId});
                return permissions;
            }

        }

        public async Task InsertPermissionsAsync(List<Entities.Permission> models)
        {

            //var sql = "BulkInsertPermission_SP";
            var sql = @"Insert Permissions (Title,AreaName,ControllerName,ActionName,ActionType)
                        Values (@Title,@AreaName,@ControllerName,@ActionName,@ActionType)";
            if (models != null)
            {
                using (var connection = _dapperUtility.DapperConnection())
                {
                    await connection.ExecuteAsync(sql, models);
                }
            }
        }

        public async Task InsertRolePermissionAsync(List<RolePermission> models)
        {
            var sql = @"Insert RolePermissions (RoleId,PermissionId)
                        Values (@RoleId,@PermissionId)";
            if (models != null)
            {
                using (var connection = _dapperUtility.DapperConnection())
                {
                    await connection.ExecuteAsync(sql, models);
                }
            }
        }
    }
}

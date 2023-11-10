using AuthenticationApi.Entities;
using AuthenticationApi.Utils;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data;

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
            var sql = @"Select * from RolePermissions  where RoleId=@RoleId";
            List<int> permissionIds = new List<int>();
            using (var connection = _dapperUtility.DapperConnection())
            {
                var rolePermission = await connection.QueryAsync<RolePermission>(sql, new { RoleId = roleId });
                foreach (var item in rolePermission)
                {
                    permissionIds.Add(item.PermissionId);
                }
                return permissionIds;
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

        public async Task InsertRolePermissionAsync(int roleid, int selectedPermission)
        {
            var sql = @"Insert RolePermissions (RoleId,PermissionId)
                        Values (@RoleId,@PermissionId)";
            using (var connection = _dapperUtility.DapperConnection())
            {
                await connection.ExecuteAsync(sql, new { RoleId = roleid, PermissionId = selectedPermission });
            }

        }
    }
}

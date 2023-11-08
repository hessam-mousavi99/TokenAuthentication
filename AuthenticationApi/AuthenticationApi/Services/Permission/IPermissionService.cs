using AuthenticationApi.Entities;

namespace AuthenticationApi.Services.Permission
{
    public interface IPermissionService
    {
        Task InsertPermissionsAsync(List<AuthenticationApi.Entities.Permission> models);
        Task InsertRolePermissionAsync(List<RolePermission> models);
        Task<List<int>> GetPermissionsByRoleIdAsync(int roleId);
       
    }
}

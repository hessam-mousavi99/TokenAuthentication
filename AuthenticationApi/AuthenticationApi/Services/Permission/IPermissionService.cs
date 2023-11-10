using AuthenticationApi.Entities;

namespace AuthenticationApi.Services.Permission
{
    public interface IPermissionService
    {
        Task InsertPermissionsAsync(List<AuthenticationApi.Entities.Permission> models);
        Task InsertRolePermissionAsync(int roleid,int selectedPermission);
        Task<List<int>> GetPermissionsByRoleIdAsync(int roleId);
       
    }
}

namespace AuthenticationApi.Models.ViewModels
{
    public class AddRolePermissionVM
    {

        public int RoleId { get; set; }
        public List<int> SelectedPermissions { get; set; }
    }
}

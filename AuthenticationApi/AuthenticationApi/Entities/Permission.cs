using AuthenticationApi.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace AuthenticationApi.Entities;

public partial class Permission
{
    [Key]
    public int PermissionId { get; set; }

    public string Title { get; set; } = null!;

    public string AreaName { get; set; } = null!;

    public string ControllerName { get; set; } = null!;

    public string ActionName { get; set; } = null!;

    public ActionType ActionType { get; set; }

    public virtual ICollection<RolePermission>? RolePermissions { get; set; } = new List<RolePermission>();
}

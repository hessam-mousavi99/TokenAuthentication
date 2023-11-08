using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AuthenticationApi.Entities;

public partial class Role
{
    [Key]
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<RolePermission>? RolePermissions { get; set; }
    public virtual ICollection<User>? Users { get; set; }
} 

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthenticationApi.Entities;

public partial class RolePermission
{
    [Key]
    public int Rpid { get; set; }

    public int RoleId { get; set; }

    public int PermissionId { get; set; }
    [ForeignKey("PermissionId")]
    public virtual Permission Permission { get; set; } = null!;
    [ForeignKey("RoleId")]
    public virtual Role Role { get; set; } = null!;

    
}

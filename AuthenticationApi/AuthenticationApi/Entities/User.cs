using System.ComponentModel.DataAnnotations;

namespace AuthenticationApi.Entities;

public partial class User
{
    public Guid Id { get; set; }

    public string Email { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string Password { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual ICollection<UserToken>? UserTokens { get; set; } 
    public virtual ICollection<Role>? Roles { get; set; } 

}

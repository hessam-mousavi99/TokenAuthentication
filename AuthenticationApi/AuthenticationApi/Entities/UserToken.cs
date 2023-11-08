using System.ComponentModel.DataAnnotations;

namespace AuthenticationApi.Entities;

public partial class UserToken
{
    [Key]
    public int Id { get; set; }

    public Guid UserId { get; set; }

    public string RefreshToken { get; set; } = null!;

    public DateTime GenerateDate { get; set; }

    public bool IsValid { get; set; }

    public virtual User User { get; set; } = null!;
}

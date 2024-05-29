using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OshService.Domain.User.User;

[Table("user")]
[Index(nameof(Login), IsUnique = true)]
public class UserModel(UserType type)
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("type")]
    public UserType Type { get; } = type;

    [Column("login")]
    public required string Login { get; set; }

    [Column("password_hash")]
    public required string PasswordHash { get; set; }

    [Column("password_salt")]
    public required string PasswordSalt { get; set; }

    [Column("first_name")]
    public string? FirstName { get; set; }

    [Column("middle_name")]
    public string? MiddleName { get; set; }

    [Column("last_name")]
    public string? LastName { get; set; }
}

public enum UserType
{
    Admin,
    Employee,
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
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

    [Column("login"), MaxLength(255)]
    public required string Login { get; set; }

    [Column("password_hash"), MaxLength(255)]
    public required string PasswordHash { get; set; }

    [Column("password_salt"), MaxLength(255)]
    public required string PasswordSalt { get; set; }

    [Column("first_name"), MaxLength(255)]
    public string? FirstName { get; set; }

    [Column("middle_name"), MaxLength(255)]
    public string? MiddleName { get; set; }

    [Column("last_name"), MaxLength(255)]
    public string? LastName { get; set; }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum UserType
{
    Admin,
    Employee,
}

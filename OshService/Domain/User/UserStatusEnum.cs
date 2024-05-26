using System.ComponentModel;
using System.Text.Json.Serialization;

namespace OshService.Domain.User;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum UserStatusEnum
{
    [Description("Пользовалель с таким логином уже существет")]
    UserLoginExists
}

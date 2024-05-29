using System.ComponentModel;
using System.Text.Json.Serialization;

namespace OshService.Setup.SetupOrganization;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SetupOrganizationStatusEnum
{
    [Description("Нет доступных привилегий")]
    NoPrivilegesAvailable,

    [Description("Организация с таким URL уже сущевует")]
    OrganizationUrlExists
}

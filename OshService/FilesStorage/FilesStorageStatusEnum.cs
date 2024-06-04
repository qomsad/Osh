using System.ComponentModel;
using System.Text.Json.Serialization;

namespace OshService.FilesStorage;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum FilesStorageStatusEnum
{
    [Description("Сервер не смог загрузить файл")]
    ServerPathError,

    [Description("Файл отсутвует")]
    FileError
}

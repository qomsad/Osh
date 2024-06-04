using AspBoot.Configuration;
using AspBoot.Handler;
using AspBoot.Service;
using OshService.Options;

namespace OshService.FilesStorage;

[Service]
public class FilesStorageService(IConfiguration configuration)
{
    public Result<FilesStorageStatusEnum> Load(IFormFile file)
    {
        var storage = configuration.GetParam<FileStorage>();
        if (storage.Path == null)
        {
            return new Result<FilesStorageStatusEnum>(FilesStorageStatusEnum.ServerPathError);
        }
        var id = Guid.NewGuid().ToString();
        var name = id + "." + "pdf";
        var path = Path.Combine(storage.Path, name);
        if (!(file.Length > 0))
        {
            return new Result<FilesStorageStatusEnum>(FilesStorageStatusEnum.ServerPathError);
        }
        using Stream fileStream = new FileStream(path, FileMode.Create);
        file.CopyTo(fileStream);
        return new Result<FilesStorageStatusEnum>(new { Id = name });
    }

    public byte[]? Get(string id)
    {
        var storage = configuration.GetParam<FileStorage>();
        if (storage.Path != null)
        {
            var file = Path.Combine(storage.Path, id);
            if (File.Exists(file))
            {
                var bytes = File.ReadAllBytes(file);
                return bytes;
            }
        }
        return null;
    }
}

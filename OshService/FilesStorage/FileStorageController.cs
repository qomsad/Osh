using System.Net;
using System.Net.Mime;
using AspBoot.Handler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OshService.Domain.User.User;
using Swashbuckle.AspNetCore.Annotations;

namespace OshService.FilesStorage;

[ApiController]
[Route("api/s3")]
public class FileStorageController(FilesStorageService service) : Controller
{
    [HttpPost]
    [Authorize(Roles = nameof(UserType.Admin))]
    public IActionResult LoadFile(IFormFile file)
    {
        return new Response<object, FilesStorageStatusEnum>()
            .Handle(_ => service.Load(file))
            .OnStatus(FilesStorageStatusEnum.ServerPathError, HttpResult.NotFound)
            .Respond();
    }

    [HttpGet("{id}")]
    [Authorize]
    [SwaggerResponse((int) HttpStatusCode.OK, "Успешная аутентификация", typeof(FileResult))]
    public IActionResult GetFile([FromRoute] string id)
    {
        var file = service.Get(id);
        if (file != null)
        {
            return File(file, MediaTypeNames.Application.Octet, "attach");
        }
        return NotFound(new Result<FilesStorageStatusEnum>(FilesStorageStatusEnum.FileError));
    }
}

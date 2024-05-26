using System.Net.Mime;
using AspBoot.Handler;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace OshService.Auth;

[ApiController]
[Route("api/auth")]
[SwaggerTag("Аутентификация")]
[Produces(MediaTypeNames.Application.Json), Consumes(MediaTypeNames.Application.Json)]
public class AuthController(AuthService service) : Controller
{
    [HttpPost("login")]
    [SwaggerOperation("Аутентификация", "Аутентификация в системе")]
    [SwaggerResponse(200, "Успешная аутентификация", typeof(AuthResponse))]
    [SwaggerResponse(404, "Пользователь не найден", typeof(Status<AuthStatusEnum>))]
    [SwaggerResponse(401, "Неверный пароль", typeof(Status<AuthStatusEnum>))]
    public IActionResult Login([FromBody, SwaggerRequestBody] AuthRequest request)
    {
        return service.Authenticate(request)
            .OnStatus(AuthStatusEnum.UserNotFound, NotFound)
            .OnStatus(AuthStatusEnum.UserPasswordMismatch, Unauthorized)
            .Respond();
    }
}

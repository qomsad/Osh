using System.Net;
using System.Net.Mime;
using AspBoot.Handler;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace OshService.Auth;

[ApiController]
[Route("api/auth")]
[SwaggerTag("Аутентификация")]
[Produces(MediaTypeNames.Application.Json)]
public class AuthController(AuthService service) : Controller
{
    [HttpPost("login")]
    [SwaggerOperation("Аутентификация", "Аутентификация в системе")]
    [SwaggerResponse((int) HttpStatusCode.OK, "Успешная аутентификация", typeof(AuthResponse))]
    [SwaggerResponse((int) HttpStatusCode.NotFound, "Пользователь не найден", typeof(Status<AuthStatusEnum>))]
    [SwaggerResponse((int) HttpStatusCode.Unauthorized, "Неверный пароль", typeof(Status<AuthStatusEnum>))]
    public IActionResult Login([FromBody, SwaggerRequestBody] AuthRequest request)
    {
        return new Response<AuthRequest, AuthStatusEnum>()
            .Handle(_ => service.Authenticate(request))
            .OnStatus(AuthStatusEnum.UserNotFound, HttpResult.NotFound)
            .OnStatus(AuthStatusEnum.UserPasswordMismatch, HttpResult.Unauthorized)
            .Respond();
    }
}

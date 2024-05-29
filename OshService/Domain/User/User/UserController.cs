using Microsoft.AspNetCore.Mvc;

namespace OshService.Domain.User.User;

[ApiController]
[Route("api/user")]
public class UserController(UserService service) : Controller
{
    [HttpPost("register")]
    public IActionResult Register(UserViewCreate view)
    {
        return service.Register(view)
            .OnStatus(UserStatusEnum.UserLoginExists, Conflict)
            .Respond();
    }
}

using System.Net.Mime;
using AspBoot.Handler;
using AspBoot.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace OshService.Setup.SuperUser;

[ApiController]
[Route("api/setup/user")]
[Produces(MediaTypeNames.Application.Json)]
public class SetupUserController(
    IValidator<SetupUserRequest> validator,
    SetupUserService service
) : Controller
{
    [HttpPost]
    public IActionResult CreateSuperUser([FromBody] SetupUserRequest request)
    {
        return new Response<SetupUserRequest, SetupUserStatusEnum>()
            .OnValidationError(validator.GetValidationProblems(request), HttpResult.ValidationProblem)
            .Handle(service.CreateSuperUser)
            .OnStatus(SetupUserStatusEnum.NoPrivilegesAvailable, HttpResult.Forbidden)
            .OnStatus(SetupUserStatusEnum.UserLoginExists, HttpResult.Conflict)
            .Respond();
    }
}

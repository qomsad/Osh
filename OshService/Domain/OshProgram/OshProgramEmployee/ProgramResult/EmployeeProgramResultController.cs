using System.Net.Mime;
using AspBoot.Handler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OshService.Domain.OshProgram.OshProgramResult;
using OshService.Domain.User.User;

namespace OshService.Domain.OshProgram.OshProgramEmployee.ProgramResult;

[ApiController]
[Route("api/employee/osh-program/result")]
[Authorize(Roles = nameof(UserType.Employee))]
[Produces(MediaTypeNames.Application.Json)]
public class EmployeeProgramResultController(EmployeeProgramResultService service)
{
    [HttpPost("{id:long}")]
    public IActionResult Result([FromRoute] long id)
    {
        return new Response<object, OshProgramResultStatusEnum>()
            .Handle(_ => service.Result(id))
            .OnStatus(OshProgramResultStatusEnum.NoPrivilegesAvailable, HttpResult.Unauthorized)
            .OnStatus(OshProgramResultStatusEnum.OshProgramNotFound, HttpResult.NotFound)
            .OnStatus(OshProgramResultStatusEnum.Timeout, HttpResult.Forbidden)
            .Respond();
    }
}

using System.Net.Mime;
using AspBoot.Data.Request;
using AspBoot.Handler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OshService.Domain.User.User;

namespace OshService.Domain.OshProgram.OshProgramEmployee;

[ApiController]
[Route("api/employee/osh-program")]
[Authorize(Roles = nameof(UserType.Employee))]
[Produces(MediaTypeNames.Application.Json)]
public class EmployeeProgramController(EmployeeProgramService service) : Controller
{
    [HttpGet]
    public IActionResult GetAssigned([FromQuery] RequestPage parameters)
    {
        return Ok(service.GetAssigned(parameters));
    }

    [HttpGet("{id:long}")]
    public IActionResult GetById([FromRoute] long id)
    {
        return new Response<EmployeeProgramViewRead, EmployeeProgramStatusEnum>()
            .Handle(_ => service.GetById(id))
            .OnStatus(EmployeeProgramStatusEnum.NoPrivilegesAvailable, HttpResult.Unauthorized)
            .OnStatus(EmployeeProgramStatusEnum.OshProgramNotFound, HttpResult.NotFound)
            .Respond();
    }
}

using System.Net.Mime;
using AspBoot.Data.Request;
using AspBoot.Handler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OshService.Domain.OshProgram.OshProgram;
using OshService.Domain.User.User;

namespace OshService.Domain.OshProgram.OshProgramEmployee;

[ApiController]
[Route("api/employee/osh-program")]
[Authorize(Roles = nameof(UserType.Employee))]
[Produces(MediaTypeNames.Application.Json)]
public class OshProgramEmployeeController(OshProgramEmployeeService service) : Controller
{
    [HttpGet]
    public IActionResult GetAssigned([FromQuery] RequestPage parameters)
    {
        return Ok(service.GetAssigned(parameters));
    }

    [HttpGet("{id:long}")]
    public IActionResult GetById([FromRoute] long id)
    {
        return new Response<OshProgramViewCreate, OshProgramEmployeeStatusEnum>()
            .Handle(_ => service.GetById(id))
            .OnStatus(OshProgramEmployeeStatusEnum.NoPrivilegesAvailable, HttpResult.Unauthorized)
            .OnStatus(OshProgramEmployeeStatusEnum.OshProgramNotFound, HttpResult.NotFound)
            .Respond();
    }
}

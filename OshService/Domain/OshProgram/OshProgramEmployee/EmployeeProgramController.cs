using System.Net.Mime;
using AspBoot.Data.Request;
using AspBoot.Handler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OshService.Domain.OshProgram.OshProgramAssignment;
using OshService.Domain.User.User;

namespace OshService.Domain.OshProgram.OshProgramEmployee;

[ApiController]
[Route("api/employee/osh-program")]
[Authorize(Roles = nameof(UserType.Employee))]
[Produces(MediaTypeNames.Application.Json)]
public class EmployeeProgramController(EmployeeProgramService service) : Controller
{
    [HttpPatch("{id:long}/start-learning")]
    public IActionResult StartLearning([FromRoute] long id)
    {
        return new Response<EmployeeProgramViewRead, OshProgramAssignmentStatusEnum>()
            .Handle(_ => service.StartLearning(id))
            .OnStatus(OshProgramAssignmentStatusEnum.NoPrivilegesAvailable, HttpResult.Unauthorized)
            .OnStatus(OshProgramAssignmentStatusEnum.ProgramNotFound, HttpResult.NotFound)
            .Respond();
    }

    [HttpPatch("{id:long}/start-training")]
    public IActionResult StartTraining([FromRoute] long id)
    {
        return new Response<EmployeeProgramViewRead, OshProgramAssignmentStatusEnum>()
            .Handle(_ => service.StartTraining(id))
            .OnStatus(OshProgramAssignmentStatusEnum.NoPrivilegesAvailable, HttpResult.Unauthorized)
            .OnStatus(OshProgramAssignmentStatusEnum.ProgramNotFound, HttpResult.NotFound)
            .Respond();
    }

    [HttpGet]
    public IActionResult GetAssigned([FromQuery] RequestPage parameters)
    {
        return Ok(service.GetAssigned(parameters));
    }

    [HttpGet("results")]
    public IActionResult GetResult([FromQuery] RequestPage parameters)
    {
        return Ok(service.GetResult(parameters));
    }

    [HttpGet("{id:long}")]
    public IActionResult GetById([FromRoute] long id)
    {
        return new Response<EmployeeProgramViewRead, OshProgramAssignmentStatusEnum>()
            .Handle(_ => service.GetById(id))
            .OnStatus(OshProgramAssignmentStatusEnum.NoPrivilegesAvailable, HttpResult.Unauthorized)
            .OnStatus(OshProgramAssignmentStatusEnum.ProgramNotFound, HttpResult.NotFound)
            .Respond();
    }
}

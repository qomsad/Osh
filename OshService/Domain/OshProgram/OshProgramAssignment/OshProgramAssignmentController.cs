using System.Net.Mime;
using AspBoot.Data.Request;
using AspBoot.Handler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OshService.Domain.User.User;

namespace OshService.Domain.OshProgram.OshProgramAssignment;

[ApiController]
[Route("api/osh-program-assignment")]
[Authorize(Roles = nameof(UserType.Admin))]
[Produces(MediaTypeNames.Application.Json)]
public class OshProgramAssignmentController(
    OshProgramAssignmentService service
) : Controller
{
    [HttpPost]
    public IActionResult Create([FromBody] OshProgramAssignmentViewCreate view)
    {
        return new Response<OshProgramAssignmentViewCreate, OshProgramAssignmentStatusEnum>()
            .Handle(_ => service.Create(view))
            .OnStatus(OshProgramAssignmentStatusEnum.NoPrivilegesAvailable, HttpResult.Unauthorized)
            .OnStatus(OshProgramAssignmentStatusEnum.EmployeeNotFound, HttpResult.NotFound)
            .OnStatus(OshProgramAssignmentStatusEnum.ProgramNotFound, HttpResult.NotFound)
            .Respond();
    }

    [HttpGet]
    public IActionResult GetFiltered([FromQuery] RequestPageSortedFiltered parameters)
    {
        return Ok(service.GetFiltered(parameters));
    }

    [HttpGet("{id:long}")]
    public IActionResult GetById([FromRoute] long id)
    {
        return new Response<OshProgramAssignmentViewCreate, OshProgramAssignmentStatusEnum>()
            .Handle(_ => service.GetById(id))
            .OnStatus(OshProgramAssignmentStatusEnum.NoPrivilegesAvailable, HttpResult.Unauthorized)
            .Respond();
    }
}

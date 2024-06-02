using System.Net.Mime;
using AspBoot.Data.Request;
using AspBoot.Handler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OshService.Domain.OshProgram.OshProgramResult;
using OshService.Domain.User.User;

namespace OshService.Domain.OshProgram.OshProgramEmployee.MaterialTraining;

[ApiController]
[Route("api/employee/osh-program/{id:long}/training")]
[Authorize(Roles = nameof(UserType.Employee))]
[Produces(MediaTypeNames.Application.Json)]
public class EmployeeMaterialTrainingController(EmployeeMaterialTrainingService service) : Controller
{
    [HttpGet]
    public IActionResult GetAssigned([FromRoute] long id, [FromQuery] RequestPage parameters)
    {
        return Ok(service.GetAssigned(id, parameters));
    }

    [HttpGet("{questionId:long}")]
    public IActionResult GetById([FromRoute] long id, [FromRoute] long questionId)
    {
        return new Response<EmployeeMaterialTrainingViewRead, OshProgramResultStatusEnum>()
            .Handle(_ => service.GetById(id, questionId))
            .OnStatus(OshProgramResultStatusEnum.NoPrivilegesAvailable, HttpResult.Unauthorized)
            .OnStatus(OshProgramResultStatusEnum.OshProgramNotFound, HttpResult.NotFound)
            .Respond();
    }
}

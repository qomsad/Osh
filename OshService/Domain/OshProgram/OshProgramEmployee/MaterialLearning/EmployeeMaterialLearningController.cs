using System.Net.Mime;
using AspBoot.Data.Request;
using AspBoot.Handler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OshService.Domain.Material.MaterialLearning.LearningSection;
using OshService.Domain.OshProgram.OshProgramResult;
using OshService.Domain.User.User;

namespace OshService.Domain.OshProgram.OshProgramEmployee.MaterialLearning;

[ApiController]
[Route("api/employee/osh-program/{id:long}/learning")]
[Authorize(Roles = nameof(UserType.Employee))]
[Produces(MediaTypeNames.Application.Json)]
public class EmployeeMaterialLearningController(EmployeeMaterialLearningService service) : Controller
{
    [HttpGet]
    public IActionResult GetAssigned([FromRoute] long id, [FromQuery] RequestPage parameters)
    {
        return Ok(service.GetAssigned(id, parameters));
    }

    [HttpGet("{learningId:long}")]
    public IActionResult GetById([FromRoute] long id, [FromRoute] long learningId)
    {
        return new Response<LearningSectionViewRead, OshProgramResultStatusEnum>()
            .Handle(_ => service.GetById(id, learningId))
            .OnStatus(OshProgramResultStatusEnum.NoPrivilegesAvailable, HttpResult.Unauthorized)
            .OnStatus(OshProgramResultStatusEnum.OshProgramNotFound, HttpResult.NotFound)
            .Respond();
    }
}

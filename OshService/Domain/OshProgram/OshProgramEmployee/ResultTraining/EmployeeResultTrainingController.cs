using System.Net.Mime;
using AspBoot.Handler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OshService.Domain.OshProgram.OshProgramResult;
using OshService.Domain.User.User;

namespace OshService.Domain.OshProgram.OshProgramEmployee.ResultTraining;

[ApiController]
[Route("api/employee/osh-program/{id:long}/training/result")]
[Authorize(Roles = nameof(UserType.Employee))]
[Produces(MediaTypeNames.Application.Json)]
public class EmployeeResultTrainingController(EmployeeResultTrainingService service) : Controller
{
    [HttpGet("{trainingId:long}")]
    public IActionResult Result([FromRoute] long id, [FromRoute] long trainingId,
        [FromBody] EmployeeResultTrainingViewCreate view)
    {
        return new Response<object, OshProgramResultStatusEnum>()
            .Handle(_ => service.Result(id, trainingId, view))
            .OnStatus(OshProgramResultStatusEnum.NoPrivilegesAvailable, HttpResult.Unauthorized)
            .OnStatus(OshProgramResultStatusEnum.OshProgramNotFound, HttpResult.NotFound)
            .OnStatus(OshProgramResultStatusEnum.Timeout, HttpResult.Forbidden)
            .Respond();
    }
}

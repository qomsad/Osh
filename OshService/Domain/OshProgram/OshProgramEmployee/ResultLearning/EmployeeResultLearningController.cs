using System.Net.Mime;
using AspBoot.Handler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OshService.Domain.OshProgram.OshProgramResult;
using OshService.Domain.User.User;

namespace OshService.Domain.OshProgram.OshProgramEmployee.ResultLearning;

[ApiController]
[Route("api/employee/osh-program/{id:long}/learning/result")]
[Authorize(Roles = nameof(UserType.Employee))]
[Produces(MediaTypeNames.Application.Json)]
public class EmployeeResultLearningController(EmployeeResultLearningService service) : Controller
{
    [HttpPost("{learningId:long}")]
    public IActionResult Result([FromRoute] long id, [FromRoute] long learningId)
    {
        return new Response<object, OshProgramResultStatusEnum>()
            .Handle(_ => service.Result(id, learningId))
            .OnStatus(OshProgramResultStatusEnum.NoPrivilegesAvailable, HttpResult.Forbidden)
            .OnStatus(OshProgramResultStatusEnum.OshProgramNotFound, HttpResult.NotFound)
            .Respond();
    }
}

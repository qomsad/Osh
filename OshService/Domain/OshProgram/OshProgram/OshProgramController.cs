using System.Net.Mime;
using AspBoot.Data.Request;
using AspBoot.Handler;
using AspBoot.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OshService.Domain.User.User;

namespace OshService.Domain.OshProgram.OshProgram;

[ApiController]
[Route("api/osh-program")]
[Authorize(Roles = nameof(UserType.Admin))]
[Produces(MediaTypeNames.Application.Json)]
public class OshProgramController(
    IValidator<OshProgramViewCreate> validator,
    OshProgramService service
) : Controller
{
    [HttpPost]
    public IActionResult Create([FromBody] OshProgramViewCreate view)
    {
        return new Response<OshProgramViewCreate, OshProgramStatusEnum>()
            .OnValidationError(validator.GetValidationProblems(view), HttpResult.ValidationProblem)
            .Handle(service.Create)
            .OnStatus(OshProgramStatusEnum.NoPrivilegesAvailable, HttpResult.Forbidden)
            .Respond();
    }

    [HttpGet]
    public IActionResult Search([FromQuery] RequestPageSearch parameters)
    {
        return Ok(service.Search(parameters));
    }

    [HttpGet("{id:long}")]
    public IActionResult GetById([FromRoute] long id)
    {
        return new Response<OshProgramViewCreate, OshProgramStatusEnum>()
            .Handle(_ => service.GetById(id))
            .OnStatus(OshProgramStatusEnum.NoPrivilegesAvailable, HttpResult.Forbidden)
            .Respond();
    }

    [HttpPut("{id:long}")]
    public IActionResult Update([FromRoute] long id, [FromBody] OshProgramViewCreate view)
    {
        return new Response<OshProgramViewCreate, OshProgramStatusEnum>()
            .OnValidationError(validator.GetValidationProblems(view), HttpResult.ValidationProblem)
            .Handle(r => service.Update(id, r))
            .OnStatus(OshProgramStatusEnum.NoPrivilegesAvailable, HttpResult.Forbidden)
            .Respond();
    }

    [HttpDelete("{id:long}")]
    public IActionResult Delete([FromRoute] long id)
    {
        return new Response<OshProgramViewCreate, OshProgramStatusEnum>()
            .Handle(_ => service.Delete(id))
            .OnStatus(OshProgramStatusEnum.NoPrivilegesAvailable, HttpResult.Forbidden)
            .Respond();
    }
}

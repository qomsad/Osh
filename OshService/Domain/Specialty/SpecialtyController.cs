using System.Net.Mime;
using AspBoot.Data.Request;
using AspBoot.Handler;
using AspBoot.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OshService.Domain.User.User;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

namespace OshService.Domain.Specialty;

[ApiController]
[Route("api/specialty")]
[Authorize(Roles = nameof(UserType.Admin))]
[Produces(MediaTypeNames.Application.Json)]
public class SpecialtyController(
    IValidator<SpecialtyViewCreate> validator,
    SpecialtyService service
) : Controller
{
    [HttpPost]
    public IActionResult Create([FromBody] SpecialtyViewCreate view)
    {
        return new Response<SpecialtyViewCreate, SpecialtyStatusEnum>()
            .OnValidationError(validator.GetValidationProblems(view), HttpResult.ValidationProblem)
            .Handle(service.Create)
            .OnStatus(SpecialtyStatusEnum.NoPrivilegesAvailable, HttpResult.Forbidden)
            .OnStatus(SpecialtyStatusEnum.SpecialtyAlreadyExists, HttpResult.Conflict)
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
        return new Response<SpecialtyViewCreate, SpecialtyStatusEnum>()
            .Handle(_ => service.GetById(id))
            .OnStatus(SpecialtyStatusEnum.NoPrivilegesAvailable, HttpResult.Forbidden)
            .Respond();
    }

    [HttpPut("{id:long}")]
    public IActionResult Update([FromRoute] long id, [FromBody] SpecialtyViewCreate view)
    {
        return new Response<SpecialtyViewCreate, SpecialtyStatusEnum>()
            .OnValidationError(validator.GetValidationProblems(view), HttpResult.ValidationProblem)
            .Handle(r => service.Update(id, r))
            .OnStatus(SpecialtyStatusEnum.NoPrivilegesAvailable, HttpResult.Forbidden)
            .OnStatus(SpecialtyStatusEnum.SpecialtyAlreadyExists, HttpResult.Conflict)
            .Respond();
    }

    [HttpDelete("{id:long}")]
    public IActionResult Delete([FromRoute] long id)
    {
        return new Response<SpecialtyViewCreate, SpecialtyStatusEnum>()
            .Handle(_ => service.Delete(id))
            .OnStatus(SpecialtyStatusEnum.NoPrivilegesAvailable, HttpResult.Forbidden)
            .OnStatus(SpecialtyStatusEnum.DeleteLock, HttpResult.Conflict)
            .Respond();
    }
}

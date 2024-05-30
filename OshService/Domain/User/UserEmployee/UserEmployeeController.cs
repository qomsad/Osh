using System.Net.Mime;
using AspBoot.Data.Request;
using AspBoot.Handler;
using AspBoot.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OshService.Domain.User.User;

namespace OshService.Domain.User.UserEmployee;

[ApiController]
[Route("api/user-employee")]
[Authorize(Roles = nameof(UserType.Admin))]
[Produces(MediaTypeNames.Application.Json)]
public class UserEmployeeController(
    IValidator<UserEmployeeViewCreate> validator,
    UserEmployeeService service
) : Controller
{
    [HttpPost]
    public IActionResult Create([FromBody] UserEmployeeViewCreate view)
    {
        return new Response<UserEmployeeViewCreate, UserEmployeeStatusEnum>()
            .OnValidationError(validator.GetValidationProblems(view), HttpResult.ValidationProblem)
            .Handle(service.Create)
            .OnStatus(UserEmployeeStatusEnum.NoPrivilegesAvailable, HttpResult.Unauthorized)
            .OnStatus(UserEmployeeStatusEnum.SpecialtyNotFound, HttpResult.NotFound)
            .OnStatus(UserEmployeeStatusEnum.UserLoginExists, HttpResult.Conflict)
            .Respond();
    }

    [HttpGet]
    public IActionResult Search([FromQuery] RequestPageSearch parameters)
    {
        return Ok(service.Search(parameters));
    }

    [HttpGet("filter")]
    public IActionResult Filter([FromQuery] RequestPageSortedFiltered parameters)
    {
        return Ok(service.Filter(parameters));
    }

    [HttpGet("{id:long}")]
    public IActionResult GetById([FromRoute] long id)
    {
        return new Response<UserEmployeeViewCreate, UserEmployeeStatusEnum>()
            .Handle(_ => service.GetById(id))
            .OnStatus(UserEmployeeStatusEnum.NoPrivilegesAvailable, HttpResult.Unauthorized)
            .Respond();
    }

    [HttpPut("{id:long}")]
    public IActionResult Update([FromRoute] long id, [FromBody] UserEmployeeViewCreate view)
    {
        return new Response<UserEmployeeViewCreate, UserEmployeeStatusEnum>()
            .OnValidationError(validator.GetValidationProblems(view), HttpResult.ValidationProblem)
            .Handle(r => service.Update(id, r))
            .OnStatus(UserEmployeeStatusEnum.NoPrivilegesAvailable, HttpResult.Unauthorized)
            .OnStatus(UserEmployeeStatusEnum.SpecialtyNotFound, HttpResult.NotFound)
            .OnStatus(UserEmployeeStatusEnum.UserLoginExists, HttpResult.Conflict)
            .Respond();
    }

    [HttpDelete("{id:long}")]
    public IActionResult Delete([FromRoute] long id)
    {
        return new Response<UserEmployeeViewCreate, UserEmployeeStatusEnum>()
            .Handle(_ => service.Delete(id))
            .OnStatus(UserEmployeeStatusEnum.NoPrivilegesAvailable, HttpResult.Unauthorized)
            .OnStatus(UserEmployeeStatusEnum.SpecialtyNotFound, HttpResult.NotFound)
            .Respond();
    }
}

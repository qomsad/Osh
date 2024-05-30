using System.Net.Mime;
using AspBoot.Data.Request;
using AspBoot.Handler;
using AspBoot.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OshService.Domain.Material.MaterialLearning.LearningSection;
using OshService.Domain.User.User;

namespace OshService.Domain.OshProgram.OshProgram.OshProgramLearningSections;

[ApiController]
[Route("api/osh-program/{id:long}/learning")]
[Authorize(Roles = nameof(UserType.Admin))]
[Produces(MediaTypeNames.Application.Json)]
public class LearningSectionsController(
    IValidator<LearningSectionViewCreate> validator,
    LearningSectionService service
) : Controller
{
    [HttpPost]
    public IActionResult Create([FromRoute] long id, [FromBody] LearningSectionViewCreate view)
    {
        return new Response<LearningSectionViewCreate, LearningSectionStatusEnum>()
            .OnValidationError(validator.GetValidationProblems(view), HttpResult.ValidationProblem)
            .Handle(r => service.Create(id, r))
            .OnStatus(LearningSectionStatusEnum.NoPrivilegesAvailable, HttpResult.Unauthorized)
            .OnStatus(LearningSectionStatusEnum.OshProgramNotFound, HttpResult.NotFound)
            .Respond();
    }

    [HttpGet]
    public IActionResult GetPage([FromRoute] long id, [FromQuery] RequestPage parameters)
    {
        return Ok(service.GetPage(id, parameters));
    }

    [HttpGet("{sectionId:long}")]
    public IActionResult GetById([FromRoute] long id, [FromRoute] long sectionId)
    {
        return new Response<LearningSectionViewCreate, LearningSectionStatusEnum>()
            .Handle(_ => service.GetById(id, sectionId))
            .OnStatus(LearningSectionStatusEnum.NoPrivilegesAvailable, HttpResult.Unauthorized)
            .Respond();
    }

    [HttpPut("{sectionId:long}")]
    public IActionResult Update([FromRoute] long id, [FromRoute] long sectionId,
        [FromBody] LearningSectionViewCreate view)
    {
        return new Response<LearningSectionViewCreate, LearningSectionStatusEnum>()
            .OnValidationError(validator.GetValidationProblems(view), HttpResult.ValidationProblem)
            .Handle(r => service.Update(id, sectionId, r))
            .OnStatus(LearningSectionStatusEnum.NoPrivilegesAvailable, HttpResult.Unauthorized)
            .Respond();
    }

    [HttpDelete("{sectionId:long}")]
    public IActionResult Delete([FromRoute] long id, [FromRoute] long sectionId)
    {
        return new Response<LearningSectionViewCreate, LearningSectionStatusEnum>()
            .Handle(_ => service.Delete(id, sectionId))
            .OnStatus(LearningSectionStatusEnum.NoPrivilegesAvailable, HttpResult.Unauthorized)
            .Respond();
    }
}

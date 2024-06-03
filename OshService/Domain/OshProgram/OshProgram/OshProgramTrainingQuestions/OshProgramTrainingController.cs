using System.Net.Mime;
using AspBoot.Data.Request;
using AspBoot.Handler;
using AspBoot.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OshService.Domain.Material.MaterialTraining.TrainingQuestion;
using OshService.Domain.User.User;

namespace OshService.Domain.OshProgram.OshProgram.OshProgramTrainingQuestions;

[ApiController]
[Route("api/osh-program/{id:long}/question")]
[Authorize(Roles = nameof(UserType.Admin))]
[Produces(MediaTypeNames.Application.Json)]
public class OshProgramTrainingController(
    IValidator<TrainingQuestionViewCreate> validator,
    TrainingQuestionService service
) : Controller
{
    [HttpPost]
    public IActionResult Create([FromRoute] long id, [FromBody] TrainingQuestionViewCreate view)
    {
        return new Response<TrainingQuestionViewCreate, TrainingQuestionStatusEnum>()
            .OnValidationError(validator.GetValidationProblems(view), HttpResult.ValidationProblem)
            .Handle(r => service.Create(id, r))
            .OnStatus(TrainingQuestionStatusEnum.NoPrivilegesAvailable, HttpResult.Forbidden)
            .OnStatus(TrainingQuestionStatusEnum.OshProgramNotFound, HttpResult.NotFound)
            .Respond();
    }

    [HttpGet]
    public IActionResult GetPage([FromRoute] long id, [FromQuery] RequestPage parameters)
    {
        return Ok(service.GetPage(id, parameters));
    }

    [HttpGet("{questionId:long}")]
    public IActionResult GetById([FromRoute] long id, [FromRoute] long questionId)
    {
        return new Response<TrainingQuestionViewCreate, TrainingQuestionStatusEnum>()
            .Handle(_ => service.GetById(id, questionId))
            .OnStatus(TrainingQuestionStatusEnum.NoPrivilegesAvailable, HttpResult.Forbidden)
            .OnStatus(TrainingQuestionStatusEnum.OshProgramNotFound, HttpResult.NotFound)
            .Respond();
    }

    [HttpPut("{questionId:long}")]
    public IActionResult Update([FromRoute] long id, [FromRoute] long questionId,
        [FromBody] TrainingQuestionViewCreate view)
    {
        return new Response<TrainingQuestionViewCreate, TrainingQuestionStatusEnum>()
            .OnValidationError(validator.GetValidationProblems(view), HttpResult.ValidationProblem)
            .Handle(r => service.Update(id, questionId, r))
            .OnStatus(TrainingQuestionStatusEnum.NoPrivilegesAvailable, HttpResult.Forbidden)
            .OnStatus(TrainingQuestionStatusEnum.OshProgramNotFound, HttpResult.NotFound)
            .Respond();
    }

    [HttpDelete("{questionId:long}")]
    public IActionResult Delete([FromRoute] long id, [FromRoute] long questionId)
    {
        return new Response<TrainingQuestionViewCreate, TrainingQuestionStatusEnum>()
            .Handle(_ => service.Delete(id, questionId))
            .OnStatus(TrainingQuestionStatusEnum.NoPrivilegesAvailable, HttpResult.Forbidden)
            .OnStatus(TrainingQuestionStatusEnum.OshProgramNotFound, HttpResult.NotFound)
            .Respond();
    }
}

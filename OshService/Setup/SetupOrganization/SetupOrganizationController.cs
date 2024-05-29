using System.Net.Mime;
using System.Security.Claims;
using AspBoot.Auth;
using AspBoot.Handler;
using AspBoot.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OshService.Domain.User.User;

namespace OshService.Setup.SetupOrganization;

[ApiController]
[Route("api/setup/organization")]
[Authorize(Roles = nameof(UserType.Admin))]
[Produces(MediaTypeNames.Application.Json), Consumes(MediaTypeNames.Application.Json)]
public class SetupOrganizationController(
    IValidator<SetupOrganizationRequest> validator,
    SetupOrganizationService service
) : Controller
{
    [HttpPost]
    public IActionResult CreateOrganization([FromBody] SetupOrganizationRequest request)
    {
        return new Response<SetupOrganizationRequest, SetupOrganizationStatusEnum>()
            .OnValidationError(validator.GetValidationProblems(request), HttpResult.ValidationProblem)
            .Handle(r => service.CreateOrganization(User.GetPrincipal(ClaimTypes.Name)!, r))
            .OnStatus(SetupOrganizationStatusEnum.NoPrivilegesAvailable, HttpResult.Forbidden)
            .OnStatus(SetupOrganizationStatusEnum.OrganizationUrlExists, HttpResult.Conflict)
            .Respond();
    }
}

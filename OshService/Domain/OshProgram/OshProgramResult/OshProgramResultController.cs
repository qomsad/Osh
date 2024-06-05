using System.Net.Mime;
using AspBoot.Data.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OshService.Domain.User.User;

namespace OshService.Domain.OshProgram.OshProgramResult;

[ApiController]
[Route("api/osh-program-result")]
[Authorize(Roles = nameof(UserType.Admin))]
[Produces(MediaTypeNames.Application.Json)]
public class OshProgramResultController(OshProgramResultService service) : Controller
{
    [HttpGet]
    public IActionResult GetResult([FromQuery] RequestPage request)
    {
        return Ok(service.GetResults(request));
    }
}

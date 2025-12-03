using Microsoft.AspNetCore.Mvc;

namespace DevIO.Api.Controllers;

public class BaseController : ControllerBase
{
    [HttpGet("[controller]/index")]
    public Task<OkObjectResult> Index()
    {
        return Task.FromResult(Ok("I'm alive"));
    }
}
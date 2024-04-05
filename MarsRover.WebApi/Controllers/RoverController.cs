using Microsoft.AspNetCore.Mvc;
using Request = MarsRover.WebApi.DTOs.Request;

namespace MarsRover.WebApi.Controllers;

[ApiController]
[Route("/api/rover/")]
public class RoverController
{
    [HttpPost]
    [Route("position")]
    public async Task<IActionResult> Move(Request.Move move)
    {
        throw new NotImplementedException();
    }
}
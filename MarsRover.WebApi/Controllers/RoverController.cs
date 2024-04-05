using MarsRover.Domain.Rover;
using Microsoft.AspNetCore.Mvc;
using Request = MarsRover.WebApi.DTOs.Request;

namespace MarsRover.WebApi.Controllers;

[ApiController]
[Route("/api/rover/")]
public class RoverController(IRover rover) : ControllerBase
{
    private readonly IRover _rover = rover;

    [HttpPost]
    [Route("position")]
    public IActionResult Move(Request.Move move)
    {
        var result = _rover.ExecuteCommands(move.Commands);

        return Ok(result);
    }
}
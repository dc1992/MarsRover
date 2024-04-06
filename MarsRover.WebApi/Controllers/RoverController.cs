using MarsRover.Domain.Rover;
using Microsoft.AspNetCore.Mvc;
using Request = MarsRover.WebApi.DTOs.Request;

namespace MarsRover.WebApi.Controllers;

[ApiController]
[Route("/api/rover/")]
public class RoverController(IRover rover) : ControllerBase
{
    [HttpPost]
    [Route("position")]
    public IActionResult Move(Request.Move move)
    {
        var result = rover.ExecuteCommands(move.Commands);

        return Ok(result);
    }
}
using MarsRover.Domain.Rover;
using MarsRover.Domain.Shared;
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

        return MapResultInAppropriateStatusCode(result);
    }

    private IActionResult MapResultInAppropriateStatusCode(ExecutionResult result)
    {
        if (result.Status == Statuses.ObstacleFound)
            return StatusCode(206, result);
        
        return Ok(result);
    }
}
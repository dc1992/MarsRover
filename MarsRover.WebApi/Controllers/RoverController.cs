using MarsRover.Domain.Rover;
using MarsRover.Domain.Shared;
using Microsoft.AspNetCore.Mvc;
using Request = MarsRover.WebApi.DTOs.Request;

namespace MarsRover.WebApi.Controllers;

[ApiController]
[Route("/api/rover/")]
public class RoverController(IRover rover) : ControllerBase
{
    /// <remarks>
    /// Sample request:
    ///
    ///     {
    ///       "commands": [
    ///         "L", "R", "F", "B"
    ///       ]
    ///     }
    ///
    /// </remarks>
    [HttpPost]
    [Route("position")]
    [ProducesResponseType(typeof(ExecutionResult), 200)]
    [ProducesResponseType(typeof(ExecutionResult), 206)]
    [ProducesResponseType(400)]
    public IActionResult Move(Request.Move move)
    {
        var result = rover.ExecuteCommands(move.Commands);

        return MapExecutionResultInAppropriateStatusCode(result);
    }

    private IActionResult MapExecutionResultInAppropriateStatusCode(ExecutionResult result)
    {
        if (result.Status == Statuses.ObstacleFound)
            return StatusCode(206, result);
        
        return Ok(result);
    }
}
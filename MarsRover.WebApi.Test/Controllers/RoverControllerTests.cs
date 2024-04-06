using MarsRover.Domain.Rover;
using MarsRover.Domain.Shared;
using MarsRover.WebApi.Controllers;
using MarsRover.WebApi.DTOs.Request;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace MarsRover.WebApi.Test.Controllers;

public class RoverControllerTests
{
    [Test]
    public void Move_IRoverReturnsOk_ShouldReturnOk()
    {
        var input = new List<string> { Commands.Left };
        var output = new ExecutionResult(new Coordinates(0, 0), Direction.NORTH, 
            new Coordinates(0, 0), Direction.WEST, Statuses.Ok);
        
        var rover = new Mock<IRover>();
        rover
            .Setup(r => r.ExecuteCommands(input))
            .Returns(output);
            
        
        var controller = new RoverController(rover.Object);
        var executionResult = controller.Move(new Move { Commands = input });

        
        var okResult = executionResult as OkObjectResult;
        Assert.That(okResult.Value, Is.EqualTo(output));
        Assert.That(okResult.StatusCode, Is.EqualTo(200));
    }
    
    [Test]
    public void Move_IRoverReturnsObstacleFound_ShouldReturnPartialContent()
    {
        var input = new List<string> { Commands.Left };
        var output = new ExecutionResult(new Coordinates(0, 0), Direction.NORTH, 
            new Coordinates(0, 0), Direction.WEST, Statuses.ObstacleFound);
        
        var rover = new Mock<IRover>();
        rover
            .Setup(r => r.ExecuteCommands(input))
            .Returns(output);
            
        
        var controller = new RoverController(rover.Object);
        var executionResult = controller.Move(new Move { Commands = input });

        
        var result = executionResult as ObjectResult;
        Assert.That(result.Value, Is.EqualTo(output));
        Assert.That(result.StatusCode, Is.EqualTo(206));
    }
}
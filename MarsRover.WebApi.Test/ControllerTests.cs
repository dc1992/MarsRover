using MarsRover.Domain.Rover;
using MarsRover.Domain.Shared;
using MarsRover.WebApi.Controllers;
using MarsRover.WebApi.DTOs.Request;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace MarsRover.WebApi.Test;

public class ControllerTests
{
    [Test]
    public void Move_ShouldCallIRoverAndReturnOk()
    {
        var input = new List<string> { Commands.Left };
        var output = new ExecutionResult();
        
        var rover = new Mock<IRover>();
        rover
            .Setup(r => r.ExecuteCommands(input))
            .Returns(output);
            
        
        var controller = new RoverController(rover.Object);
        var executionResult = controller.Move(new Move { Commands = input });

        
        var okResult = executionResult as OkObjectResult;
        Assert.That(okResult.Value, Is.EqualTo(output));
    }
}
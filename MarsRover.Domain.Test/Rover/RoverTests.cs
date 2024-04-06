using MarsRover.Domain.Planet;
using MarsRover.Domain.Shared;
using RoverDir = MarsRover.Domain.Rover;
using Moq;

namespace MarsRover.Domain.Test.Rover;

public class RoverTests
{
    private Mock<IPlanet> _planet;
    private RoverDir.Rover _rover;
    
    [SetUp]
    public void Setup()
    {
        _planet = new Mock<IPlanet>();
        _planet
            .Setup(p => p.Height)
            .Returns(10);
        _planet
            .Setup(p => p.Width)
            .Returns(10);
        _rover = new RoverDir.Rover(_planet.Object);
    }

    [Test]
    public void ExecuteCommands_LeftCommand_ShouldRotateLeft()
    {
        var result = _rover.ExecuteCommands([Commands.Left]);

        Assert.That(result.StartingPoint.X, Is.EqualTo(0));
        Assert.That(result.StartingPoint.Y, Is.EqualTo(0));
        Assert.That(result.StartingDirection, Is.EqualTo(Direction.NORTH));
        
        Assert.That(result.DestinationPoint.X, Is.EqualTo(0));
        Assert.That(result.DestinationPoint.Y, Is.EqualTo(0));
        Assert.That(result.DestinationDirection, Is.EqualTo(Direction.WEST));
        
        Assert.That(result.Status, Is.EqualTo(Statuses.Ok));
    }
    
    [Test]
    public void ExecuteCommands_RightCommand_ShouldRotateRight()
    {
        var result = _rover.ExecuteCommands([Commands.Right]);

        Assert.That(result.StartingPoint.X, Is.EqualTo(0));
        Assert.That(result.StartingPoint.Y, Is.EqualTo(0));
        Assert.That(result.StartingDirection, Is.EqualTo(Direction.NORTH));
        
        Assert.That(result.DestinationPoint.X, Is.EqualTo(0));
        Assert.That(result.DestinationPoint.Y, Is.EqualTo(0));
        Assert.That(result.DestinationDirection, Is.EqualTo(Direction.EAST));
        
        Assert.That(result.Status, Is.EqualTo(Statuses.Ok));
    }
    
    [Test]
    public void ExecuteCommands_ForwardCommand_ShouldMoveForward()
    {
        var result = _rover.ExecuteCommands([Commands.Forward]);

        Assert.That(result.StartingPoint.X, Is.EqualTo(0));
        Assert.That(result.StartingPoint.Y, Is.EqualTo(0));
        Assert.That(result.StartingDirection, Is.EqualTo(Direction.NORTH));
        
        Assert.That(result.DestinationPoint.X, Is.EqualTo(0));
        Assert.That(result.DestinationPoint.Y, Is.EqualTo(1));
        Assert.That(result.DestinationDirection, Is.EqualTo(Direction.NORTH));
        
        Assert.That(result.Status, Is.EqualTo(Statuses.Ok));
    }
    
    [Test]
    public void ExecuteCommands_BackwardCommand_ShouldMoveBackward()
    {
        var result = _rover.ExecuteCommands([Commands.Backward]);

        Assert.That(result.StartingPoint.X, Is.EqualTo(0));
        Assert.That(result.StartingPoint.Y, Is.EqualTo(0));
        Assert.That(result.StartingDirection, Is.EqualTo(Direction.NORTH));
        
        Assert.That(result.DestinationPoint.X, Is.EqualTo(0));
        Assert.That(result.DestinationPoint.Y, Is.EqualTo(9));
        Assert.That(result.DestinationDirection, Is.EqualTo(Direction.NORTH));
        
        Assert.That(result.Status, Is.EqualTo(Statuses.Ok));
    }
    
    [Test]
    public void ExecuteCommands_MultipleCommandsWithoutObstacles_ShouldMoveToExpectedPoint()
    {
        _planet
            .Setup(p => p.CheckForObstacle(It.IsAny<Coordinates>()))
            .Returns(false);
        
        var result = _rover.ExecuteCommands([Commands.Forward, Commands.Forward, Commands.Right, Commands.Forward, Commands.Right, Commands.Forward]);

        Assert.That(result.StartingPoint.X, Is.EqualTo(0));
        Assert.That(result.StartingPoint.Y, Is.EqualTo(0));
        Assert.That(result.StartingDirection, Is.EqualTo(Direction.NORTH));
        
        Assert.That(result.DestinationPoint.X, Is.EqualTo(1));
        Assert.That(result.DestinationPoint.Y, Is.EqualTo(1));
        Assert.That(result.DestinationDirection, Is.EqualTo(Direction.SOUTH));
        
        Assert.That(result.Status, Is.EqualTo(Statuses.Ok));
    }
    
    [Test]
    public void ExecuteCommands_MultipleCommandsWithObstacles_ShouldMoveToExpectedPointAndReturnObstacleCode()
    {
        _planet
            .Setup(p => p.CheckForObstacle(It.IsAny<Coordinates>()))
            .Returns(false);
        _planet
            .Setup(p => p.CheckForObstacle(It.Is<Coordinates>(co => co.X == 1 && co.Y == 1)))
            .Returns(true);
        
        var result = _rover.ExecuteCommands([Commands.Forward, Commands.Forward, Commands.Right, Commands.Forward, Commands.Right, Commands.Forward]);

        Assert.That(result.StartingPoint.X, Is.EqualTo(0));
        Assert.That(result.StartingPoint.Y, Is.EqualTo(0));
        Assert.That(result.StartingDirection, Is.EqualTo(Direction.NORTH));
        
        Assert.That(result.DestinationPoint.X, Is.EqualTo(1));
        Assert.That(result.DestinationPoint.Y, Is.EqualTo(2));
        Assert.That(result.DestinationDirection, Is.EqualTo(Direction.SOUTH));
        
        Assert.That(result.Status, Is.EqualTo(Statuses.ObstacleFound));
    }

    [Test]
    public void ExecuteCommands_RoverSurpassesNorthEdge_ShouldMoveToExpectedPoint()
    {
        _planet
            .Setup(p => p.CheckForObstacle(It.IsAny<Coordinates>()))
            .Returns(false);
        
        var result = _rover.ExecuteCommands([Commands.Forward, Commands.Forward, Commands.Forward, Commands.Forward, Commands.Forward, 
            Commands.Forward, Commands.Forward, Commands.Forward, Commands.Forward, Commands.Forward]);

        Assert.That(result.StartingPoint.X, Is.EqualTo(0));
        Assert.That(result.StartingPoint.Y, Is.EqualTo(0));
        Assert.That(result.StartingDirection, Is.EqualTo(Direction.NORTH));
        
        Assert.That(result.DestinationPoint.X, Is.EqualTo(0));
        Assert.That(result.DestinationPoint.Y, Is.EqualTo(0));
        Assert.That(result.DestinationDirection, Is.EqualTo(Direction.NORTH));
        
        Assert.That(result.Status, Is.EqualTo(Statuses.Ok));
    }
    
    [Test]
    public void ExecuteCommands_RoverSurpassesEastEdge_ShouldMoveToExpectedPoint()
    {
        _planet
            .Setup(p => p.CheckForObstacle(It.IsAny<Coordinates>()))
            .Returns(false);
        
        var result = _rover.ExecuteCommands([Commands.Right, Commands.Forward, Commands.Forward, Commands.Forward, Commands.Forward, Commands.Forward, 
            Commands.Forward, Commands.Forward, Commands.Forward, Commands.Forward, Commands.Forward]);

        Assert.That(result.StartingPoint.X, Is.EqualTo(0));
        Assert.That(result.StartingPoint.Y, Is.EqualTo(0));
        Assert.That(result.StartingDirection, Is.EqualTo(Direction.NORTH));
        
        Assert.That(result.DestinationPoint.X, Is.EqualTo(0));
        Assert.That(result.DestinationPoint.Y, Is.EqualTo(0));
        Assert.That(result.DestinationDirection, Is.EqualTo(Direction.EAST));
        
        Assert.That(result.Status, Is.EqualTo(Statuses.Ok));
    }
    
    [Test]
    public void ExecuteCommands_RoverSurpassesWestEdge_ShouldMoveToExpectedPoint()
    {
        _planet
            .Setup(p => p.CheckForObstacle(It.IsAny<Coordinates>()))
            .Returns(false);
        
        var result = _rover.ExecuteCommands([Commands.Left, Commands.Forward]);

        Assert.That(result.StartingPoint.X, Is.EqualTo(0));
        Assert.That(result.StartingPoint.Y, Is.EqualTo(0));
        Assert.That(result.StartingDirection, Is.EqualTo(Direction.NORTH));
        
        Assert.That(result.DestinationPoint.X, Is.EqualTo(9));
        Assert.That(result.DestinationPoint.Y, Is.EqualTo(0));
        Assert.That(result.DestinationDirection, Is.EqualTo(Direction.WEST));
        
        Assert.That(result.Status, Is.EqualTo(Statuses.Ok));
    }
}
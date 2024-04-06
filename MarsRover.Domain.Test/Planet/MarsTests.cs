using MarsRover.Domain.Planet;
using MarsRover.Domain.Shared;

namespace MarsRover.Domain.Test.Planet;

public class MarsTests
{
    private Mars _mars;
    
    [SetUp]
    public void Setup()
    {
        _mars = new Mars();
    }

    [TestCase(1, 1)]
    [TestCase(5, 5)]
    [TestCase(9, 9)]
    public void CheckForObstacle_ObstacleDetected_ShouldReturnTrue(int x, int y)
    {
        Assert.That(_mars.CheckForObstacle(new Coordinates(x, y)), Is.True);
    }
    
    [TestCase(1, 0)]
    [TestCase(5, 7)]
    [TestCase(3, 9)]
    public void CheckForObstacle_ObstacleNotDetected_ShouldReturnFalse(int x, int y)
    {
        Assert.That(_mars.CheckForObstacle(new Coordinates(x, y)), Is.False);
    }
}
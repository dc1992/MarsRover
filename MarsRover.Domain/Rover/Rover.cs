using MarsRover.Domain.Planet;
using MarsRover.Domain.Shared;

namespace MarsRover.Domain.Rover;

//for simplicity's sake I assume that the starting point is fixed
public class Rover(IPlanet planet, Direction currentDirection = Direction.NORTH, int startingXPosition = 0, int startingYPosition = 0)
    : IRover
{
    private Coordinates _currentPosition = new(startingXPosition, startingYPosition);

    public ExecutionResult ExecuteCommands(ICollection<string> commands)
    {
        var startingPoint = _currentPosition;
        var startingDirection = currentDirection;
        
        foreach (var command in commands)
        {
            switch (command)
            {
                case Commands.Left:
                    RotateCounterClockwise();
                    break;
                
                case Commands.Right:
                    RotateClockwise();
                    break;

                case Commands.Forward:
                case Commands.Backward:
                {
                    var steps = command == Commands.Forward ? 1 : -1;
                    var nextPosition = CalculateNextPosition(steps);
                    
                    if (planet.CheckForObstacle(nextPosition))
                        return new ExecutionResult(startingPoint, startingDirection, _currentPosition, currentDirection, Statuses.ObstacleFound);
                    
                    _currentPosition = nextPosition;
                    break;
                }
            }
        }

        var executionResult = new ExecutionResult(startingPoint, startingDirection, _currentPosition, currentDirection, Statuses.Ok);

        return executionResult;
    }

    private void RotateCounterClockwise()
    {
        currentDirection = currentDirection switch
        {
            Direction.NORTH => Direction.WEST,
            Direction.WEST => Direction.SOUTH,
            Direction.SOUTH => Direction.EAST,
            Direction.EAST => Direction.NORTH
        };
    }
    
    private void RotateClockwise()
    {
        currentDirection = currentDirection switch
        {
            Direction.NORTH => Direction.EAST,
            Direction.EAST => Direction.SOUTH,
            Direction.SOUTH => Direction.WEST,
            Direction.WEST => Direction.NORTH
        };
    }

    private Coordinates CalculateNextPosition(int numberOfSteps)
    {
        return currentDirection switch
        {
            Direction.NORTH => new Coordinates(_currentPosition.X, CalculateIndex(_currentPosition.Y, numberOfSteps,planet.Height)),
            Direction.EAST => new Coordinates(CalculateIndex(_currentPosition.X, numberOfSteps,planet.Width), _currentPosition.Y),
            Direction.SOUTH => new Coordinates(_currentPosition.X, CalculateIndex(_currentPosition.Y, -numberOfSteps,planet.Height)),
            Direction.WEST => new Coordinates(CalculateIndex(_currentPosition.X, -numberOfSteps,planet.Width), _currentPosition.Y)
        };
    }

    private int CalculateIndex(int currentPosition, int step, int totalLength)
    {
        return ((currentPosition + step) % totalLength + totalLength) % totalLength;
    }
}
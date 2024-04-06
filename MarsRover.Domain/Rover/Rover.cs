using MarsRover.Domain.Shared;

namespace MarsRover.Domain.Rover;

//for simplicity's sake I assume that the starting point is fixed
public class Rover(Direction currentDirection = Direction.NORTH, int startingXPosition = 0, int startingYPosition = 0)
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
                    Move(1);
                    break;
                case Commands.Backward:
                    Move(-1);
                    break;
            }
        }

        var destinationPoint = _currentPosition;
        var destinationDirection = currentDirection;
        var status = Statuses.Ok;

        var executionResult = new ExecutionResult(startingPoint, startingDirection, destinationPoint, destinationDirection, status);

        return executionResult;
    }

    private void RotateCounterClockwise()
    {
        currentDirection = currentDirection switch
        {
            Direction.NORTH => Direction.WEST,
            Direction.WEST => Direction.SOUTH,
            Direction.SOUTH => Direction.EAST,
            Direction.EAST => Direction.NORTH,
            _ => currentDirection
        };
    }
    
    private void RotateClockwise()
    {
        currentDirection = currentDirection switch
        {
            Direction.NORTH => Direction.EAST,
            Direction.EAST => Direction.SOUTH,
            Direction.SOUTH => Direction.WEST,
            Direction.WEST => Direction.NORTH,
            _ => currentDirection
        };
    }

    private void Move(int numberOfSteps)
    {
        _currentPosition = currentDirection switch
        {
            Direction.NORTH => new Coordinates(_currentPosition.X, _currentPosition.Y + numberOfSteps),
            Direction.EAST => new Coordinates(_currentPosition.X + numberOfSteps, _currentPosition.Y),
            Direction.SOUTH => new Coordinates(_currentPosition.X, _currentPosition.Y - numberOfSteps),
            Direction.WEST => new Coordinates(_currentPosition.X - numberOfSteps, _currentPosition.Y),
            _ => _currentPosition
        };
    }
}
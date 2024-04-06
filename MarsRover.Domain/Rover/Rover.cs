using MarsRover.Domain.Shared;

namespace MarsRover.Domain.Rover;

public class Rover : IRover
{
    private Direction _currentDirection;
    private Coordinates _currentPosition;
    
    public Rover()
    {
        //for simplicity's sake I assume that the starting point is fixed 
        _currentDirection = Direction.NORTH;
        _currentPosition = new Coordinates(0, 0);
    }
    
    public ExecutionResult ExecuteCommands(ICollection<string> commands)
    {
        var startingPoint = _currentPosition;
        var startingDirection = _currentDirection;
        
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
        var destinationDirection = _currentDirection;
        var status = "OK";

        var executionResult = new ExecutionResult(startingPoint, startingDirection, destinationPoint, destinationDirection, status);

        return executionResult;
    }

    private void RotateCounterClockwise()
    {
        _currentDirection = _currentDirection switch
        {
            Direction.NORTH => Direction.WEST,
            Direction.WEST => Direction.SOUTH,
            Direction.SOUTH => Direction.EAST,
            Direction.EAST => Direction.NORTH,
            _ => _currentDirection
        };
    }
    
    private void RotateClockwise()
    {
        _currentDirection = _currentDirection switch
        {
            Direction.NORTH => Direction.EAST,
            Direction.EAST => Direction.SOUTH,
            Direction.SOUTH => Direction.WEST,
            Direction.WEST => Direction.NORTH,
            _ => _currentDirection
        };
    }

    private void Move(int numberOfSteps)
    {
        _currentPosition = _currentDirection switch
        {
            Direction.NORTH => new Coordinates(_currentPosition.X, _currentPosition.Y + numberOfSteps),
            Direction.EAST => new Coordinates(_currentPosition.X + numberOfSteps, _currentPosition.Y),
            Direction.SOUTH => new Coordinates(_currentPosition.X, _currentPosition.Y - numberOfSteps),
            Direction.WEST => new Coordinates(_currentPosition.X - numberOfSteps, _currentPosition.Y),
            _ => _currentPosition
        };
    }
}
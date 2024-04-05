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
        throw new NotImplementedException();
    }
}
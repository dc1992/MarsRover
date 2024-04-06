namespace MarsRover.Domain.Shared;

public class ExecutionResult(Coordinates startingPoint, Direction startingDirection, Coordinates arrivalPoint, 
    Direction arrivalDirection, string status)
{
    public Coordinates StartingPoint { get; } = startingPoint;
    public Direction StartingDirection { get; } = startingDirection;
    
    public Coordinates ArrivalPoint { get; } = arrivalPoint;
    public Direction ArrivalDirection { get; } = arrivalDirection;
    
    public string Status { get; } = status;
}
namespace MarsRover.Domain.Shared;

public class ExecutionResult(Coordinates startingPoint, Direction startingDirection, Coordinates destinationPoint, 
    Direction destinationDirection, string status)
{
    public Coordinates StartingPoint { get; } = startingPoint;
    public Direction StartingDirection { get; } = startingDirection;
    
    public Coordinates DestinationPoint { get; } = destinationPoint;
    public Direction DestinationDirection { get; } = destinationDirection;
    
    public string Status { get; } = status;
}
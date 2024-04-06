using MarsRover.Domain.Shared;

namespace MarsRover.Domain.Planet;

public interface IPlanet
{
    public int Length { get; }
    public int Height { get; }
    public bool CheckForObstacle(Coordinates coordinates);
}
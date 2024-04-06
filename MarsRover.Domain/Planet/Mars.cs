using MarsRover.Domain.Shared;

namespace MarsRover.Domain.Planet;

public class Mars : IPlanet
{
    public int Length { get; }
    public int Height { get; }
    private ICollection<Coordinates> _obstacles;

    //for simplicity's sake I assume that planet dimensions and obstacles are fixed
    public Mars()
    {
        Length = 10;
        Height = 10;
        _obstacles = new List<Coordinates>
        {
            new(1, 1),
            new(5, 5),
            new(9, 9)
        };
    }

    public bool CheckForObstacle(Coordinates coordinates)
    {
        return _obstacles
            .Any(obstacle => obstacle.X == coordinates.X 
                             && obstacle.Y == coordinates.Y);
    }
}
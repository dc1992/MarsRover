namespace MarsRover.Domain.Shared;

public class Coordinates(int x, int y)
{
    public int X { get; } = x;
    public int Y { get; } = y;
}
namespace MarsRover.Domain.Shared;

public static class Commands
{
    public const string Left = "L", 
        Right = "R", 
        Forward = "F", 
        Backward = "B";
    
    public static readonly string[] ValidCommands = [ Left, Right, Forward, Backward ];
}
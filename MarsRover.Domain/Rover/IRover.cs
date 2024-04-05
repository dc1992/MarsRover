using MarsRover.Domain.Shared;

namespace MarsRover.Domain.Rover;

public interface IRover
{
    public ExecutionResult ExecuteCommands(ICollection<string> commands);
}
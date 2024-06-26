using System.ComponentModel.DataAnnotations;
using static MarsRover.Domain.Shared.Commands;

namespace MarsRover.WebApi.DTOs.Request;

public class Move : IValidatableObject
{
    public ICollection<string> Commands { get; set; }
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var results = new List<ValidationResult>();
        
        if (Commands == null || !Commands.Any())
            results.Add(new ValidationResult($"{nameof(Commands)} should not be empty"));

        if (Commands != null && Commands.Any(command => !ValidCommands.Contains(command)))
            results.Add(new ValidationResult($"{nameof(Commands)} should only contain valid commands: [{string.Join(',', ValidCommands)}]"));
        
        return results;
    }
}
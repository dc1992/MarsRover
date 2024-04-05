using MarsRover.WebApi.DTOs.Request;

namespace MarsRover.WebApi.Test;

public class ValidationTests
{
    [Test]
    public void EmptyRequest_ShouldReturnEmptyCommandsError()
    {
        var moveReq = new Move();

        var validationResults = moveReq.Validate(null);
        
        Assert.That(validationResults.First().ErrorMessage, Is.EqualTo("Commands should not be empty"));
    }
    
    [Test]
    public void TotallyInvalidRequest_ShouldReturnInvalidCommandsError()
    {
        var moveReq = new Move
        {
            Commands = [ "WRONG_PARAM", "ANOTHER_WRONG_PARAM" ]
        };

        var validationResults = moveReq.Validate(null);
        
        Assert.That(validationResults.First().ErrorMessage, Is.EqualTo($"Commands should only contain valid commands: [{string.Join(',', Constants.Commands.ValidCommands)}]"));
    }
    
    [Test]
    public void PartiallyInvalidRequest_ShouldReturnInvalidCommandsError()
    {
        var moveReq = new Move
        {
            Commands = [ "WRONG_PARAM", Constants.Commands.Left ]
        };

        var validationResults = moveReq.Validate(null);
        
        Assert.That(validationResults.First().ErrorMessage, Is.EqualTo($"Commands should only contain valid commands: [{string.Join(',', Constants.Commands.ValidCommands)}]"));
    }
    
    [Test]
    public void ValidRequest_ShouldReturnEmptyValidationResult()
    {
        var moveReq = new Move
        {
            Commands = [ Constants.Commands.Left ]
        };

        var validationResults = moveReq.Validate(null);
        
        Assert.That(validationResults, Is.Empty);
    }
}
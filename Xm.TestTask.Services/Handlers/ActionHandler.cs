using FluentResults;
using Serilog;
using Xm.TestTask.Messages;
using Xm.TestTask.Services.Utilities;

namespace Xm.TestTask.Services.Strategies;

/// <summary>
/// Handler for message type of action. 
/// </summary>
public class ActionHandler : IMessageHandler
{
    private readonly ILogger _logger;

    public ActionHandler(ILogger logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// returns action
    /// </summary>
    public string DataType => "action";

    /// <summary>
    /// handle action message data, deserialiaze it to ActionMessage
    /// </summary>
    /// <param name="message">Message data</param>
    /// <returns>Result</returns>
    public async Task<Result> HandleAsync(byte[] message)
    {
        var action = SerializerUtilities.DeserializeFromBytes<ActionMessage>(message);

        if (action.IsFailed)
        {
            _logger.Error("Could not deserialize {@dataType}. Errors: {@errors}", DataType, action.Errors);
            return action.ToResult();
        }

        // Do some very important job
        await Task.Delay(100);

        _logger.Information("{@dataType} was saved successfully. Action {@actionName}", DataType, action.Value.Name);
        return Result.Ok();
    }
}

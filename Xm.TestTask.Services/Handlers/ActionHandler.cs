using FluentResults;
using Serilog;
using Xm.TestTask.Messages;
using Xm.TestTask.Services.Utilities;

namespace Xm.TestTask.Services.Strategies;

public class ActionHandler : IMessageHandler
{
    private readonly ILogger _logger;

    public ActionHandler(ILogger logger)
    {
        _logger = logger;
    }

    public string DataType => "action";

    public async Task<Result> HandleAsync(byte[] message)
    {
        await Task.Delay(100);
        var action = SerializerUtilities.DeserializeFromBytes<ActionMessage>(message);

        if (action.IsFailed)
        {
            _logger.Error("Could not deserialize {@dataType}. Errors: {@errors}", DataType, action.Errors);
            return action.ToResult();
        }

        _logger.Information("{@dataType} was saved successfully. Action {@actionName}", DataType, action.Value.Name);
        return Result.Ok();
    }
}

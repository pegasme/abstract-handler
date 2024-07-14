using FluentResults;
using Serilog;
using System;
using Xm.TestTask.Messages;
using Xm.TestTask.Services.Utilities;

namespace Xm.TestTask.Services.Strategies;

public class NotificationHandler : IMessageHandler
{
    private readonly ILogger _logger;

    public NotificationHandler(ILogger logger)
    {
        _logger = logger;
    }

    public string DataType => "notification";

    public async Task<Result> HandleAsync(byte[] message)
    {
        await Task.Delay(50);

        var notification = SerializerUtilities.DeserializeFromBytes<NotificationMessage>(message);

        if (notification.IsFailed)
        {
            _logger.Error("Could not deserialize {@dataType}. Errors: {@errors}", DataType, notification.Errors);
            return notification.ToResult();
        }

        _logger.Information("{@dataType} was saved successfully. Notification Message: {message}", DataType, notification.Value.Message);
        return Result.Ok();
    }
}

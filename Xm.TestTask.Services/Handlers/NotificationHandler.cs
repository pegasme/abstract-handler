using FluentResults;
using Serilog;
using System;
using Xm.TestTask.Messages;
using Xm.TestTask.Services.Utilities;

namespace Xm.TestTask.Services.Strategies;

/// <summary>
/// Handler for message type of notification. 
/// </summary>
public class NotificationHandler : IMessageHandler
{
    private readonly ILogger _logger;

    public NotificationHandler(ILogger logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// returns notification
    /// </summary>
    public string DataType => "notification";

    // <summary>
    /// handle action message data, deserialize it to NotificationMessage
    /// </summary>
    /// <param name="message">Message data</param>
    /// <returns>Result</returns>
    public async Task<Result> HandleAsync(byte[] message)
    {
        var notification = SerializerUtilities.DeserializeFromBytes<NotificationMessage>(message);

        if (notification.IsFailed)
        {
            _logger.Error("Could not deserialize {@dataType}. Errors: {@errors}", DataType, notification.Errors);
            return notification.ToResult();
        }

        // Do some very important job
        await Task.Delay(50);

        _logger.Information("{@dataType} was saved successfully. Notification Message: {message}", DataType, notification.Value.Message);
        return Result.Ok();
    }
}

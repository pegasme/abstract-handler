using FluentResults;
using Serilog;
using Serilog.Core;

namespace Xm.TestTask.Services.Strategies;

/// <summary>
/// Handler for message type of avatar. 
/// </summary>
public class AvatarHandler : IMessageHandler
{
    private readonly ILogger _logger;

    public AvatarHandler(ILogger logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// returns avatar
    /// </summary>
    public string DataType => "avatar";

    /// <summary>
    /// handle action message data it to AvatarMessage
    /// </summary>
    /// <param name="message">Message data</param>
    /// <returns>Result</returns>
    public async Task<Result> HandleAsync(byte[] message)
    {
        await Task.Delay(50);
        _logger.Information("{@dataType} was saved successfully", DataType);
        return Result.Ok();
    }
}

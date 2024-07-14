using FluentResults;
using Serilog;
using Serilog.Core;

namespace Xm.TestTask.Services.Strategies;

public class AvatarHandler : IMessageHandler
{
    private readonly ILogger _logger;

    public AvatarHandler(ILogger logger)
    {
        _logger = logger;
    }
    public string DataType => "avatar";

    public async Task<Result> HandleAsync(byte[] message)
    {
        await Task.Delay(50);
        _logger.Information("{@dataType} was saved successfully", DataType);
        return Result.Ok();
    }
}

using FluentResults;
using Serilog;
using Xm.TestTask.Services.Strategies;

namespace Xm.TestTask.Services;

public interface IProcessMessageService
{
    Task<Result> ProcessMessageAsync(string messageType, byte[] data);
}

public class ProcessMessageService : IProcessMessageService
{
    private readonly Dictionary<string, IMessageHandler> messageHandlers;
    private readonly ILogger _logger;

    public ProcessMessageService(IEnumerable<IMessageHandler> strategies, ILogger logger)
    {
        messageHandlers = strategies.ToDictionary(s => s.DataType, s => s);
        _logger = logger;
    }

    public async Task<Result> ProcessMessageAsync(string messageType, byte[] data)
    {
        if (string.IsNullOrWhiteSpace(messageType))
        {
            _logger.Error($"{nameof(messageType)} is null or empty", messageType);
            return Result.Fail($"{nameof(messageType)} is null or empty");
        }

        if (!messageHandlers.TryGetValue(messageType, out var messageHandler))
        {
            _logger.Error("Handler for message type {@messageType} was not found", messageType);
            return Result.Fail($"Handler for message type {messageType} was not found");
        }

        return await messageHandler.HandleAsync(data);
    }
}

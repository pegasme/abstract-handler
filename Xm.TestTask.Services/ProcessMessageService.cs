using FluentResults;
using Serilog;
using Xm.TestTask.Services.Strategies;

namespace Xm.TestTask.Services;

public interface IProcessMessageService
{
    Task<Result> ProcessMessageAsync(string messageType, byte[] data, CancellationToken token = default);
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

    /// <summary>
    /// Call correct handlers depends on message type
    /// </summary>
    /// <param name="messageType">message type value - action, notification, avatar</param>
    /// <param name="data">message data</param>
    /// <returns>Result</returns>
    public async Task<Result> ProcessMessageAsync(string messageType, byte[] data, CancellationToken token = default)
    {
        if (string.IsNullOrWhiteSpace(messageType))
        {
            var errMessage = $"{nameof(messageType)} is null or empty";
            _logger.Error(errMessage);
            return Result.Fail(errMessage);
        }

        if (!messageHandlers.TryGetValue(messageType, out var messageHandler))
        {
            // use structured logging - message type is a parameter and we could found by it
            _logger.Error("Handler for message type {@messageType} was not found", messageType);
            return Result.Fail($"Handler for message type {messageType} was not found");
        }

        return await messageHandler.HandleAsync(data);
    }
}

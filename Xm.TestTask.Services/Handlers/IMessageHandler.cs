using FluentResults;

namespace Xm.TestTask.Services.Strategies;

public interface IMessageHandler
{
    string DataType { get; }

    Task<Result> HandleAsync(byte[] message);
}

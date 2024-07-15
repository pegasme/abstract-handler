using Xm.TestTask.Services;

namespace Xm.TestTask.Events;

public class EventListener : IDisposable
{
    private readonly IProcessMessageService _messageProcessService;
    private readonly IEventBus _eventBus;
    private readonly CancellationTokenSource _cts;

    public EventListener(IProcessMessageService messageProcessService, IEventBus eventBus)
    {
        _messageProcessService = messageProcessService;
        _eventBus = eventBus;
        _cts = new CancellationTokenSource();
    }

    public void Dispose()
    {
        _cts.Cancel();
    }

    public void StartListening()
    {
        Task.Run(async () =>
        {
            await foreach (var @event in _eventBus.ListenAsync(_cts.Token))
            {
                if (_cts.IsCancellationRequested)
                    break;
                await _messageProcessService.ProcessMessageAsync(@event.DataType, @event.Body, _cts.Token);
            }
        });
    }
}
using Xm.TestTask.Services;

namespace Xm.TestTask.Events;

public class EventListener
{
    private readonly IProcessMessageService _messageProcessService;
    private readonly IEventBus _eventBus;

    public EventListener(IProcessMessageService messageProcessService, IEventBus eventBus)
    {
        _messageProcessService = messageProcessService;
        _eventBus = eventBus;
    }

    public void StartListening()
    {
        Task.Run(async () =>
        {
            await foreach (var @event in _eventBus.ListenAsync())
            {
                await _messageProcessService.ProcessMessageAsync(@event.DataType, @event.Body);
            }
        });
    }
}
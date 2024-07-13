namespace Xm.TestTask.Events
{
    public class EventListener
    {
        private readonly IEventBus _eventBus;

        public EventListener(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void StartListening()
        {
            Task.Run(async () =>
            {
                await foreach (var @event in _eventBus.ListenAsync())
                {
                    // todo: handle
                }
            });
        }
    }
}
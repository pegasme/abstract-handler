namespace Xm.TestTask.Events
{
    public interface IEventBus
    {
        IAsyncEnumerable<Event> ListenAsync();
    }
}
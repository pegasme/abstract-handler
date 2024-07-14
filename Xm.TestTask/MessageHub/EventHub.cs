using Microsoft.AspNetCore.SignalR;
using System.Text;
using Xm.TestTask.Services;

namespace Xm.TestTask.MessageHub;

public class EventHub : Hub
{
    private readonly IProcessMessageService _messageProcessService;
    public EventHub(IProcessMessageService messageProcessService)
    {
        _messageProcessService = messageProcessService;
    }
    public async Task SendMessage(string dataType, string message)
    {
        var binaryMessage = Encoding.UTF8.GetBytes(message);
        await  _messageProcessService.ProcessMessageAsync(dataType, binaryMessage);
    }
}

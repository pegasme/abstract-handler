using Microsoft.AspNetCore.Mvc;
using System.Net;
using Xm.TestTask.Services;

namespace Xm.TestTask.Controllers;

[Route("[controller]")]
public class WebhookController : ControllerBase
{
    private readonly IProcessMessageService _messageProcessService;

    public WebhookController(IProcessMessageService messageProcessService)
    {
        _messageProcessService = messageProcessService;
    }

    
    [HttpPost]
    public async Task<IActionResult> HandleAsync()
    {
        var isDataTypeExists = Request.Headers.TryGetValue("x-xdt", out var dataType);
        var dataBody = await ReadRequestBodyAsync();

        if (!isDataTypeExists || string.IsNullOrWhiteSpace(dataType))
            return BadRequest(nameof(dataType));

        var result = await _messageProcessService.ProcessMessageAsync(dataType, dataBody);
        if (result.IsFailed)
            return StatusCode((int)HttpStatusCode.InternalServerError);

        return Ok();
    }


    private async Task<byte[]> ReadRequestBodyAsync()
    {
        using (var memoryStream = new MemoryStream())
        {
            await Request.Body.CopyToAsync(memoryStream);

            return memoryStream.ToArray();
        }
    }
}
using Microsoft.AspNetCore.Mvc;

namespace Xm.TestTask.Controllers
{
    public class WebhookController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> HandleAsync()
        {
            var dataType = Request.Headers["x-xdt"].First();
            var dataBody = await ReadRequestBodyAsync();

            // todo: handle

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
}
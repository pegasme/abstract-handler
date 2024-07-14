using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Xm.TestTask.OperationFilters;

public class BodyTypeOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.RequestBody = new OpenApiRequestBody() { Required = true };
        operation.RequestBody.Content.Add("text/plain", new OpenApiMediaType()
        {
            Schema = new OpenApiSchema()
            {
                Type = "string",
                Format = "binary",
            },
        });
    }
}


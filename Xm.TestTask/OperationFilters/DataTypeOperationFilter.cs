using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Xm.TestTask.OperationFilters;

public class DataTypeOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null)
            operation.Parameters = new List<OpenApiParameter>();

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "x-xdt",
            In = ParameterLocation.Header,
            Description = "Data type. Supported: avatar, action, notification",
            Required = false,
            Schema = new OpenApiSchema
            {
                Type = "String"
            }
        });
    }
}

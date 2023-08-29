using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Configurations;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace sunstealer.azure.functions;

public class DocumentFilter : IDocumentFilter
{
    public void Apply(IHttpRequestDataObject req, OpenApiDocument document)
    {
        var schemas = document.Components.Schemas;
        foreach (var schema in schemas)
        {
            if (schema.Key == "model1Example")
            {
                schema.Value.Example = new OpenApiArray() {
                    new OpenApiObject()
                    {
                        ["Key"] = new OpenApiInteger(1),
                        ["Value"] = new OpenApiString("value1")
                    },
                    new OpenApiObject()
                    {
                        ["Key"] = new OpenApiInteger(2),
                        ["Value"] = new OpenApiString("value2")
                    },
                    new OpenApiObject()
                    {
                        ["Key"] = new OpenApiInteger(3),
                        ["Value"] = new OpenApiString("value3")
                    }
                }; 
            }
        }
    }
}

public class OpenApiConfigurationOptions : DefaultOpenApiConfigurationOptions
{
    public OpenApiConfigurationOptions()
    {
        this.DocumentFilters.Add(new DocumentFilter());
    }

    public OpenApiInfo Info { get; set; } = new OpenApiInfo()
    {
        Version = "1.0.0",
        Title = "Azure Function API",
        Description = "Azure Function API with OpenAPI 3.x.",
    };

    public OpenApiVersionType OpenApiVersion { get; set; } = OpenApiVersionType.V3;
}
namespace sunstealer.azure.functions;

using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;

public class Model1Example : OpenApiExample<Model1>
{
    public override IOpenApiExample<Model1> Build(NamingStrategy? namingStrategy = null)
    {
        this.Examples.Add(OpenApiExampleResolver.Resolve("model1Example",
            new Model1() {
                new Model1Entry() { Key = 1, Value = "value1" },
                new Model1Entry() { Key = 2, Value = "value2" },
                new Model1Entry() { Key = 3, Value = "value3" },
            }, namingStrategy));

        return this;
    }
}

[OpenApiExample(typeof(Model1Example))]
public class Model1 : List<Model1Entry>
{
}

public class Model1Entry
{
    public int Key { get; set; } = 0;
    public string Value { get; set; } = string.Empty;
}

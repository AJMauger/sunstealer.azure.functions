using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace sunstealer.azure.functions
{
    public class Function1
    {
        private readonly ILogger _logger;

        public Function1(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Function1>();
        }

        [Function("Function1")]
        [OpenApiOperation(operationId: "Post")]
        [OpenApiRequestBody("application/json", typeof(Model1Example), Description = "Description.Model1", Example = typeof(Model1Example))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Model1Example), Description = "Description.Response", Example = typeof(Model1Example))]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "APIKey", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "parameter1", In = ParameterLocation.Query, Required = false, Type = typeof(string))]
        public async Task<HttpResponseData> Post([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            try
            {
                _logger.LogInformation("Function1.");
                var payload = await req.ReadFromJsonAsync<Model1>();
                foreach (var entry in payload)
                {
                    entry.Value = entry.Value.ToUpper();
                }

                var response = req.CreateResponse(HttpStatusCode.OK);
                await response.WriteAsJsonAsync(payload);
                return response;
            } 
            catch(Exception e)
            {
                var response = req.CreateResponse(HttpStatusCode.BadRequest);
                _logger.LogError("Function1.", e);
                return response;
            }
        }
    }
}

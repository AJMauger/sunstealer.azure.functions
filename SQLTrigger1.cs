using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Extensions.Sql;
using Microsoft.Extensions.Logging;

namespace sunstealer.azure.functions
{
    public /* ajm: static*/ class SQLTrigger1
    {
        private readonly ILogger _logger;

        public SQLTrigger1(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<SQLTrigger1>();
        }

        [Function("SQLTrigger1")]
        public /* ajm: static*/ void Run(
            [SqlTrigger("[dbo].[Table1]", "ConnectionStringSetting")]
            IReadOnlyList<SqlChange<Table1>> changes, 
            FunctionContext context)
        {
            foreach (SqlChange<Table1> change in changes)
            {
                Table1 table1 = change.Item;
                _logger.LogInformation($"Change operation: {change.Operation}");
                _logger.LogInformation($"UUID: {table1.UUID}");
            }
        }
    }
}

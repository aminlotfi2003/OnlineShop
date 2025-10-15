using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace OnlineShop.Web.Abstractions.Extensions;

public static class HealthChecksExtensions
{
    public static IHealthChecksBuilder AddBasicLiveness(this IServiceCollection services)
        => services.AddHealthChecks().AddCheck("self", () => HealthCheckResult.Healthy(), tags: new[] { "live" });

    // builder.AddNpgSql(connectionString) or AddSqlServer(...)
    // builder.AddRabbitMQ(rabbitConnection)
    // builder.AddRedis(redisConn)
    // builder.AddElasticsearch(new Uri("http://localhost:9200"))
}

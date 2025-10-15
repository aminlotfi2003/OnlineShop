using Microsoft.AspNetCore.Http;

namespace OnlineShop.Observability.Correlation;

public sealed class CorrelationMiddleware(RequestDelegate next, ICorrelationContextAccessor accessor)
{
    private const string HeaderName = "X-Correlation-Id";
    public async Task Invoke(HttpContext ctx)
    {
        var id = ctx.Request.Headers.TryGetValue(HeaderName, out var v) && !string.IsNullOrWhiteSpace(v)
            ? v.ToString()
            : Guid.NewGuid().ToString("N");
        accessor.CorrelationId = id;
        ctx.Response.Headers[HeaderName] = id;
        await next(ctx);
    }
}

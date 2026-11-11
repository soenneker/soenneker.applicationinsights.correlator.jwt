using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Soenneker.ApplicationInsights.Correlator.Jwt.Abstract;
using System;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;

namespace Soenneker.ApplicationInsights.Correlator.Jwt;

public sealed class JwtTelemetryCorrelator : IStartupFilter, IJwtTelemetryCorrelator
{
    public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
    {
        ArgumentNullException.ThrowIfNull(next);
        return app =>
        {
            app.Use(async (context, continuation) =>
            {
                Enrich(context);
                await continuation(context);
            });
            next(app);
        };
    }

    public void Enrich(HttpContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        var activity = context.Features.Get<IHttpActivityFeature>()?.Activity;
        var request = context.Request;
        if (activity is null || !activity.IsAllDataRequested)
            return;

        if (!request.Headers.TryGetValue(HeaderNames.Authorization, out StringValues authValues))
            return;

        string? auth = authValues.Count > 0 ? authValues[0] : null;
        if (string.IsNullOrWhiteSpace(auth))
            return;

        int spaceIndex = auth.IndexOf(' ');
        if (spaceIndex < 0 || spaceIndex + 1 >= auth.Length)
            return;

        string jwt = auth[(spaceIndex + 1)..];
        if (jwt.Length == 0)
            return;

        // ⚠️ Consider hashing / claim extraction instead of raw JWT
        activity.SetTag("app.jwt", jwt);
    }
}

using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using OpenTelemetry.Instrumentation.AspNetCore;

namespace Soenneker.ApplicationInsights.Correlator.Jwt;

/// <summary>
/// Represents the jwt telemetry correlator.
/// </summary>
public sealed class JwtTelemetryCorrelator : IConfigureOptions<AspNetCoreTraceInstrumentationOptions>
{
    /// <summary>
    /// Configures the specified options.
    /// </summary>
    /// <param name="options">The options.</param>
    public void Configure(AspNetCoreTraceInstrumentationOptions options)
    {
        options.EnrichWithHttpRequest = static (activity, request) =>
        {
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
        };
    }
}
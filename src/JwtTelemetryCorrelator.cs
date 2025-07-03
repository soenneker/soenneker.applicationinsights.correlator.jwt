using System.Linq;
using Soenneker.ApplicationInsights.Correlator.Jwt.Abstract;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using System;

namespace Soenneker.ApplicationInsights.Correlator.Jwt;

/// <inheritdoc cref="IJwtTelemetryCorrelator"/>
public sealed class JwtTelemetryCorrelator : ITelemetryInitializer
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public JwtTelemetryCorrelator(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void Initialize(ITelemetry telemetry)
    {
        if (telemetry is not RequestTelemetry requestTelemetry)
            return;

        HttpContext? context = _httpContextAccessor.HttpContext;

        if (context == null || !context.Request.Headers.TryGetValue(HeaderNames.Authorization, out StringValues authorizationHeaderValues))
            return;

        string? authorizationHeader = authorizationHeaderValues.FirstOrDefault();

        if (authorizationHeader == null) 
            return;

        ReadOnlySpan<char> authorizationSpan = authorizationHeader.AsSpan();

        int spaceIndex = authorizationSpan.IndexOf(' ');

        if (spaceIndex != -1 && authorizationSpan.Length > spaceIndex + 1)
        {
            ReadOnlySpan<char> jwtSpan = authorizationSpan.Slice(spaceIndex + 1);

            if (!jwtSpan.IsEmpty)
            {
                requestTelemetry.Properties["Jwt"] = jwtSpan.ToString();
            }
        }
    }
}
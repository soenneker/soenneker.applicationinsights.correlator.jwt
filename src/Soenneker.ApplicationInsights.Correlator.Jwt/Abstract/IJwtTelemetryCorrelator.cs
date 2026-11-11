using Microsoft.AspNetCore.Http;

namespace Soenneker.ApplicationInsights.Correlator.Jwt.Abstract;

/// <summary>
/// A telemetry initializer that adds a request's authorization header
/// </summary>
public interface IJwtTelemetryCorrelator
{
    /// <summary>Adds the authorization token to the native request activity when that activity requests enrichment.</summary>
    void Enrich(HttpContext context);
}

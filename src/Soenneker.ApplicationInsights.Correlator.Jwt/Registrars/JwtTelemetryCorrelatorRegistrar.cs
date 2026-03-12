using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OpenTelemetry.Instrumentation.AspNetCore;
using Soenneker.ApplicationInsights.Correlator.Jwt.Abstract;

namespace Soenneker.ApplicationInsights.Correlator.Jwt.Registrars;

/// <summary>
/// A telemetry initializer that adds a request's authorization header
/// </summary>
public static class JwtTelemetryCorrelatorRegistrar
{
    /// <summary>
    /// Adds <see cref="IJwtTelemetryCorrelator"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddJwtTelemetryCorrelatorAsSingleton(this IServiceCollection services)
    {
        services.AddSingleton<IConfigureOptions<AspNetCoreTraceInstrumentationOptions>, JwtTelemetryCorrelator>();
        return services;
    }
}
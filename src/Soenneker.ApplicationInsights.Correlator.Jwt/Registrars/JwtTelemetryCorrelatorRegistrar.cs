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
    /// <param name="services">Service collection that receives the registration.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
    public static IServiceCollection AddJwtTelemetryCorrelatorAsSingleton(this IServiceCollection services)
    {
        services.AddSingleton<IConfigureOptions<AspNetCoreTraceInstrumentationOptions>, JwtTelemetryCorrelator>();
        return services;
    }
}

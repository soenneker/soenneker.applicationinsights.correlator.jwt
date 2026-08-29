[![](https://img.shields.io/nuget/v/soenneker.applicationinsights.correlator.jwt.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.applicationinsights.correlator.jwt/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.applicationinsights.correlator.jwt/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.applicationinsights.correlator.jwt/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.applicationinsights.correlator.jwt.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.applicationinsights.correlator.jwt/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.applicationinsights.correlator.jwt/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.applicationinsights.correlator.jwt/actions/workflows/codeql.yml)

# Soenneker.ApplicationInsights.Correlator.Jwt

An OpenTelemetry ASP.NET Core instrumentation option configurator that copies the credential portion of an incoming `Authorization` header into the current request activity as `app.jwt`.

## Security warning

This package records the raw credential value in telemetry. For a bearer request, that value is the complete JWT. Anyone with access to the exported trace data may be able to reuse an unexpired token or read claims contained in it.

Do not enable this package unless your security and privacy controls explicitly permit raw authorization credentials in telemetry. Ensure exporter destinations, retention, access control, diagnostic logging, and downstream processing are all appropriate for secrets. In most applications, a non-secret correlation identifier or a one-way hash of a stable claim is safer.

The implementation does not validate the authentication scheme or token format. It takes all text after the first space in any `Authorization` header, so `Basic`, custom schemes, and malformed bearer values can also be recorded.

## Installation

```bash
dotnet add package Soenneker.ApplicationInsights.Correlator.Jwt
```

## Registration

Register Azure Monitor/OpenTelemetry and then add the configurator:

```csharp
using Azure.Monitor.OpenTelemetry.AspNetCore;
using Soenneker.ApplicationInsights.Correlator.Jwt.Registrars;

builder.Services
       .AddOpenTelemetry()
       .UseAzureMonitor();

builder.Services.AddJwtTelemetryCorrelatorAsSingleton();
```

No `IJwtTelemetryCorrelator` service is resolved at runtime. The registrar adds `JwtTelemetryCorrelator` as an `IConfigureOptions<AspNetCoreTraceInstrumentationOptions>` implementation, which OpenTelemetry applies when ASP.NET Core tracing options are created.

## Resulting telemetry

Given this request header:

```text
Authorization: Bearer eyJhbGciOi...
```

the ASP.NET Core server activity receives this tag:

```text
app.jwt = eyJhbGciOi...
```

Requests without an authorization header, without a space separating scheme and credential, or with an empty credential are left unchanged. The tag is also skipped when the activity is null or is not requesting all data.

## Composition caveat

The configurator assigns `AspNetCoreTraceInstrumentationOptions.EnrichWithHttpRequest`; it does not compose with an existing callback. Another options configurator that assigns the same property can replace this behavior, or this configurator can replace that behavior, depending on registration and options-configuration order.

If the application needs multiple enrichers, register one application-owned configurator that invokes each enrichment operation explicitly.

## API

`AddJwtTelemetryCorrelatorAsSingleton(IServiceCollection)` registers the options configurator and returns the same service collection for chaining.

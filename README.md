[![](https://img.shields.io/nuget/v/soenneker.applicationinsights.correlator.jwt.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.applicationinsights.correlator.jwt/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.applicationinsights.correlator.jwt/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.applicationinsights.correlator.jwt/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.applicationinsights.correlator.jwt.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.applicationinsights.correlator.jwt/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.applicationinsights.correlator.jwt/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.applicationinsights.correlator.jwt/actions/workflows/codeql.yml)

# Soenneker.ApplicationInsights.Correlator.Jwt

A telemetry initializer that adds a request's authorization header.

## Install

```bash
dotnet add package Soenneker.ApplicationInsights.Correlator.Jwt
```

## Quick start

```csharp
using Soenneker.ApplicationInsights.Correlator.Jwt.Registrars;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
var result = services.AddJwtTelemetryCorrelatorAsSingleton();
```

Adds `IJwtTelemetryCorrelator` as a singleton service.

## What you get

- `IJwtTelemetryCorrelator` — A telemetry initializer that adds a request's authorization header.
- `JwtTelemetryCorrelatorRegistrar` — A telemetry initializer that adds a request's authorization header.
- `JwtTelemetryCorrelator` — Represents the jwt telemetry correlator.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `JwtTelemetryCorrelatorRegistrar.AddJwtTelemetryCorrelatorAsSingleton(services)` | Adds `IJwtTelemetryCorrelator` as a singleton service. | The same service collection, so additional registrations can be chained. |
| `JwtTelemetryCorrelator.Configure(options)` | Applies jwt telemetry correlator-specific settings to the supplied options. | Returns no value; the requested change is complete when the method returns. |

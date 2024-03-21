[![](https://img.shields.io/nuget/v/soenneker.applicationinsights.correlator.jwt.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.applicationinsights.correlator.jwt/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.applicationinsights.correlator.jwt/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.applicationinsights.correlator.jwt/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.applicationinsights.correlator.jwt.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.applicationinsights.correlator.jwt/)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.ApplicationInsights.Correlator.Jwt
### A telemetry initializer that adds a request's authorization header

## Installation

```
dotnet add package Soenneker.ApplicationInsights.Correlator.Jwt
```

## Usage

### 1. Add the following to your `Startup.cs` file

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddJwtTelemetryCorrelatorAsSingleton();
}
```

That's it!

If requests are made with the request header`Authorization` , and have the scheme `bearer <token>`, `Jwt` will be visible on the individual requests in Application Insights.
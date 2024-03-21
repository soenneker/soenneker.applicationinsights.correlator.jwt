using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Soenneker.ApplicationInsights.Correlator.Jwt.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;
using Xunit.Abstractions;

namespace Soenneker.ApplicationInsights.Correlator.Jwt.Tests;

[Collection("Collection")]
public class JwtTelemetryCorrelatorTests : FixturedUnitTest
{
    private readonly IJwtTelemetryCorrelator _util;

    public JwtTelemetryCorrelatorTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _util = Resolve<IJwtTelemetryCorrelator>(true);
    }
}

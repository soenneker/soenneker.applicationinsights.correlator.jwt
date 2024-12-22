using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.ApplicationInsights.Correlator.Jwt.Tests;

[Collection("Collection")]
public class JwtTelemetryCorrelatorTests : FixturedUnitTest
{
    public JwtTelemetryCorrelatorTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
    }

    [Fact]
    public void Default()
    {

    }
}

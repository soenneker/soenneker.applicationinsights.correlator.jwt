using Soenneker.Tests.HostedUnit;

namespace Soenneker.ApplicationInsights.Correlator.Jwt.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public class JwtTelemetryCorrelatorTests : HostedUnitTest
{
    public JwtTelemetryCorrelatorTests(Host host) : base(host)
    {
    }

    [Test]
    public void Default()
    {

    }
}

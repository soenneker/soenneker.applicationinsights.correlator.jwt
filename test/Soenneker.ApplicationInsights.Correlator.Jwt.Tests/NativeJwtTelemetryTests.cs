using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

namespace Soenneker.ApplicationInsights.Correlator.Jwt.Tests;

public sealed class NativeJwtTelemetryTests
{
    [Test]
    [Arguments(true)]
    [Arguments(false)]
    public async ValueTask Enriches_only_the_request_activity_when_requested(bool requested)
    {
        using var requestActivity = new Activity("request") { IsAllDataRequested = requested };
        using var child = new Activity("child").Start();
        var context = new DefaultHttpContext();
        context.Features.Set<IHttpActivityFeature>(new TestActivityFeature(requestActivity));
        context.Request.Headers.Authorization = "Bearer test-token";
        new JwtTelemetryCorrelator().Enrich(context);
        await Assert.That(requestActivity.GetTagItem("app.jwt")).IsEqualTo(requested ? "test-token" : null);
        await Assert.That(child.GetTagItem("app.jwt")).IsNull();
    }
}

using System.Diagnostics;
using Microsoft.AspNetCore.Http.Features;

namespace Soenneker.ApplicationInsights.Correlator.Jwt.Tests;

internal sealed class TestActivityFeature(Activity activity) : IHttpActivityFeature
{
    public Activity Activity { get; set; } = activity;
}

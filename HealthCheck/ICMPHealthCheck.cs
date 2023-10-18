using System.Net.NetworkInformation;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HealthCheck;

public class ICMPHealthCheck : IHealthCheck
{
    private readonly string Host = "www.does-not-exist.coms";

    private readonly int HealthyRoundtripTime = 300;
    
    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            using var ping = new Ping();
            var reply = await ping.SendPingAsync(Host);

            switch (reply.Status)
            {
                case IPStatus.Success:
                    return (reply.RoundtripTime > HealthyRoundtripTime)
                        ? HealthCheckResult.Degraded()
                        : HealthCheckResult.Healthy();
                default:
                    return HealthCheckResult.Unhealthy();
            }

        }
        catch (Exception e)
        {
            return HealthCheckResult.Unhealthy();
        }
    }
}
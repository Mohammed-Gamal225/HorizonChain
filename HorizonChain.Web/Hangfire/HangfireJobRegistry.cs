using Hangfire;
using Horizon.Application.Abstractions.Services;

namespace HorizonChain.Web.Hangfire;

public static class HangfireJobRegistry
{
    public static IServiceCollection AddHangfireWithJobs(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHangfire(config =>
        {
            config.UseSqlServerStorage(configuration.GetConnectionString("HangfireConnection"));
        });

        services.AddHangfireServer();

        return services;
    }
    public static void RegisterRecurringJobs()
    {
        RecurringJob.AddOrUpdate<IJobOrderSyncService>(
            "sync-job-orders",
            service => service.SyncJobOrdersAsync("2025-05-19", CancellationToken.None),
            Cron.Hourly
        );


    }
}

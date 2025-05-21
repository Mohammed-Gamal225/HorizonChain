using Horizon.Application.Configuration.Mapping;
using Horizon.Application.Services;

namespace Horizon.Application.DependencyInjection;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<ErpAuthOptions>(configuration.GetSection(ErpAuthOptions.SectionName));
        services.AddHttpClient();
        services.AddScoped<IERPAuthService, ERPAuthService>();
        services.AddScoped<IErpApiClient, ErpApiClient>();
        services.AddScoped<IJobOrderSyncService, JobOrderSyncService>();
        services.AddScoped<IJobOrderService, JobOrderService>();

        services.AddMapsterConfig();
        return services;
    }
}

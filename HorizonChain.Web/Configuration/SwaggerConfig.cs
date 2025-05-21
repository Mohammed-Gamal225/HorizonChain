using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace HorizonChain.Web.Configuration;


public static class SwaggerConfig
{
    public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "HorizonChain API",
                Version = "v1",
                Description = "API documentation for HorizonChain system",
                Contact = new OpenApiContact
                {
                    Name = "UE Technology",
                    Email = "support@ue-tech.com",
                    Url = new Uri("https://ue-tech.com")
                }
            });
        });

        return services;
    }

    public static void UseCustomSwaggerUI(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.DocumentTitle = "HorizonChain API Docs";
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "HorizonChain v1");
            options.DisplayRequestDuration();
            options.EnableFilter();
            options.DocExpansion(DocExpansion.None);
            if (File.Exists("wwwroot/swagger-ui/custom.css"))
            {
                options.InjectStylesheet("/swagger-ui/custom.css");
            }
        });
    }
}
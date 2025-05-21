using Hangfire;
using Horizon.Application.Abstractions.Services;
using Horizon.Application.DependencyInjection;
using Horizon.Infrastructure.Persistence.DependencyInjection;
using HorizonChain.Web.Common.Middlewares;
using HorizonChain.Web.Configuration;
using HorizonChain.Web.Hangfire;
using HorizonChain.Web.Services;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddApplicationPart(typeof(Horizon.Presentation.AssemblyReference).Assembly);

builder.Services.AddCustomSwagger();

builder.Services.AddApplicationServices(builder.Configuration)
    .AddPersistence(builder.Configuration);
builder.Services.AddHangfireWithJobs(builder.Configuration);
builder.Services.AddSingleton<ISlaughterSessionStore, SlaughterSessionStore>();

var app = builder.Build();
app.UseCustomExceptionHandlingMiddleware();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
    app.UseCustomSwaggerUI();

app.UseHangfireDashboard();

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

HangfireJobRegistry.RegisterRecurringJobs();
app.Run();

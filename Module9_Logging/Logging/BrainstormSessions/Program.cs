using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BrainstormSessions.Core.Interfaces;
using BrainstormSessions.Core.Model;
using BrainstormSessions.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, loggerConfiguration) =>
{
    loggerConfiguration
        .ReadFrom.Configuration(context.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.Console();

    var emailSection = context.Configuration.GetSection("Serilog:Email");
    if (bool.TryParse(emailSection["Enabled"], out bool enabled) && enabled)
    {
        int port = int.TryParse(emailSection["Port"], out int parsedPort) ? parsedPort : 25;

        loggerConfiguration.WriteTo.Email(
            from: emailSection["From"]!,
            to: emailSection["To"]!,
            host: emailSection["Host"]!,
            port: port,
            subject: emailSection["Subject"] ?? "BrainstormSessions error log",
            body: emailSection["Body"] ?? "{Timestamp} [{Level}] {Message}{NewLine}{Exception}",
            restrictedToMinimumLevel: LogEventLevel.Error);
    }
});

builder.Services.AddDbContext<AppDbContext>(
    optionsBuilder => optionsBuilder.UseInMemoryDatabase("InMemoryDb"));

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IBrainstormSessionRepository, EFStormSessionRepository>();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using IServiceScope scope = app.Services.CreateScope();
    IBrainstormSessionRepository repository = scope.ServiceProvider.GetRequiredService<IBrainstormSessionRepository>();

    await SeedDatabaseAsync(repository);
}

app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

Log.CloseAndFlush();

static async Task SeedDatabaseAsync(IBrainstormSessionRepository repo)
{
    List<BrainstormSession> sessionList = await repo.ListAsync();
    if (sessionList.Count == 0)
    {
        await repo.AddAsync(GetTestSession());
    }
}

static BrainstormSession GetTestSession()
{
    BrainstormSession session = new()
    {
        Name = "Test Session 1",
        DateCreated = new DateTime(2016, 8, 1)
    };

    Idea idea = new()
    {
        DateCreated = new DateTime(2016, 8, 1),
        Description = "Totally awesome idea",
        Name = "Awesome idea"
    };

    session.AddIdea(idea);
    return session;
}

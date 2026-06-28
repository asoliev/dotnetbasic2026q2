using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BrainstormSessions.Core.Interfaces;
using BrainstormSessions.Core.Model;
using BrainstormSessions.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(
    optionsBuilder => optionsBuilder.UseInMemoryDatabase("InMemoryDb"));

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IBrainstormSessionRepository, EFStormSessionRepository>();

var app = builder.Build();

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
    var session = new BrainstormSession()
    {
        Name = "Test Session 1",
        DateCreated = new DateTime(2016, 8, 1)
    };

    var idea = new Idea()
    {
        DateCreated = new DateTime(2016, 8, 1),
        Description = "Totally awesome idea",
        Name = "Awesome idea"
    };

    session.AddIdea(idea);
    return session;
}

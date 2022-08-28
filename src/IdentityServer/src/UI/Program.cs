using DuongTruong.IdentityServer.UI.Configurations;
using DuongTruong.IdentityServer.UI.Utils;

var stopwatch = System.Diagnostics.Stopwatch.StartNew();

var builder = WebApplication.CreateBuilder(args);

var warmUpBehavior = builder.Configuration.GetValue("WarmUp", WarmUpBehavior.Skip);

// Add services to the container.
builder.Services.AddSingleton(stopwatch);
builder.Services.AddRazorPages();
builder.Services.AddDefaultDependencies(builder.Configuration);

var app = builder.Build();

if (warmUpBehavior == WarmUpBehavior.Skip)
{
    app.Logger.LogInformation("Skip warm up application.");
}
else
{
    app.Logger.LogInformation("Finished seeding data while warming up.");
    await app.WarmUpAsync();
    app.Logger.LogInformation("Warming up consumes {time}s.", stopwatch.ElapsedMilliseconds / 1000.0);
    if (warmUpBehavior == WarmUpBehavior.Exit)
        return;
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();
app.UseAuthorization();

app.MapRazorPages();

app.Run();

app.Logger.LogInformation("App running for {elapsed time} s.", stopwatch.Elapsed);

namespace DuongTruong.IdentityServer.UI
{
    public partial class Program
    {
        public readonly static SemaphoreSlim SemaphoreSlim = new(1);
    }
}
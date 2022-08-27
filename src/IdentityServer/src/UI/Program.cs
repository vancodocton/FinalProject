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

await app.WarmUpAsync(warmUpBehavior);
if (warmUpBehavior == WarmUpBehavior.Exit)
    return;

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

app.Logger.LogInformation($"App running for {stopwatch.Elapsed} s.");

namespace DuongTruong.IdentityServer.UI
{
    public partial class Program
    {
        public readonly static SemaphoreSlim SemaphoreSlim = new(1);
    }
}
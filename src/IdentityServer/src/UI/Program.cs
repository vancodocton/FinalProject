using DuongTruong.Extensions.DependencyInjection;
using DuongTruong.IdentityServer.Infrastructure.Identity;
using DuongTruong.IdentityServer.UI.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDefaultDependencies(builder.Configuration);

var app = builder.Build();

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

await app.Services.CreateScope().ServiceProvider.InitializeDataAsync();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();
app.UseAuthorization();

app.MapRazorPages();

app.Run();

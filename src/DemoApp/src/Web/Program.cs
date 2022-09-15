using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
    .AddCookie()
    .AddOpenIdConnect(options =>
    {
        var config = builder.Configuration.GetRequiredSection(OpenIdConnectDefaults.AuthenticationScheme);
        // Token issuer - IdentityServer
        options.Authority = config.GetValue<string>("Issuer");
        options.ClientId = config.GetValue<string>("ClientId");
        options.ClientSecret = config.GetValue<string>("ClientSecret");
        options.ResponseType = OpenIdConnectResponseType.Code;

        options.SaveTokens = true;

        // implict add openid profile scope // skip adding
        options.Scope.Add("demoapi");
        options.Scope.Add(OpenIdConnectScope.OfflineAccess);
    });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages()
    .RequireAuthorization();

app.Run();

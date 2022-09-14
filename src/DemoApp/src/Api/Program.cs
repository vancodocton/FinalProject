using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        var config = builder.Configuration.GetRequiredSection(JwtBearerDefaults.AuthenticationScheme);
        // Token issuer - IdentityServer
        options.Authority = config.GetValue<string>("Issuer")
            ?? throw new ArgumentNullException(nameof(JwtBearerOptions.Authority), "JwtBearer Authority cannot be null.");
        // Enable Validate Audiences
        options.TokenValidationParameters.ValidAudiences = config.GetSection("Audiences").Get<string[]>();

        // it's recommended to check the type header to avoid "JWT confusion" attacks
        options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1.0.0-Preview-1", new()
    {
        Title = "DuongTruong DemoApp Api v0.1.0-Preview-1",
        Description = "DuongTruong DemoApp Api, version 1.0.0-Preview-1",
        Version = "1.0.0-Preview-1",
    });

    var oidcScheme = new OpenApiSecurityScheme()
    {
        Type = SecuritySchemeType.OpenIdConnect,
        Description = "Duende Identity Server v6",
        OpenIdConnectUrl = new("https://localhost:7011/.well-known/openid-configuration"),
    };

    options.AddSecurityDefinition("oidc", oidcScheme);

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new ()
            {
                Reference = new()
                {
                    Id = "oidc",
                    Type = ReferenceType.SecurityScheme,
                },
            },
            new string[]
            {
                "openid",
                "urn:demoapi.read",
                "urn:demoapi.write",
            }
        },
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // OAS v3
    app.UseSwagger(options =>
    {
        options.RouteTemplate = "swagger/{documentName}/openapi.json";
        options.SerializeAsV2 = false;
    });
    app.UseSwaggerUI(options =>
    {
        // config OAS v3 and Swagger v2 endpoints
        options.SwaggerEndpoint("v1.0.0-Preview-1/openapi.json", "DuongTruong.DemoApp.Api.v1.0.0-Preview-1.OAS-v3");

        options.OAuthUsePkce();
        options.OAuthClientId("demoweb");
        options.OAuthClientSecret("secret");
        options.OAuthScopes(new string[]
        {
            "openid",
            "urn:demoapi.read",
            "urn:demoapi.write",
        });
        options.ShowExtensions();
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "Demo API woked.")
    .ExcludeFromDescription()
    .AllowAnonymous();
app.MapControllers()
    .RequireAuthorization();

app.Run();

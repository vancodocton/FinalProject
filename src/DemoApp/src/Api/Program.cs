using Microsoft.AspNetCore.Authentication.JwtBearer;

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
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
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

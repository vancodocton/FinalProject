using FinalProject.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

string dbProvider = builder.Configuration.GetValue("DatabaseProvider", "Sqlite")
    ?? throw new ArgumentNullException(nameof(dbProvider), "Database provider is not configured.");

builder.Services.AddDbContext<AppIdentityDbContext>(options =>
{
    _ = dbProvider switch
    {
        "Sqlite" => options.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection"),
            x => x.MigrationsAssembly("Infrastructure.Sqlite")),
        "Postgre" => options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreConnection"),
            x => x.MigrationsAssembly("Infrastructure.PostgreSQL")),
        _ => throw new Exception($"Unsupported database provider: {dbProvider}")
    };
});

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.Stores.MaxLengthForKeys = 128;
})
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<AppIdentityDbContext>()
    .AddDefaultUI();

builder.Services.AddIdentityServer()
    .AddConfigurationStore(options =>
    {
        options.ConfigureDbContext = b => _ = dbProvider switch
        {
            "Sqlite" => b.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection"),
                x => x.MigrationsAssembly("Infrastructure.Sqlite")),
            "Postgre" => b.UseNpgsql(builder.Configuration.GetConnectionString("PostgreConnection"),
                x => x.MigrationsAssembly("Infrastructure.PostgreSQL")),
            _ => throw new Exception($"Unsupported database provider: {dbProvider}")
        };
    })
    .AddOperationalStore(options =>
    {
        options.ConfigureDbContext = b => _ = dbProvider switch
        {
            "Sqlite" => b.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection"),
                x => x.MigrationsAssembly("Infrastructure.Sqlite")),
            "Postgre" => b.UseNpgsql(builder.Configuration.GetConnectionString("PostgreConnection"),
                x => x.MigrationsAssembly("Infrastructure.PostgreSQL")),
            _ => throw new Exception($"Unsupported database provider: {dbProvider}")
        };
    })
    .AddAspNetIdentity<ApplicationUser>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();
app.UseAuthorization();

app.MapRazorPages()
    .RequireAuthorization();

app.Run();

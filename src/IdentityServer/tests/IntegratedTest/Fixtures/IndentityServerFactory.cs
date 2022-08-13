﻿using Duende.IdentityServer.EntityFramework.DbContexts;
using DuongTruong.IdentityServer.Infrastructure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DuongTruong.IdentityServer.IntegratedTest.Fixtures
{
    public class IndentityServerFactory : WebApplicationFactory<UI.Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                {
                    var options = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                    if (options != null)
                        services.Remove(options);

                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("AspNetIdentity.InMemory");
                    });
                }

                {
                    var options = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<ConfigurationDbContext>));

                    if (options != null)
                    {
                        services.Remove(options);
                        services.AddDbContext<ConfigurationDbContext>(options =>
                        {
                            options.UseInMemoryDatabase("ConfigurationDb.InMemory");
                        });
                    }
                }

                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    {
                        var db = scopedServices.GetRequiredService<ApplicationDbContext>();
                        db.Database.EnsureCreated();
                    }
                    {
                        var db = scopedServices.GetService<ConfigurationDbContext>();
                        if (db != null)
                            db.Database.EnsureCreated();
                    }
                }

            });

            base.ConfigureWebHost(builder);
        }
    }
}

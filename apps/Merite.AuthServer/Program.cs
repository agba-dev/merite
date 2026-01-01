using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Merite.Administration.EntityFrameworkCore;
using Merite.SaaS.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;

namespace Merite;

public class Program
{
    public static async Task<int> Main(string[] args)
    {
        MeriteLogging.Initialize();

        try
        {
            Log.Information("Starting Merite.AuthServer.");

            var builder = WebApplication.CreateBuilder(args);
            builder.AddServiceDefaults();
            builder.AddSharedEndpoints();

            builder.AddNpgsqlDbContext<AdministrationDbContext>(
                connectionName: MeriteNames.AdministrationDb,
                configure => configure.DisableRetry = true
            );
            builder.AddNpgsqlDbContext<IdentityDbContext>(
                connectionName: MeriteNames.IdentityServiceDb,
                configure => configure.DisableRetry = true
            );
            builder.AddNpgsqlDbContext<SaaSDbContext>(
                connectionName: MeriteNames.SaaSDb,
                configure => configure.DisableRetry = true
            );

            builder.Host.AddAppSettingsSecretsJson().UseAutofac().UseSerilog();

            await builder.AddApplicationAsync<MeriteAuthServerModule>();

            var app = builder.Build();

            await app.InitializeApplicationAsync();

            await app.RunAsync();

            return 0;
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Merite.AuthServer terminated unexpectedly!");
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}

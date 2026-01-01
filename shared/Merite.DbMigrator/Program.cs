using Serilog;
using Merite.Administration.EntityFrameworkCore;
using Merite.Projects.EntityFrameworkCore;
using Merite.SaaS.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;

namespace Merite.DbMigrator;

internal class Program
{
    private static async Task Main(string[] args)
    {
        MeriteLogging.Initialize();

        var builder = Host.CreateApplicationBuilder(args);

        builder.AddServiceDefaults();

        builder.AddNpgsqlDbContext<AdministrationDbContext>(
            connectionName: MeriteNames.AdministrationDb
        );
        builder.AddNpgsqlDbContext<IdentityDbContext>(connectionName: MeriteNames.IdentityServiceDb);
        builder.AddNpgsqlDbContext<SaaSDbContext>(connectionName: MeriteNames.SaaSDb);
        builder.AddNpgsqlDbContext<ProjectsDbContext>(connectionName: MeriteNames.ProjectsDb);

        builder.Configuration.AddAppSettingsSecretsJson();

        builder.Logging.AddSerilog();

        builder.Services.AddHostedService<DbMigratorHostedService>();

        var host = builder.Build();

        await host.RunAsync();
    }
}

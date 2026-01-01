using Microsoft.Extensions.Hosting;
using Projects;

namespace Merite.AppHost;

internal class Program
{
    private static void Main(string[] args)
    {
        const string LaunchProfileName = "Aspire";
        var builder = DistributedApplication.CreateBuilder(args);

        var postgres = builder.AddPostgres(MeriteNames.Postgres).WithPgWeb();
        var rabbitMq = builder.AddRabbitMQ(MeriteNames.RabbitMq).WithManagementPlugin();
        var redis = builder.AddRedis(MeriteNames.Redis).WithRedisCommander();
        var seq = builder.AddSeq(MeriteNames.Seq);

        var adminDb = postgres.AddDatabase(MeriteNames.AdministrationDb);
        var identityDb = postgres.AddDatabase(MeriteNames.IdentityServiceDb);
        var projectsDb = postgres.AddDatabase(MeriteNames.ProjectsDb);
        var saasDb = postgres.AddDatabase(MeriteNames.SaaSDb);

        var migrator = builder
            .AddProject<Merite_DbMigrator>(
                MeriteNames.DbMigrator,
                launchProfileName: LaunchProfileName
            )
            .WithReference(adminDb)
            .WithReference(identityDb)
            .WithReference(projectsDb)
            .WithReference(saasDb)
            .WithReference(seq)
            .WaitFor(postgres);

        var admin = builder
            .AddProject<Merite_Administration_HttpApi_Host>(
                MeriteNames.AdministrationApi,
                launchProfileName: LaunchProfileName
            )
            .WithExternalHttpEndpoints()
            .WithReference(adminDb)
            .WithReference(identityDb)
            .WithReference(rabbitMq)
            .WithReference(redis)
            .WithReference(seq)
            .WaitForCompletion(migrator);

        var identity = builder
            .AddProject<Merite_IdentityService_HttpApi_Host>(
                MeriteNames.IdentityServiceApi,
                launchProfileName: LaunchProfileName
            )
            .WithExternalHttpEndpoints()
            .WithReference(adminDb)
            .WithReference(identityDb)
            .WithReference(saasDb)
            .WithReference(rabbitMq)
            .WithReference(redis)
            .WithReference(seq)
            .WaitForCompletion(migrator);

        var saas = builder
            .AddProject<Merite_SaaS_HttpApi_Host>(
                MeriteNames.SaaSApi,
                launchProfileName: LaunchProfileName
            )
            .WithExternalHttpEndpoints()
            .WithReference(adminDb)
            .WithReference(saasDb)
            .WithReference(rabbitMq)
            .WithReference(redis)
            .WithReference(seq)
            .WaitForCompletion(migrator);

        builder
            .AddProject<Merite_Projects_HttpApi_Host>(
                MeriteNames.ProjectsApi,
                launchProfileName: LaunchProfileName
            )
            .WithExternalHttpEndpoints()
            .WithReference(adminDb)
            .WithReference(projectsDb)
            .WithReference(rabbitMq)
            .WithReference(redis)
            .WithReference(seq)
            .WaitForCompletion(migrator);

        var gateway = builder
            .AddProject<Merite_Gateway>(MeriteNames.Gateway, launchProfileName: LaunchProfileName)
            .WithExternalHttpEndpoints()
            .WithReference(seq)
            .WaitFor(admin)
            .WaitFor(identity)
            .WaitFor(saas);

        var authserver = builder
            .AddProject<Merite_AuthServer>(
                MeriteNames.AuthServer,
                launchProfileName: LaunchProfileName
            )
            .WithExternalHttpEndpoints()
            .WithReference(adminDb)
            .WithReference(identityDb)
            .WithReference(saasDb)
            .WithReference(rabbitMq)
            .WithReference(redis)
            .WithReference(seq)
            .WaitForCompletion(migrator);

        builder
            .AddProject<Merite_WebApp_Blazor>(
                MeriteNames.WebAppClient,
                launchProfileName: LaunchProfileName
            )
            .WithExternalHttpEndpoints()
            .WithReference(seq)
            .WaitFor(authserver)
            .WaitFor(gateway);

        builder.Build().Run();
    }
}

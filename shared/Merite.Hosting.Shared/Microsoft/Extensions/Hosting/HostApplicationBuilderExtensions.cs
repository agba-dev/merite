using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.Hosting;

public static class HostApplicationBuilderExtensions
{
    public static IHostApplicationBuilder AddSharedEndpoints(this IHostApplicationBuilder builder)
    {
        builder.AddRabbitMQClient(
            connectionName: MeriteNames.RabbitMq,
            action =>
                action.ConnectionString = builder.Configuration.GetConnectionString(
                    MeriteNames.RabbitMq
                )
        );
        builder.AddRedisDistributedCache(connectionName: MeriteNames.Redis);
        builder.AddSeqEndpoint(connectionName: MeriteNames.Seq);

        return builder;
    }
}

using Ingestor.Workers;

namespace Ingestor;

public class Program
{
    
    public async static Task Main(string[] args)
    {
        MongoDbClient.InitClient(connectionString: "mongodb://root:root@mongodb_container:27017"); // TODO: make configurable
        var builder = Host.CreateApplicationBuilder(args);
        builder.Services.AddHostedService<GemelRunner>();
        
        var host = builder.Build();
        host.Run();

    }
}
using MongoDB.Driver;

namespace Ingestor;

public class MongoClient
{
    private readonly IMongoClient _client;
    private readonly IMongoDatabase _database;
    
    public MongoClient(string connectionString, string databaseName, string username, string password)
    {
        var settings = MongoClientSettings.FromConnectionString(connectionString);
        settings.Credential = MongoCredential.CreateCredential(databaseName, username, password);

        _client = new MongoClient(settings);
        _database = _client.GetDatabase(databaseName);
    }
}
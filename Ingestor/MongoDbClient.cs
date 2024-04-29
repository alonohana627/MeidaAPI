using MongoDB.Bson;
using MongoDB.Driver;

namespace Ingestor;

// Singleton for DB
public class MongoDbClient
{
    private static MongoDbClient? _externalClient;
    private readonly IMongoClient _client;
    private readonly IMongoDatabase _database;

    public static MongoDbClient? GetClient()
    {
        return _externalClient;
    }

    public static void InitClient(string connectionString,
        string databaseName = "israel_datagov")
    {
        if (_externalClient != null)
        {
            return;
        }

        _externalClient = new MongoDbClient(connectionString, databaseName);
    }

    private MongoDbClient(string connectionString, string databaseName = "israel_datagov")
    {
        _client = new MongoClient(connectionString);
        _database = _client.GetDatabase(databaseName);
        InitializeDatabase();
    }

    private void InitializeDatabase()
    {
        if (!_client.ListDatabaseNames().ToList().Contains(_database.DatabaseNamespace.DatabaseName))
        {
            _client.GetDatabase(_database.DatabaseNamespace.DatabaseName);
        }

        if (!_database.ListCollectionNames().ToList().Contains("gemel"))
        {
            _database.CreateCollection("gemel");
        }
    }

    public IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        return _database.GetCollection<T>(collectionName);
    }

    public void InsertDocument<T>(string collectionName, T document)
    {
        var collection = GetCollection<T>(collectionName);
        var id = document?.GetType().GetProperty("_id")?.GetValue(document)?.ToString();
        collection.ReplaceOne(Builders<T>.Filter.Eq("_id", id), document,
            new ReplaceOptions { IsUpsert = true });
    }
}
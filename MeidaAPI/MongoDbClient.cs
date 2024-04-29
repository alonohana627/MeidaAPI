using MongoDB.Bson;
using MongoDB.Driver;

namespace MediaAPI;

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
    }

    public IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        return _database.GetCollection<T>(collectionName);
    }
    
    public async Task<List<T>> GetDocuments<T>(string collectionName, int limit = 0, int offset = 0)
    {
        var documents = _database.GetCollection<T>(collectionName);
        var docsAsTask = await documents.Find(new BsonDocument())
            .Skip(offset)
            .Limit(limit)
            .ToListAsync();

        return docsAsTask;
    }
}
using System.Text.Json;

namespace Ingestor.Workers;

using Ingestor.Models;

public class GemelRunner(ILogger<Worker> logger) : BackgroundService
{
    private const string DatastoreID = "a30dcbea-a1d2-482c-ae29-8f781f5025fb";
    private static readonly string GemelBaseURL = Constants.BASE_URL + "?resource_id=" + DatastoreID + "&limit=100";

    private static async Task<int?> GetTotal(CancellationToken stoppingToken)
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, GemelBaseURL);
        var response = await client.SendAsync(request, stoppingToken);
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }
        
        var jsonFromServer = await response.Content.ReadAsStringAsync(stoppingToken);
        var gemel = JsonSerializer.Deserialize<Gemel>(jsonFromServer);

        return gemel?.result.total;
    }
    
    private static async Task IngestionIteration(CancellationToken stoppingToken, int totalAmount)
    {
        var client = new HttpClient();
        for (int i = 0; i < totalAmount; i += 100)
        {
            var currentUrl = GemelBaseURL + $"&offset={i}";
            var request = new HttpRequestMessage(HttpMethod.Get, currentUrl);
            var response = await client.SendAsync(request, stoppingToken);
            if (!response.IsSuccessStatusCode)
            {
                await Task.Delay(10000, stoppingToken);
                continue;
            }
            
            var jsonFromServer = await response.Content.ReadAsStringAsync(stoppingToken);
            Gemel? gemel = JsonSerializer.Deserialize<Gemel>(jsonFromServer);
            if (gemel == null)
            {
                await Task.Delay(10000, stoppingToken);
                continue;
            }

            var dbClient = MongoDbClient.GetClient();
            if (dbClient == null)
            {
                return;
            }

            foreach (var record in gemel.result.records)
            {
                dbClient.InsertDocument("gemel", record);
            }
        }
    }
    
    // TODO: configurable delays
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("Gemel Full Ingestion Request at: {time}", DateTimeOffset.Now);
            int? totalAmount = await GetTotal(stoppingToken);
            if (totalAmount == null)
            {
                await Task.Delay(10000, stoppingToken);
                continue;
            }

            await IngestionIteration(stoppingToken, totalAmount ?? 0);
            
            await Task.Delay(3600000, stoppingToken);
        }
    }
}
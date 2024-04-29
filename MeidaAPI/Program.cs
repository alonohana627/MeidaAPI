using MediaAPI;
using MeidaAPI.Models;
using Microsoft.OpenApi.Models;

namespace MeidaAPI;

public class Program
{
    static readonly int MAX_PER_REQUEST = 500; // TODO: make configurable

    static (int, int) ParseCountOffset(String? count, String? offset)
    {
        int parsedCount, parsedOffset;

        if (!int.TryParse(count ?? "10", out parsedCount))
        {
            parsedCount = 10;
        }

        if (!int.TryParse(offset ?? "0", out parsedOffset))
        {
            parsedOffset = 10;
        }

        if (parsedCount <= 0 || parsedCount > MAX_PER_REQUEST)
        {
            parsedCount = MAX_PER_REQUEST;
        }

        if (parsedOffset < 0)
        {
            parsedOffset = 0;
        }

        return (parsedCount, parsedOffset);
    }

    static OpenApiOperation RegisterGemelQueries(OpenApiOperation o)
    {
        o.Parameters.Add(new OpenApiParameter
        {
            Name = "count",
            In = ParameterLocation.Query,
            Description = "Number of entries to retrieve (default is 10).",
            Required = false,
            Schema = new OpenApiSchema
            {
                Type = "integer",
                Minimum = 1,
                Maximum = MAX_PER_REQUEST
            }
        });
        o.Parameters.Add(new OpenApiParameter
        {
            Name = "offset",
            In = ParameterLocation.Query,
            Description = "Number of entries to skip (default is 0).",
            Required = false,
            Schema = new OpenApiSchema
            {
                Type = "integer",
                Minimum = 0
            }
        });

        return o;
    }

    public static void Main(string[] args)
    {
        MongoDbClient.InitClient(connectionString: "mongodb://root:root@mongodb_container:27017"); // TODO: make configurable
        var client = MongoDbClient.GetClient();
        if (client == null)
        {
            return;
        }

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddAuthorization();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapGet("/economics/gemel/info", async (HttpContext context) =>
            {
                var rawDescription = await client
                    .GetDocuments<RawGemelDescriptionEntry>("gemel_description", limit: 50);

                List<GemelDescriptionEntry> entries = rawDescription
                    .ConvertAll(GemelDescriptionEntry.FromRawGemelDescriptionEntry);

                return entries;
            })
            .WithName("GetGemelInformation")
            .WithOpenApi();

        app.MapGet("/economics/gemel", async (HttpContext context) =>
            {
                string? count = context.Request.Query["count"];
                string? offset = context.Request.Query["offset"];
                Gemel g = new Gemel();

                (int parsedCount, int parsedOffset) = ParseCountOffset(count, offset);

                var docs = await client.GetDocuments<GemelEntry>(
                    collectionName: "gemel",
                    offset: parsedOffset,
                    limit: parsedCount
                );

                g.count = docs.Count;
                g.offset = parsedOffset;
                g.entries = docs.ToArray();
                g.success = docs.Count != 0;
                return g;
            })
            .WithName("GetGemel")
            .WithOpenApi(RegisterGemelQueries);

        app.Run();
    }
}
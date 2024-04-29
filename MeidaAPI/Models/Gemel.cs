using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MeidaAPI.Models;

public class Gemel
{
    public int count { get; set; }
    public int offset { get; set; }
    public bool success { get; set; }

    public GemelEntry[] entries { get; set; }
}

public class GemelEntry
{
    public int? _id { get; set; }
    public int? FUND_ID { get; set; }
    public string? FUND_NAME { get; set; }
    public string? FUND_CLASSIFICATION { get; set; }
    public string? CONTROLLING_CORPORATION { get; set; }
    public string? MANAGING_CORPORATION { get; set; }
    public int? REPORT_PERIOD { get; set; }
    public DateTime? INCEPTION_DATE { get; set; }
    public string? TARGET_POPULATION { get; set; }
    public string? SPECIALIZATION { get; set; }
    public string? SUB_SPECIALIZATION { get; set; }
    public float? DEPOSITS { get; set; }
    public float? WITHDRAWLS { get; set; }
    public float? INTERNAL_TRANSFERS { get; set; }
    public float? NET_MONTHLY_DEPOSITS { get; set; }
    public float? TOTAL_ASSETS { get; set; }
    public float? AVG_ANNUAL_MANAGEMENT_FEE { get; set; }
    public float? AVG_DEPOSIT_FEE { get; set; }
    public float? MONTHLY_YIELD { get; set; }
    public float? YEAR_TO_DATE_YIELD { get; set; }
    public float? YIELD_TRAILING_3_YRS { get; set; }
    public float? YIELD_TRAILING_5_YRS { get; set; }
    public float? AVG_ANNUAL_YIELD_TRAILING_3YRS { get; set; }
    public float? AVG_ANNUAL_YIELD_TRAILING_5YRS { get; set; }
    public float? STANDARD_DEVIATION { get; set; }
    public float? ALPHA { get; set; }
    public float? SHARPE_RATIO { get; set; }
    public float? LIQUID_ASSETS_PERCENT { get; set; }
    public float? STOCK_MARKET_EXPOSURE { get; set; }
    public float? FOREIGN_EXPOSURE { get; set; }
    public float? FOREIGN_CURRENCY_EXPOSURE { get; set; }
    public int? MANAGING_CORPORATION_LEGAL_ID { get; set; }
    public DateTime? CURRENT_DATE { get; set; }
}

public class RawGemelDescriptionEntry
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string _id { get; set; }

    public String? MDS_Name;
    public String? MDS_Info_Name;
    public String? MDS_Des;
}

public class GemelDescriptionEntry
{
    public String? FieldName { get; set; }
    public String? FieldInfoName { get; set; }
    public String? FieldDescription { get; set; }

    private GemelDescriptionEntry(String? fieldName, String? fieldInfoName, String? fieldDescription)
    {
        FieldName = fieldName;
        FieldInfoName = fieldInfoName;
        FieldDescription = fieldDescription;
    }

    public static GemelDescriptionEntry FromRawGemelDescriptionEntry(RawGemelDescriptionEntry entry)
    {
        return new GemelDescriptionEntry(fieldName: entry.MDS_Name, fieldInfoName: entry.MDS_Info_Name,
            fieldDescription: entry.MDS_Des);
    }
}
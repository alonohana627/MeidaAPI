namespace Ingestor.Datastores;

// Kupot Gemel 2024 Dataset - Resource ID: a30dcbea-a1d2-482c-ae29-8f781f5025fb
public class Gemel
{
    public string help { get; set; }
    public bool success { get; set; }
    public Result result { get; set; }
}

public class Result
{
    public bool include_total { get; set; }
    public int limit { get; set; }
    public string records_format { get; set; }
    public string resource_id { get; set; }
    public object total_estimation_threshold { get; set; }
    public Record[] records { get; set; }
    public Field[] fields { get; set; }
    public _Links _links { get; set; }
    public int total { get; set; }
    public bool total_was_estimated { get; set; }
}

public class _Links
{
    public string start { get; set; }
    public string next { get; set; }
}

public class Record
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

public class Field
{
    public string id { get; set; }
    public string type { get; set; }
}

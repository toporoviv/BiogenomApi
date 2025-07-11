using System.Text.Json.Serialization;
using BiogenomApi.Domain.Enums;
using BiogenomApi.Services.Models.Enums;

namespace BiogenomApi.Api.Models.Responses;

public class GetDailyIntakeVitaminsStatsResponse
{
    [JsonPropertyName("vitamin_title")]
    public required string VitaminTitle { get; init; }
    
    [JsonPropertyName("current_intake")]
    public double CurrentIntake { get; init; }
    
    [JsonPropertyName("state")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public VitaminState State { get; init; }
    
    [JsonPropertyName("measurement_unit")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public MeasurementUnit MeasurementUnit { get; init; }
    
    [JsonPropertyName("lower_limit")]
    public double LowerLimit { get; init; }
    
    [JsonPropertyName("upper_limit")]
    public double? UpperLimit { get; init; }
}
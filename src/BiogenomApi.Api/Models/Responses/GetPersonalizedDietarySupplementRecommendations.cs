using System.Text.Json.Serialization;
using BiogenomApi.Domain.Enums;

namespace BiogenomApi.Api.Models.Responses;

public class GetPersonalizedDietarySupplementRecommendationsResponse
{
    [JsonPropertyName("measurement_unit")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public MeasurementUnit MeasurementUnit { get; init; }

    [JsonPropertyName("lower_limit")]
    public double LowerLimit { get; init; }
    
    [JsonPropertyName("upper_limit")]
    public double? UpperLimit { get; init; }
    
    [JsonPropertyName("current_intake")]
    public double CurrentIntake { get; init; }
    
    [JsonPropertyName("dietary_supplement_intake")]
    public double? DietarySupplementIntake { get; init; }
    
    [JsonPropertyName("food_intake")]
    public double? FoodIntake { get; init; }
}
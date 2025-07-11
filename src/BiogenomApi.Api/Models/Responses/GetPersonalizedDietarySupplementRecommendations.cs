using System.Text.Json.Serialization;

namespace BiogenomApi.Api.Models.Responses;

public class GetPersonalizedDietarySupplementRecommendationsResponse
{
    [JsonPropertyName("current_intake")]
    public double CurrentIntake { get; init; }
    
    [JsonPropertyName("dietary_supplement_intake")]
    public double? DietarySupplementIntake { get; init; }
    
    [JsonPropertyName("food_intake")]
    public double? FoodIntake { get; init; }
}
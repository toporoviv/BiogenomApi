using System.Text.Json.Serialization;

namespace BiogenomApi.Api.Models.Responses;

public class GetPersonalizedDietarySupplementsResponse
{
    [JsonPropertyName("title")]
    public required string Title { get; init; }
    
    [JsonPropertyName("alternatives_count")]
    public int AlternativesCount { get; init; }
    
    [JsonPropertyName("image")]
    public required byte[] Image { get; init; }
}
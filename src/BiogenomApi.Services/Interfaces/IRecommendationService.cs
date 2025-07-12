using BiogenomApi.Services.Dtos;

namespace BiogenomApi.Services.Interfaces;

public interface IRecommendationService
{
    Task<PersonalizedDietarySupplementsRecommendationsDto> GetRecommendationForUserAsync(
        ThresholdRecommendationDto dto,
        CancellationToken cancellationToken = default);
}
using BiogenomApi.Services.Dtos;

namespace BiogenomApi.Services.Interfaces;

public interface IVitaminService
{
    Task<DailyIntakeVitaminDto[]> GetDailyIntakeVitaminsStatsAsync(
        GetDailyIntakeVitaminDto dto,
        CancellationToken cancellationToken = default);

    Task<PersonalizedDietarySupplementsDto[]> GetPersonalizedDietarySupplementsAsync(
        GetPersonalizedDietarySupplementsDto dto,
        CancellationToken cancellationToken = default);

    Task<PersonalizedDietarySupplementsRecommendationsDto[]> GetPersonalizedDietarySupplementRecommendationsAsync(
        GetPersonalizedDietarySupplementsRecommendationsDto dto,
        CancellationToken cancellationToken = default);
}
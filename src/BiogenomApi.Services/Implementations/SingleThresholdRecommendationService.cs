using System.Transactions;
using BiogenomApi.Infrastructure;
using BiogenomApi.Infrastructure.Interfaces;
using BiogenomApi.Services.Dtos;
using BiogenomApi.Services.Extensions;
using BiogenomApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BiogenomApi.Services.Implementations;

internal sealed class SingleThresholdRecommendationService(IContextFactory<DataContext> context) 
    : IRecommendationService
{
    public async Task<PersonalizedDietarySupplementsRecommendationsDto> GetRecommendationForUserAsync(
        ThresholdRecommendationDto dto, 
        CancellationToken cancellationToken = default)
    {
        var vitaminDietarySupplement = dto.Vitamin.GetFirstVitaminDietarySupplementForVitaminDeficiency();

        if (vitaminDietarySupplement is not null && 
            dto.Vitamin.IsValueInNormalRange(vitaminDietarySupplement.Amount.Value + dto.CurrentIntake))
        {
            return new PersonalizedDietarySupplementsRecommendationsDto(
                dto.MeasurementUnit,
                dto.CurrentIntake,
                dto.Vitamin.LowerLimit,
                dto.Vitamin.UpperLimit,
                vitaminDietarySupplement.Amount.Value);
        }
        
        // пища - редко изменяемая сущность (либо вообще не изменяемая), поэтому используем ReadUncommitted
        var food = await context.ExecuteWithoutCommitAsync(async dataContext =>
        {
            return await dataContext.Foods
                .Include(x => x.Vitamins)
                .ThenInclude(x => x.Vitamin)
                .FirstOrDefaultAsync(
                    food => food.Vitamins.Any(foodVitamin => foodVitamin.Amount.Unit == dto.Vitamin.MeasurementUnit),
                    cancellationToken);
        }, IsolationLevel.ReadUncommitted, cancellationToken);

        var foodVitamin = food?.Vitamins.FirstOrDefault();

        if (foodVitamin is not null)
        {
            return new PersonalizedDietarySupplementsRecommendationsDto(
                dto.Vitamin.MeasurementUnit,
                dto.CurrentIntake,
                dto.Vitamin.LowerLimit,
                dto.Vitamin.UpperLimit,
                vitaminDietarySupplement?.Amount.Value,
                foodVitamin.Amount.Value);
        }
        
        return new PersonalizedDietarySupplementsRecommendationsDto(
            dto.Vitamin.MeasurementUnit,
            dto.CurrentIntake,
            dto.Vitamin.LowerLimit,
            dto.Vitamin.UpperLimit,
            vitaminDietarySupplement?.Amount.Value);
    }
}
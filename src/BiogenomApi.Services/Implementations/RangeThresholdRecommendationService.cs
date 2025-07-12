using System.Transactions;
using BiogenomApi.Infrastructure;
using BiogenomApi.Infrastructure.Interfaces;
using BiogenomApi.Services.Dtos;
using BiogenomApi.Services.Extensions;
using BiogenomApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BiogenomApi.Services.Implementations;

internal sealed class RangeThresholdRecommendationService(IContextFactory<DataContext> context) 
    : IRecommendationService
{
    public async Task<PersonalizedDietarySupplementsRecommendationsDto> GetRecommendationForUserAsync(
        ThresholdRecommendationDto dto, CancellationToken cancellationToken = default)
    {
        // пользователь за счет питания превысил норму
        if (dto.UpperLimit < dto.CurrentIntake)
        {
            return new PersonalizedDietarySupplementsRecommendationsDto(
                dto.MeasurementUnit,
                dto.CurrentIntake,
                dto.LowerLimit,
                dto.UpperLimit);
        }
        
        var vitaminDietarySupplement = dto.Vitamin.GetFirstVitaminDietarySupplementForVitaminDeficiency();

        if (vitaminDietarySupplement is not null)
        {
            var newIntake = vitaminDietarySupplement.Amount.Value + dto.CurrentIntake;
            
            if (dto.Vitamin.IsValueInNormalRange(newIntake))
            {
                return new PersonalizedDietarySupplementsRecommendationsDto(
                    dto.MeasurementUnit,
                    dto.CurrentIntake,
                    dto.Vitamin.LowerLimit,
                    dto.Vitamin.UpperLimit,
                    vitaminDietarySupplement.Amount.Value);   
            }
            if (newIntake > dto.UpperLimit) // Если мы превысили норму за счет БАДа, то снижаем его дозировку
            {
                return new PersonalizedDietarySupplementsRecommendationsDto(
                    dto.MeasurementUnit,
                    dto.CurrentIntake,
                    dto.Vitamin.LowerLimit,
                    dto.Vitamin.UpperLimit,
                    vitaminDietarySupplement.Amount.Value - (newIntake - dto.UpperLimit));
            }
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
            var newIntake = vitaminDietarySupplement?.Amount.Value ?? 0 + dto.CurrentIntake + foodVitamin.Amount.Value;

            if (dto.Vitamin.IsValueInNormalRange(newIntake))
            {
                return new PersonalizedDietarySupplementsRecommendationsDto(
                    dto.Vitamin.MeasurementUnit,
                    dto.CurrentIntake,
                    dto.Vitamin.LowerLimit,
                    dto.Vitamin.UpperLimit,
                    vitaminDietarySupplement?.Amount.Value,
                    foodVitamin.Amount.Value);   
            }

            // Превысили норму за счет пищи, снижаем ее дозировку
            return new PersonalizedDietarySupplementsRecommendationsDto(
                dto.Vitamin.MeasurementUnit,
                dto.CurrentIntake,
                dto.Vitamin.LowerLimit,
                dto.Vitamin.UpperLimit,
                vitaminDietarySupplement?.Amount.Value,
                foodVitamin.Amount.Value - (newIntake - dto.UpperLimit));   
        }
        
        return new PersonalizedDietarySupplementsRecommendationsDto(
            dto.Vitamin.MeasurementUnit,
            dto.CurrentIntake,
            dto.Vitamin.LowerLimit,
            dto.Vitamin.UpperLimit,
            vitaminDietarySupplement?.Amount.Value);
    }
}
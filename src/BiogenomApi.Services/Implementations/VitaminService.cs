using BiogenomApi.Domain.Entities;
using BiogenomApi.Infrastructure;
using BiogenomApi.Infrastructure.Interfaces;
using BiogenomApi.Services.Dtos;
using BiogenomApi.Services.Exceptions;
using BiogenomApi.Services.Extensions;
using BiogenomApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BiogenomApi.Services.Implementations;

internal sealed class VitaminService(IContextFactory<DataContext> contextFactory) : IVitaminService
{
    public Task<DailyIntakeVitaminDto[]> GetDailyIntakeVitaminsStatsAsync(
        GetDailyIntakeVitaminDto dto,
        CancellationToken cancellationToken = default)
    {
        return contextFactory.ExecuteWithoutCommitAsync(async context =>
        {
            var currentUser = await context.Users
                .Include(x => x.VitaminSurveys)
                .ThenInclude(x => x.Results)
                .FirstOrDefaultAsync(user => user.Id == dto.UserId.Value, cancellationToken);
            
            if (currentUser is null)
            {
                throw new UserNotFoundException($"User with id={dto.UserId} not found");
            }

            var lastSurveyResults = GetLastSurveyResults(currentUser);

            if (lastSurveyResults is null || !lastSurveyResults.Any())
            {
                throw new InvalidOperationException("User has no survey results.");
            }

            return lastSurveyResults
                .Select(result => new DailyIntakeVitaminDto(
                    result.Vitamin.Title, 
                    result.Amount.Value,
                    result.Amount.Unit,
                    result.Vitamin.GetState(result.Amount),
                    result.Vitamin.LowerLimit,
                    result.Vitamin.UpperLimit))
                .ToArray();
        }, cancellationToken: cancellationToken);
    }

    public Task<PersonalizedDietarySupplementsDto[]> GetPersonalizedDietarySupplementsAsync(
        GetPersonalizedDietarySupplementsDto dto,
        CancellationToken cancellationToken = default)
    {
        return contextFactory.ExecuteWithoutCommitAsync(async context =>
        {
            var currentUser = await context.Users
                .Include(x => x.VitaminSurveys)
                .ThenInclude(x => x.Results)
                .ThenInclude(x => x.Vitamin)
                .ThenInclude(x => x.RelatedSupplements)
                .ThenInclude(x => x.DietarySupplement)
                .ThenInclude(x => x.Images)
                .FirstOrDefaultAsync(user => user.Id == dto.UserId.Value, cancellationToken);

            if (currentUser is null)
            {
                throw new UserNotFoundException($"User with id={dto.UserId} not found");
            }

            var lastSurveyResults = GetLastSurveyResults(currentUser);

            if (lastSurveyResults is null || !lastSurveyResults.Any())
            {
                throw new InvalidOperationException("User has no survey results.");
            }
            
            return lastSurveyResults
                .Where(result => !result.Vitamin.IsValueInNormalRange(result.Amount))
                .Select(result => GetPersonalizedDietarySupplementsDtoFromVitamin(result.Vitamin))
                .ToArray();
            
        }, cancellationToken: cancellationToken);
    }

    public Task<PersonalizedDietarySupplementsRecommendationsDto[]> GetPersonalizedDietarySupplementRecommendationsAsync(
        GetPersonalizedDietarySupplementsRecommendationsDto dto,
        CancellationToken cancellationToken = default)
    {
        /*
         * - Находим витамины с дефицитом
         * - Пытаемся компенсировать это БАДами
         * - Если БАД не вывел в норму (или такого нет), то добавляем в рекомендации еще и пищу (например, если речь про ккал)
         * 
         *  P.S. Логика может быть другой, в качестве примера придумал это
         */
        return contextFactory.ExecuteWithoutCommitAsync(async context =>
        {
            var currentUser = await context.Users
                .Include(x => x.VitaminSurveys)
                .ThenInclude(x => x.Results)
                .ThenInclude(x => x.Vitamin)
                .ThenInclude(x => x.RelatedSupplements)
                .FirstOrDefaultAsync(user => user.Id == dto.UserId.Value, cancellationToken);

            if (currentUser is null)
            {
                throw new UserNotFoundException($"User with id={dto.UserId} not found");
            }

            var lastSurveyResults = GetLastSurveyResults(currentUser);

            if (lastSurveyResults is null || !lastSurveyResults.Any())
            {
                throw new InvalidOperationException("User has no survey results.");
            }

            var badResults = lastSurveyResults
                .Where(result => !result.Vitamin.IsValueInNormalRange(result.Amount))
                .Select(result => (Vitamin: result.Vitamin, CurrentIntake: result.Amount))
                .ToArray();

            var recommendations = new List<PersonalizedDietarySupplementsRecommendationsDto>();
            
            foreach (var badResult in badResults)
            {
                var vitaminDietarySupplement = GetFirstVitaminDietarySupplementForVitaminDeficiency(badResult.Vitamin);
                
                if (vitaminDietarySupplement is not null && 
                    badResult.Vitamin.IsValueInNormalRange(
                        vitaminDietarySupplement.Amount.Value + badResult.CurrentIntake.Value))
                {
                    recommendations.Add(new PersonalizedDietarySupplementsRecommendationsDto(
                        badResult.CurrentIntake.Value,
                        vitaminDietarySupplement.Amount.Value));
                    
                    continue;
                }

                var foodVitamin = await GetFirstFoodVitaminForVitaminDeficiencyAsync(context, badResult.Vitamin, cancellationToken);

                if (foodVitamin is not null)
                {
                    recommendations.Add(new PersonalizedDietarySupplementsRecommendationsDto(
                        badResult.CurrentIntake.Value,
                        vitaminDietarySupplement?.Amount.Value,
                        foodVitamin.Amount.Value));
                    
                    continue;
                }
                
                recommendations.Add(new PersonalizedDietarySupplementsRecommendationsDto(badResult.CurrentIntake.Value));
            }

            return recommendations.ToArray();
        }, cancellationToken: cancellationToken);
    }

    private PersonalizedDietarySupplementsDto GetPersonalizedDietarySupplementsDtoFromVitamin(Vitamin vitamin)
    {
        var image = vitamin.RelatedSupplements.First().DietarySupplement.Images.First().Data; // выбираем первый БАД
        var alternativeCount = vitamin.RelatedSupplements.Count - 1;

        return new PersonalizedDietarySupplementsDto(vitamin.Title, image, alternativeCount);
    }

    private List<UserVitaminSurveyResult> GetLastSurveyResults(User user)
    {
        return user.VitaminSurveys
            .OrderBy(x => x.SurveyAtUtc)
            .Last()
            .Results;
    }

    private VitaminDietarySupplement? GetFirstVitaminDietarySupplementForVitaminDeficiency(Vitamin vitamin)
    {
        return vitamin.RelatedSupplements
            .FirstOrDefault(rs =>
                rs.Amount.Unit == vitamin.MeasurementUnit);
    }

    private async Task<FoodVitamin?> GetFirstFoodVitaminForVitaminDeficiencyAsync(
        DataContext context,
        Vitamin vitamin, 
        CancellationToken cancellationToken)
    {
        var food = await context.Foods
            .Include(x => x.Vitamins)
            .ThenInclude(x => x.Vitamin)
            .FirstOrDefaultAsync(food => food.Vitamins.Any(foodVitamin => foodVitamin.Amount.Unit == vitamin.MeasurementUnit), cancellationToken);

        return food?.Vitamins.FirstOrDefault();
    }
}
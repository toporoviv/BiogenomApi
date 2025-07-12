using BiogenomApi.Domain.Entities;
using BiogenomApi.Infrastructure;
using BiogenomApi.Infrastructure.Interfaces;
using BiogenomApi.Services.Dtos;
using BiogenomApi.Services.Exceptions;
using BiogenomApi.Services.Extensions;
using BiogenomApi.Services.Factories;
using BiogenomApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BiogenomApi.Services.Implementations;

internal sealed class VitaminService(
    IContextFactory<DataContext> contextFactory, 
    IRecommendationServiceFactory recommendationServiceFactory) : IVitaminService
{
    public async Task<DailyIntakeVitaminDto[]> GetDailyIntakeVitaminsStatsAsync(
        GetDailyIntakeVitaminDto dto,
        CancellationToken cancellationToken = default)
    {
        var currentUser = await contextFactory.ExecuteWithoutCommitAsync(async context =>
        {
            var currentUser = await context.Users
                .Include(x => x.VitaminSurveys)
                .ThenInclude(x => x.Results)
                .ThenInclude(x => x.Vitamin)
                .FirstOrDefaultAsync(user => user.Id == dto.UserId.Value, cancellationToken);
            
            if (currentUser is null)
            {
                throw new UserNotFoundException($"User with id={dto.UserId.Value} not found");
            }

            return currentUser;
        }, cancellationToken: cancellationToken);
        
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
    }

    public async Task<PersonalizedDietarySupplementsDto[]> GetPersonalizedDietarySupplementsAsync(
        GetPersonalizedDietarySupplementsDto dto,
        CancellationToken cancellationToken = default)
    {
        var currentUser = await contextFactory.ExecuteWithoutCommitAsync(async context =>
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
                throw new UserNotFoundException($"User with id={dto.UserId.Value} not found");
            }

            return currentUser;
        }, cancellationToken: cancellationToken);
        
        var lastSurveyResults = GetLastSurveyResults(currentUser);

        if (lastSurveyResults is null || !lastSurveyResults.Any())
        {
            throw new InvalidOperationException("User has no survey results.");
        }
            
        return lastSurveyResults
            .Where(result => !result.Vitamin.IsValueInNormalRange(result.Amount))
            .Select(result => GetPersonalizedDietarySupplementsDtoFromVitamin(result.Vitamin))
            .ToArray();
    }

    public async Task<PersonalizedDietarySupplementsRecommendationsDto[]> GetPersonalizedDietarySupplementRecommendationsAsync(
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
        var currentUser = await contextFactory.ExecuteWithoutCommitAsync(async context =>
        {
            var currentUser = await context.Users
                .Include(x => x.VitaminSurveys)
                .ThenInclude(x => x.Results)
                .ThenInclude(x => x.Vitamin)
                .ThenInclude(x => x.RelatedSupplements)
                .FirstOrDefaultAsync(user => user.Id == dto.UserId.Value, cancellationToken);

            if (currentUser is null)
            {
                throw new UserNotFoundException($"User with id={dto.UserId.Value} not found");
            }

            return currentUser;
        }, cancellationToken: cancellationToken);
        
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
            var recommendationService = recommendationServiceFactory.Create(badResult.Vitamin);

            var recommendation = await recommendationService
                .GetRecommendationForUserAsync(
                    new ThresholdRecommendationDto(
                        badResult.Vitamin,
                        badResult.Vitamin.MeasurementUnit,
                        badResult.CurrentIntake.Value,
                        badResult.Vitamin.LowerLimit,
                        badResult.Vitamin.UpperLimit),
                    cancellationToken);
                
            recommendations.Add(recommendation);
        }

        return recommendations.ToArray();
    }

    private PersonalizedDietarySupplementsDto GetPersonalizedDietarySupplementsDtoFromVitamin(Vitamin vitamin)
    {
        var image = vitamin.RelatedSupplements.First().DietarySupplement.Images.FirstOrDefault()?.Data; // выбираем первый БАД
        var alternativeCount = vitamin.RelatedSupplements.Count - 1;

        return new PersonalizedDietarySupplementsDto(vitamin.Title, image ?? [], alternativeCount);
    }

    private List<UserVitaminSurveyResult> GetLastSurveyResults(User user)
    {
        return user.VitaminSurveys
            .OrderBy(x => x.SurveyAtUtc)
            .Last()
            .Results;
    }
}
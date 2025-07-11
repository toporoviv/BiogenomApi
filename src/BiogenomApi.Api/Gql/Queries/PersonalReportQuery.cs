using BiogenomApi.Api.Gql.Dtos;
using BiogenomApi.Services.Dtos;
using BiogenomApi.Services.Interfaces;
using GraphQL;

namespace BiogenomApi.Api.Gql.Queries;

[ExtendObjectType("Query")]
public class PersonalReportQuery
{
    [GraphQLName("dailyIntakeVitaminsStats")]
    public async Task<IEnumerable<DailyIntakeVitaminGqlDto>> GetDailyIntakeVitaminsStatsAsync(
        [Service] IVitaminService vitaminService,
        [GraphQLName("userId")] int userId,
        CancellationToken cancellationToken)
    {
        var dto = new GetDailyIntakeVitaminDto(userId);
        var result = await vitaminService.GetDailyIntakeVitaminsStatsAsync(dto, cancellationToken);
        return result.Select(x => new DailyIntakeVitaminGqlDto
        {
            VitaminTitle = x.VitaminTitle,
            LowerLimit = x.LowerLimit,
            MeasurementUnit = x.MeasurementUnit,
            State = x.State,
            UpperLimit = x.UpperLimit,
            CurrentIntake = x.CurrentIntake
        });
    }

    [GraphQLName("personalizedDietarySupplements")]
    public async Task<IEnumerable<DietarySupplementGqlDto>> GetPersonalizedDietarySupplementsAsync(
        [Service] IVitaminService vitaminService,
        [GraphQLName("userId")] int userId,
        CancellationToken cancellationToken)
    {
        var dto = new GetPersonalizedDietarySupplementsDto(userId);
        var result = await vitaminService.GetPersonalizedDietarySupplementsAsync(dto, cancellationToken);
        return result.Select(x => new DietarySupplementGqlDto
        {
            Image = x.Image,
            Title = x.Title,
            AlternativesCount = x.AlternativesCount
        });
    }

    [GraphQLName("personalizedDietarySupplementRecommendations")]
    public async Task<IEnumerable<DietarySupplementRecommendationGqlDto>> GetPersonalizedDietarySupplementRecommendationsAsync(
        [Service] IVitaminService vitaminService,
        [GraphQLName("userId")] int userId,
        CancellationToken cancellationToken)
    {
        var dto = new GetPersonalizedDietarySupplementsRecommendationsDto(userId);
        var result = await vitaminService.GetPersonalizedDietarySupplementRecommendationsAsync(dto, cancellationToken);
        return result.Select(x => new DietarySupplementRecommendationGqlDto
        {
            CurrentIntake = x.CurrentIntake,
            FoodIntake = x.FoodIntake,
            DietarySupplementIntake = x.DietarySupplementIntake
        });
    }
}
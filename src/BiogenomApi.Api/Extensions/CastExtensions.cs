using BiogenomApi.Api.Models.Responses;
using BiogenomApi.Services.Dtos;

namespace BiogenomApi.Api.Extensions;

internal static class CastExtensions
{
    public static List<GetDailyIntakeVitaminsStatsResponse> AsResponse(this IReadOnlyList<DailyIntakeVitaminDto> src)
    {
        var list = new List<GetDailyIntakeVitaminsStatsResponse>();
        
        foreach (var item in src)
        {
            list.Add(new GetDailyIntakeVitaminsStatsResponse
            {
                VitaminTitle = item.VitaminTitle,
                CurrentIntake = item.CurrentIntake,
                LowerLimit = item.LowerLimit,
                State = item.State,
                MeasurementUnit = item.MeasurementUnit,
                UpperLimit = item.UpperLimit
            });
        }

        return list;
    }
    
    public static List<GetPersonalizedDietarySupplementsResponse> AsResponse(
        this IEnumerable<PersonalizedDietarySupplementsDto> src)
    {
        var list = new List<GetPersonalizedDietarySupplementsResponse>();
        
        foreach (var item in src)
        {
            list.Add(new GetPersonalizedDietarySupplementsResponse
            {
                Image = item.Image,
                Title = item.Title,
                AlternativesCount = item.AlternativesCount
            });
        }

        return list;
    }
    
    public static List<GetPersonalizedDietarySupplementRecommendationsResponse> AsResponse(
        this IEnumerable<PersonalizedDietarySupplementsRecommendationsDto> src)
    {
        var list = new List<GetPersonalizedDietarySupplementRecommendationsResponse>();
        
        foreach (var item in src)
        {
            list.Add(new GetPersonalizedDietarySupplementRecommendationsResponse
            {
                CurrentIntake = item.CurrentIntake,
                FoodIntake = item.FoodIntake,
                DietarySupplementIntake = item.DietarySupplementIntake,
                MeasurementUnit = item.MeasurementUnit,
                UpperLimit = item.UpperLimit,
                LowerLimit = item.LowerLimit
            });
        }

        return list;
    }
}
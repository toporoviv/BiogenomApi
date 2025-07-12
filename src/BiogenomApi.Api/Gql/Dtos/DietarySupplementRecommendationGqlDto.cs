using BiogenomApi.Domain.Enums;

namespace BiogenomApi.Api.Gql.Dtos;

public class DietarySupplementRecommendationGqlDto
{
    [GraphQLName("currentIntake")]
    public double CurrentIntake { get; init; }

    [GraphQLName("dietarySupplementIntake")]
    public double? DietarySupplementIntake { get; init; }

    [GraphQLName("foodIntake")]
    public double? FoodIntake { get; init; }
    
    [GraphQLName("measurementUnit")]
    public MeasurementUnit MeasurementUnit { get; init; }

    [GraphQLName("lowerLimit")]
    public double LowerLimit { get; init; }

    [GraphQLName("upperLimit")]
    public double? UpperLimit { get; init; }
}
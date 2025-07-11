namespace BiogenomApi.Api.Gql.Dtos;

public class DietarySupplementRecommendationGqlDto
{
    [GraphQLName("currentIntake")]
    public double CurrentIntake { get; init; }

    [GraphQLName("dietarySupplementIntake")]
    public double? DietarySupplementIntake { get; init; }

    [GraphQLName("foodIntake")]
    public double? FoodIntake { get; init; }
}
using BiogenomApi.Domain.Enums;
using BiogenomApi.Services.Models.Enums;

namespace BiogenomApi.Api.Gql.Dtos;

public class DailyIntakeVitaminGqlDto
{
    [GraphQLName("vitaminTitle")]
    public required string VitaminTitle { get; init; }

    [GraphQLName("currentIntake")]
    public double CurrentIntake { get; init; }

    [GraphQLName("state")]
    public VitaminState State { get; init; }

    [GraphQLName("measurementUnit")]
    public MeasurementUnit MeasurementUnit { get; init; }

    [GraphQLName("lowerLimit")]
    public double LowerLimit { get; init; }

    [GraphQLName("upperLimit")]
    public double? UpperLimit { get; init; }
}
using BiogenomApi.Api.Models.Responses;
using GraphQL.Types;

namespace BiogenomApi.Api.Gql.Dtos;

public class DietarySupplementGqlDto
{
    [GraphQLName("title")]
    public required string Title { get; init; }

    [GraphQLName("alternativesCount")]
    public int AlternativesCount { get; init; }

    [GraphQLName("image")]
    public required byte[] Image { get; init; }
}
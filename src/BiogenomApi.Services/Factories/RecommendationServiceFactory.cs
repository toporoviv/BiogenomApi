using BiogenomApi.Domain.Entities;
using BiogenomApi.Infrastructure;
using BiogenomApi.Infrastructure.Interfaces;
using BiogenomApi.Services.Implementations;
using BiogenomApi.Services.Interfaces;

namespace BiogenomApi.Services.Factories;

public interface IRecommendationServiceFactory
{
    IRecommendationService Create(Vitamin vitamin);
}

internal sealed class RecommendationServiceFactory(IContextFactory<DataContext> context) : IRecommendationServiceFactory
{
    public IRecommendationService Create(Vitamin vitamin)
    {
        if (vitamin.UpperLimit is null)
        {
            return new SingleThresholdRecommendationService(context);
        }

        return new RangeThresholdRecommendationService(context);
    }
}
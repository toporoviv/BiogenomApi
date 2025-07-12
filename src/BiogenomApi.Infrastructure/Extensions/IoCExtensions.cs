using BiogenomApi.Infrastructure.Implementations;
using BiogenomApi.Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BiogenomApi.Infrastructure.Extensions;

public static class IoCExtensions
{
    public static IServiceCollection AddBiogenomInfrastructure(this IServiceCollection services)
    {
        return services
            .AddScoped<IContextFactory<DataContext>, ContextFactory<DataContext>>();
    }
}
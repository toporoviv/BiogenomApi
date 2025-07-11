using BiogenomApi.Services.Implementations;
using BiogenomApi.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BiogenomApi.Services.Extensions;

public static class IoCExtensions
{
    public static IServiceCollection AddBiogenomServices(this IServiceCollection services)
    {
        return services.AddScoped<IVitaminService, VitaminService>();
    }
}
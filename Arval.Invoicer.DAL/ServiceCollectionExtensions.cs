using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Arval.Invoicer.DAL;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddZaptecApiConnector(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<ZaptecApiConnector>(client => {
            client.BaseAddress = new Uri("https://api.zaptec.com");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        });

        return services;
    }
}
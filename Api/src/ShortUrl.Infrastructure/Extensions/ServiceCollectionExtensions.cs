using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShortUrl.Core.Urls.Add;

namespace ShortUrl.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddCosmosUrlDataStore(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddSingleton<CosmosClient>(new CosmosClient(configuration["CosmosDb:ConnectionString"]));
    services.AddSingleton<IUrlDataStore>(s =>
    {
      var client = s.GetRequiredService<CosmosClient>();
      var container = client.GetContainer(configuration["DatabaseName"], configuration["ContainerName"]);
      return new CosmosDbUrlDataStore(container);
    });
    return services;
  }
}

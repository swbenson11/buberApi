using Microsoft.Extensions.DependencyInjection;

namespace BuberDinner.infrastructure;

public static class DependencyInjection
{
   public static IServiceCollection AddBuberInfrastructure(this IServiceCollection services){
      return services;
   }

}

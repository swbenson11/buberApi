using BuberDinner.application.Common.Authentication;
using BuberDinner.application.Common.Interfaces.Authentication;
using BuberDinner.application.Common.Interfaces.Services;
using BuberDinner.infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BuberDinner.infrastructure;

public static class DependencyInjection
{
   public static IServiceCollection AddBuberInfrastructure(this IServiceCollection services){
      services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
      services.AddScoped<IDateTimeProvider, DateTimeProvider>();
      return services;
   }

}

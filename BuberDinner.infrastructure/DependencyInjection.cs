using BuberDinner.application.Common.Authentication;
using BuberDinner.application.Common.Interfaces.Authentication;
using BuberDinner.application.Common.Interfaces.Persistence;
using BuberDinner.application.Common.Interfaces.Services;
using BuberDinner.infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuberDinner.infrastructure;

public static class DependencyInjection
{
   public static IServiceCollection AddBuberInfrastructure(
      this IServiceCollection services, 
      ConfigurationManager  configuration
   ){
      services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
      services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
      services.AddScoped<IDateTimeProvider, DateTimeProvider>();
      services.AddScoped<IUserRepository, UserRepository>();
      return services;
   }

}

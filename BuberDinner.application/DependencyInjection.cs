using BuberDinner.application.Services.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace BuberDinner.application;

public static class DependencyInjection
{
   public static IServiceCollection AddBuberApplication(this IServiceCollection services){
      services.AddScoped<IAuthenticationService, AuthenticationService>();
      return services;
   }

}

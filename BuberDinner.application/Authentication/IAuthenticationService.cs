namespace BuberDinner.application.Services.Authentication;

public interface IAuthenticationService
{
   Task<AuthenticationResult> Register(string firstName, string lastNamer, string email, string password);
   Task<AuthenticationResult> Login(string email, string password);
}
namespace BuberDinner.application.Services.Authenticiation;

public interface IAuthenticationService
{
   Task<AuthenicationResult> Register(string firstName, string lastNamer, string email, string password);
   Task<AuthenicationResult> Login(string email, string password);
}
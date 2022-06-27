namespace BuberDinner.application.Services.Authenticiation;

public interface IAuthenticationService
{
   AuthenicationResult Register(string firstName, string lastNamer, string email, string password);
   AuthenicationResult Login(string email, string password);
}
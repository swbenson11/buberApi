
using ErrorOr;

namespace BuberDinner.application.Services.Authentication;

public interface IAuthenticationService
{
   Task<ErrorOr<AuthenticationResult>> Register(string firstName, string lastNamer, string email, string password);
   Task<ErrorOr<AuthenticationResult>> Login(string email, string password);
}
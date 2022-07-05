using BuberDinner.application.Common.Errors;
using OneOf;

namespace BuberDinner.application.Services.Authentication;

public interface IAuthenticationService
{
   Task<OneOf<AuthenticationResult, IProcessedError>> Register(string firstName, string lastNamer, string email, string password);
   Task<AuthenticationResult> Login(string email, string password);
}
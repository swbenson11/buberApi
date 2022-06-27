namespace BuberDinner.application.Services.Authenticiation;

public class AuthenticationService: IAuthenticationService
{
   public AuthenicationResult Register(string firstName, string lastName, string email, string password){
      return new AuthenicationResult(
         Guid.NewGuid(),
         firstName,
         lastName,
         email,
         "token"
      );
   }
   public AuthenicationResult Login(string email, string password){
      return new AuthenicationResult(
         Guid.NewGuid(),
         "firstName",
         "lastName",
         email,
         "token"
      );
   }
}
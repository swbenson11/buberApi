namespace BuberDinner.application.Services.Authenticiation;

public class AuthenticationService: IAuthenticationService
{
   public async Task<AuthenicationResult> Register(string firstName, string lastName, string email, string password){
      var result =  new AuthenicationResult(
         Guid.NewGuid(),
         firstName,
         lastName,
         email,
         "token"
      );
      return result;
   }
   public async Task<AuthenicationResult> Login(string email, string password){
      return new AuthenicationResult(
         Guid.NewGuid(),
         "firstName",
         "lastName",
         email,
         "token"
      );
   }
}
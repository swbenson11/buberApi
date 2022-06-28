using BuberDinner.application.Common.Interfaces.Authentication;

namespace BuberDinner.application.Services.Authenticiation;

public class AuthenticationService: IAuthenticationService
{
   private readonly IJwtTokenGenerator _jwtTokenGenerator;

      public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator){
      _jwtTokenGenerator = jwtTokenGenerator;

   }
   public async Task<AuthenicationResult> Register(string firstName, string lastName, string email, string password){
      // Check if user already exists

      // Create User - TODO This should be a seperate user service to create a user on our database. 
      Guid userId = Guid.NewGuid();

      //Create JWT Token
      var token= _jwtTokenGenerator.GenerateToken(userId, firstName, lastName);

      var result =  new AuthenicationResult(
         Guid.NewGuid(),
         firstName,
         lastName,
         email,
         token
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
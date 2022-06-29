using BuberDinner.application.Common.Interfaces.Authentication;
using BuberDinner.application.Common.Interfaces.Persistence;
using BuberDinner.domain.Entities;

namespace BuberDinner.application.Services.Authentication;

public class AuthenticationService: IAuthenticationService
{
   private readonly IJwtTokenGenerator _jwtTokenGenerator;
   private readonly IUserRepository _userRepo;

   public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepo)
   {
      _jwtTokenGenerator = jwtTokenGenerator;
      _userRepo = userRepo;
   }

   // Marked this as a Task to see what the code would look like, even then my repos aren't Async yet. 
   public async Task<AuthenticationResult> Register(string firstName, string lastName, string email, string password){
      // Check if user already exists
      if(_userRepo.GetUserByEmail(email) is not null){
         throw new Exception("User with given email already exists.");
      }

      var user = new User {
         FirstName = firstName,
         LastName = lastName,
         Email = email,
         Password = password
      };
      _userRepo.Add(user);

      // Create User - TODO This should be a seperate user service to create a user on our database. 
      Guid userId = Guid.NewGuid();

      //Create JWT Token
      var token= _jwtTokenGenerator.GenerateToken(user);

      var result =  new AuthenticationResult(
         user,
         token
      );
      return result;
   }
   public async Task<AuthenticationResult> Login(string email, string password){
      // Note: this could use customer errors which could be caught by the controller and turned into http error codes. 

      if(_userRepo.GetUserByEmail(email) is not User user){ // cool trick
         throw new Exception("User with given email does not exist.");
      }

      if(user.Password != password){
         throw new Exception("Invalid Password.");
      }

      var token = _jwtTokenGenerator.GenerateToken(user);

      return new AuthenticationResult(
         user,
         token
      );
   }
}
using BuberDinner.application.Common.Interfaces.Authentication;
using BuberDinner.application.Common.Interfaces.Persistence;

using BuberDinner.domain.Entities;
using BuberDinner.Domain.Common.Errors;
using ErrorOr;

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
   public async Task<ErrorOr<AuthenticationResult>> Register(string firstName, string lastName, string email, string password){
      // Check if user already exists
      if(_userRepo.GetUserByEmail(email) is not null){
         return Errors.User.DuplicateEmail;
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
   public async Task<ErrorOr<AuthenticationResult>> Login(string email, string password){
      // Note: this could use customer errors which could be caught by the controller and turned into http error codes. 

      if(_userRepo.GetUserByEmail(email) is not User user){ // cool trick
        return Errors.Authentication.InvalidCredentials;
      }

      if(user.Password != password){
         return Errors.Authentication.InvalidCredentials;
      }

      var token = _jwtTokenGenerator.GenerateToken(user);

      return new AuthenticationResult(
         user,
         token
      );
   }
}
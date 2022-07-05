using BuberDinner.application.Common.Errors;
using BuberDinner.application.Services.Authentication;
using BuberDinner.contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.api.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController: ControllerBase
{
   private readonly IAuthenticationService _authService;
   public AuthenticationController(IAuthenticationService authService){
      _authService = authService;
   }

   [HttpPost("register")]
   public async Task<IActionResult> Register(RegisterRequest request){
      var registerResult = await _authService.Register(
         request.FirstName,
         request.LastName,
         request.Email,
         request.Password
      );

      // TODO this could be a utility class
      if(registerResult.IsT1){
         var error = registerResult.AsT1;
         // I deliberately did not put statusCode in the IProcessedError. I don't want services interpreting and dictating 
         // Status codes to higher level classes
         var statusCode = error.GetType() == typeof(DuplicateEmailError) ? StatusCodes.Status409Conflict: StatusCodes.Status500InternalServerError;

         return Problem(statusCode: statusCode, title: error.ErrorMessage);
      }

      var authResult = registerResult.AsT0;
      var response = new AuthenticationResponse(
         authResult.User.Id,
         authResult.User.FirstName,
         authResult.User.LastName,
         authResult.User.Email,
         authResult.Token
      );

      return Ok(response);
   }

   [HttpPost("login")]
   public async Task<IActionResult> Login(LoginRequest request){
      // Note: you could  wrap this in a timer so that failed log in attempts aren't longer when the email is found
      var authResult = await _authService.Login(
         request.Email,
         request.Password
      );
      var response = new AuthenticationResponse(
         authResult.User.Id,
         authResult.User.FirstName,
         authResult.User.LastName,
         authResult.User.Email,
         authResult.Token
      );

      return Ok(response);
   }
}
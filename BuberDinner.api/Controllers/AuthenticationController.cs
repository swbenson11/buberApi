using BuberDinner.Api.Controllers;
using BuberDinner.application.Services.Authentication;
using BuberDinner.contracts.Authentication;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.api.Controllers;

[Route("auth")]
public class AuthenticationController: ApiController
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

      if(registerResult.IsError){
         return Problem(registerResult.FirstError);
      }

      var authResult = registerResult.Value;
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

      if(authResult.IsError){
         var error = authResult.FirstError;
         var statusCode = error.Type == ErrorType.Conflict ? StatusCodes.Status409Conflict: StatusCodes.Status500InternalServerError;
         // Title for use in error parsing in front end.
         return Problem(statusCode: statusCode,  detail: error.Description, title: error.Code);
      }

      var response = new AuthenticationResponse(
         authResult.Value.User.Id,
         authResult.Value.User.FirstName,
         authResult.Value.User.LastName,
         authResult.Value.User.Email,
         authResult.Value.Token
      );

      return Ok(response);
   }
}
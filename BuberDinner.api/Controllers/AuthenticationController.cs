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
      var authResult = await _authService.Register(
         request.FirstName,
         request.LastName,
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
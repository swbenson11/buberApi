using BuberDinner.application.Services.Authenticiation;
using BuberDinner.contracts.Authenticiation;
using Microsoft.AspNetCore.Mvc;

namespace BuberDiner.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthencicationController: ControllerBase
{
   private readonly IAuthenticationService _authService;
   public AuthencicationController(IAuthenticationService authService){
      _authService = authService;
   }

   [HttpPost("register")]
   public IActionResult Register(RegisterRequest request){
      var authResult = _authService.Register(
         request.FirstName,
         request.LastName,
         request.Email,
         request.Password
      );
      var response = new AuthenticationResponse(
         authResult.Id,
         authResult.FirstName,
         authResult.LastName,
         authResult.Email,
         authResult.Token
      );

      return Ok(response);
   }

   [HttpPost("login")]
   public IActionResult Login(LoginRequest request){
      var authResult = _authService.Login(
         request.Email,
         request.Password
      );
      var response = new AuthenticationResponse(
         authResult.Id,
         authResult.FirstName,
         authResult.LastName,
         authResult.Email,
         authResult.Token
      );

      return Ok(response);
   }
}
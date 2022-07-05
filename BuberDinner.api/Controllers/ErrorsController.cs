using BuberDinner.application.Common.Errors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.api.Controllers;

[ApiController]
public class ErrorsController: ControllerBase
{

   [Route("/error")]
   public IActionResult Error(){
      Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

      var (statusCode, message) = exception switch
      {
         // Guide wanted to remove this, but I like it
         DuplicateEmailException ex => (StatusCodes.Status409Conflict, ex.ErrorMessage),
         IProcessedException ex => ( ex.StatusCode != null ? (int)ex.StatusCode : 500, ex.ErrorMessage),
         _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred")
      };

      return Problem();
   }
}
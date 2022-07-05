using BuberDinner.Api.Controllers;
using BuberDinner.application.Common.Errors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.api.Controllers;

public class ErrorsController: ApiController
{

   [Route("/error")]
   public IActionResult Error(){
      Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

      // var (statusCode, message) = exception switch
      // {
      //    // Guide wanted to remove this, but I like it
      //    IProcessedError ex => ( ex.StatusCode != null ? (int)ex.StatusCode : 500, ex.ErrorMessage),
      //    _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred")
      // };

      return Problem();
   }
}
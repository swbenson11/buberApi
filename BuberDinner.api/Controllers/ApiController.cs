using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers;

[ApiController]
public class ApiController : ControllerBase
{
   protected IActionResult Problem(Error error){
      var statusCode = error.Type switch 
      {
         ErrorType.Conflict => StatusCodes.Status409Conflict,
         ErrorType.Validation => StatusCodes.Status400BadRequest,
         ErrorType.NotFound => StatusCodes.Status404NotFound,
         _ => StatusCodes.Status500InternalServerError,
      };

      return  Problem(statusCode: statusCode, detail: error.Description, title: error.Code);
   }
}
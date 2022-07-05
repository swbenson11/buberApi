using ErrorOr;
using Microsoft.AspNetCore.Mvc;

public static class ErrorHandling {
   public static ProblemDetails processErrorToProblem(Error error){
      // TODO extend this with more types. 
      var statusCode = error.Type == ErrorType.Conflict ? StatusCodes.Status409Conflict: StatusCodes.Status500InternalServerError;
      return  new ProblemDetails{Status= statusCode,  Detail= error.Description, Title= error.Code};
   } 
}
using System.Net;
namespace BuberDinner.application.Common.Errors;

// Original guide said IServiceError, but I like to make it as a "processed error", to make that it's an 
// error we've caught already. Useful for logging, and processing errors higher up the abstraction levels. 
public interface IProcessedException
{
   // I've been following a guide, but I don't like putting status code here. 
   // Now the error message/service is are dictating what non found or duplicate email errors become for http status
   // I would rather have controllers decided, with a middleware or the controller converting as a back up.  
   // That is why I made this nullable
   public HttpStatusCode? StatusCode {get;}
   public string ErrorMessage {get;}

}
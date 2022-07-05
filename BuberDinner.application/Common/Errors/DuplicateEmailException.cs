using System.Net;

namespace BuberDinner.application.Common.Errors;
public class DuplicateEmailException : Exception, IProcessedException
{
   public HttpStatusCode? StatusCode => null;

   public string ErrorMessage => throw new NotImplementedException();

}
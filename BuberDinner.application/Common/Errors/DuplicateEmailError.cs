namespace BuberDinner.application.Common.Errors;
public record DuplicateEmailError : IProcessedError
{
   public  string ErrorMessage => "Email already exists";
}
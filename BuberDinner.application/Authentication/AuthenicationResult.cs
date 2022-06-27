namespace BuberDinner.application.Services.Authenticiation;

public record AuthenicationResult (
   Guid Id, 
   string FirstName,
   string LastName,
   string Email,
   string Token
);
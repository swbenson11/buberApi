namespace BuberDinner.contracts.Authenticiation;

public record RegisterRequest(
   string FirstName,
   string LastName,
   string Email,
   string Password
);
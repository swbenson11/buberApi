namespace BuberDinner.contracts.Authenticiation;

public record LoginRequest(
   string Email,
   string Password
);
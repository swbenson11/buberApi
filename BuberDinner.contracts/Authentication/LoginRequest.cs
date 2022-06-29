namespace BuberDinner.contracts.Authentication;

public record LoginRequest(
   string Email,
   string Password
);
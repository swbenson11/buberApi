using BuberDinner.domain.Entities;

namespace BuberDinner.application.Services.Authentication;

public record AuthenticationResult (
   User User, 
   string Token
);
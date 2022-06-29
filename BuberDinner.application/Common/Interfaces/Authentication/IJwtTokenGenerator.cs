using BuberDinner.domain.Entities;

namespace BuberDinner.application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
   string GenerateToken(User user);
}
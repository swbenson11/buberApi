using BuberDinner.domain.Entities;

namespace BuberDinner.application.Common.Interfaces.Persistence;

public interface IUserRepository
{
   User? GetUserByEmail(string email);
   void Add(User user);
}
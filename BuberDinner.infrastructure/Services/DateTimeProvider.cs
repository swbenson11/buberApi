
using BuberDinner.application.Common.Interfaces.Services;

namespace BuberDinner.infrastructure.Services;

public class DateTimeProvider: IDateTimeProvider
{
   public DateTime UtcNow => DateTime.UtcNow;
}
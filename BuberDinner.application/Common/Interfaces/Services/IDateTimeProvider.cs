namespace BuberDinner.application.Common.Interfaces.Services;

public interface IDateTimeProvider
{
   DateTime UtcNow { get; }
}
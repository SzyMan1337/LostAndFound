namespace LostAndFound.AuthService.Core.DateTimeProviders
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}

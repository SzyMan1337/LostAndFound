namespace LostAndFound.ProfileService.Core.DateTimeProviders
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}

namespace LostAndFound.ChatService.Core.DateTimeProviders
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}

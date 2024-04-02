namespace LostAndFound.PublicationService.Core.Helpers.DateTimeProviders
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}

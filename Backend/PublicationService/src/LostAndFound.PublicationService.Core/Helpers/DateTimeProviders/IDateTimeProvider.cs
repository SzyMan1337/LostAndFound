namespace LostAndFound.PublicationService.Core.Helpers.DateTimeProviders
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}

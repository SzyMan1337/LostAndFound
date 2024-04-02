namespace LostAndFound.ProfileService.Core.DateTimeProviders
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}

namespace LostAndFound.ChatService.Core.DateTimeProviders
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}

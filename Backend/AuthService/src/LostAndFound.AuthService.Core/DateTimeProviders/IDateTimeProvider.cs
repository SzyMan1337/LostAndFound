namespace LostAndFound.AuthService.Core.DateTimeProviders
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}

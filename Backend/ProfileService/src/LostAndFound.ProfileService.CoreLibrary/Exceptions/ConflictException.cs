namespace LostAndFound.ProfileService.CoreLibrary.Exceptions
{
    public class ConflictException : Exception
    {
        public ConflictException(string message) : base(message) { }
    }
}

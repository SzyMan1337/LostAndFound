namespace LostAndFound.ProfileService.CoreLibrary.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message) { }
    }
}

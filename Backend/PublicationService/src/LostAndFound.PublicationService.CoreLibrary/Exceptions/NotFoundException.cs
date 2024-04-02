namespace LostAndFound.PublicationService.CoreLibrary.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }
}

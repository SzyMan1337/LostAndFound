namespace LostAndFound.PublicationService.DataAccess.Entities
{
    public class Vote
    {
        public Guid VoterId { get; set; }
        public int Rating { get; set; }
        public DateTime CreationDate { get; set; }
    }
}

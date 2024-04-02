using LostAndFound.PublicationService.CoreLibrary.Enums;

namespace LostAndFound.PublicationService.CoreLibrary.Requests
{
    /// <summary>
    /// Data to update user vote
    /// </summary>
    public class UpdatePublicationRatingRequestDto
    {
        /// <summary>
        /// New publication vote
        /// </summary>
        public SinglePublicationVote NewPublicationVote { get; set; }
    }
}

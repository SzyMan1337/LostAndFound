using LostAndFound.PublicationService.CoreLibrary.Enums;

namespace LostAndFound.PublicationService.CoreLibrary.Requests
{
    /// <summary>
    /// Data to update publication state
    /// </summary>
    public class UpdatePublicationStateRequestDto
    {
        /// <summary>
        /// New publication state
        /// </summary>
        public PublicationState PublicationState { get; set; }
    }
}

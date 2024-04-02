using LostAndFound.PublicationService.CoreLibrary.Enums;

namespace LostAndFound.PublicationService.CoreLibrary.Requests
{
    /// <summary>
    /// Data to update publication details
    /// </summary>
    public class UpdatePublicationDetailsRequestDto
    {
        /// <summary>
        /// Publication title
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        ///  Publication description
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Publication incident address
        /// </summary>
        public string IncidentAddress { get; set; } = string.Empty;

        /// <summary>
        /// Date of the incident
        /// </summary>
        public DateTime IncidentDate { get; set; }

        /// <summary>
        /// Category id of the object being the subject of the publication
        /// </summary>
        public string SubjectCategoryId { get; set; } = string.Empty;

        /// <summary>
        /// Publication type, lost/found object
        /// </summary>
        public PublicationType PublicationType { get; set; }

        /// <summary>
        /// Publication state (open/closed)
        /// </summary>
        public PublicationState PublicationState { get; set; }
    }
}

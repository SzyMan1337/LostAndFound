namespace LostAndFound.ProfileService.CoreLibrary.Requests
{
    /// <summary>
    /// Data to create a new comment
    /// </summary>
    public class CreateProfileCommentRequestDto
    {
        /// <summary>
        /// New comment text
        /// </summary>
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// Profile rating
        /// </summary>
        public float ProfileRating { get; set; }
    }
}

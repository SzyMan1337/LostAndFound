namespace LostAndFound.ProfileService.CoreLibrary.Requests
{
    /// <summary>
    /// Data for updating the profile comment
    /// </summary>
    public class UpdateProfileCommentRequestDto
    {
        /// <summary>
        /// Updated comment text
        /// </summary>
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// Profile rating
        /// </summary>
        public float ProfileRating { get; set; }
    }
}

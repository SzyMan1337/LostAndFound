namespace LostAndFound.ProfileService.CoreLibrary.Responses
{
    /// <summary>
    /// Profile comment details
    /// </summary>
    public class CommentDataResponseDto
    {
        /// <summary>
        /// Base comment author data
        /// </summary>
        public AuthorDataResponseDto Author { get; set; } = null!;

        /// <summary>
        /// Comment text
        /// </summary>
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// Profile rating
        /// </summary>
        public float ProfileRating { get; set; }

        /// <summary>
        /// Date of adding the comment
        /// </summary>
        public DateTime CreationDate { get; set; }
    }
}

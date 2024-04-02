namespace LostAndFound.ProfileService.CoreLibrary.Responses
{
    /// <summary>
    /// Profile comments section 
    /// </summary>
    public class ProfileCommentsSectionResponseDto
    {
        /// <summary>
        /// Comment of the user downloading the sections
        /// </summary>
        public CommentDataResponseDto? MyComment { get; set; }

        /// <summary>
        /// Page of comments
        /// </summary>
        public IEnumerable<CommentDataResponseDto> Comments { get; set; } = Enumerable.Empty<CommentDataResponseDto>();
    }
}

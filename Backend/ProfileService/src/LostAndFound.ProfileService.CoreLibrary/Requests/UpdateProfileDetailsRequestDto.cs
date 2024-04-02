namespace LostAndFound.ProfileService.CoreLibrary.Requests
{
    /// <summary>
    /// Updated user profile details
    /// </summary>
    public class UpdateProfileDetailsRequestDto
    {
        /// <summary>
        /// User name updated
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// User surname updated
        /// </summary>
        public string? Surname { get; set; }

        /// <summary>
        /// Profile description updated
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// City of residence updated
        /// </summary>
        public string? City { get; set; }
    }
}

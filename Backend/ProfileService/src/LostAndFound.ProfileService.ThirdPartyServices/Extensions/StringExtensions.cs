namespace LostAndFound.ProfileService.ThirdPartyServices.Extensions
{
    public static class StringExtensions
    {
        public static string TruncateToShortString(this string str, int maxLength)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            return str[..Math.Min(str.Length, maxLength)];
        }
    }
}

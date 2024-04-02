namespace LostAndFound.AuthService.CoreLibrary.Settings
{
    public class AuthenticationSettings
    {
        private const string settingName = "LostAndFoundAuthentication";

        public static string SettingName => settingName;
        public string AccessTokenSecret { get; set; } = string.Empty;
        public string RefreshTokenSecret { get; set; } = string.Empty;
        public int AccessTokenExpirationMinutes { get; set; }
        public int RefreshTokenExpirationMinutes { get; set; }
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
    }
}

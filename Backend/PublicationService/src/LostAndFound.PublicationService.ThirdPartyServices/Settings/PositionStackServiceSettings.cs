namespace LostAndFound.PublicationService.ThirdPartyServices.Settings
{
    public class PositionStackServiceSettings
    {
        private const string settingName = "PositionStackService";

        public static string SettingName => settingName;

        public string Uri { get; set; } = string.Empty;
        public string AccessKey { get; set; } = string.Empty;
    }
}

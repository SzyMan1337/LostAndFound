namespace LostAndFound.AuthService.DataAccess.Settings
{
    public class AuthServiceDatabaseSettings
    {
        private const string settingName = "LostAndFoundMongoCluster";

        public static string SettingName => settingName;
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
    }
}

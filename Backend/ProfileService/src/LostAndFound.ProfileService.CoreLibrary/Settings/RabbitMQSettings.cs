namespace LostAndFound.ProfileService.CoreLibrary.Settings
{
    public class RabbitMQSettings
    {
        private const string settingName = "RabbitMQ";

        public static string SettingName => settingName;

        public string HostName { get; set; } = string.Empty;
        public string QueueName { get; set; } = string.Empty;
        public int Port { get; set; }
    }
}

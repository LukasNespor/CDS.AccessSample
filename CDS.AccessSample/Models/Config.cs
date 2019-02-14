using System.Configuration;

namespace CDS.AccessSample.Models
{
    internal static class Config
    {
        public static string EnvironmentName { get { return ConfigurationManager.AppSettings.Get(nameof(EnvironmentName)); } }
        public static string AzureAppId { get { return ConfigurationManager.AppSettings.Get(nameof(AzureAppId)); } }
        public static string UserName { get { return ConfigurationManager.AppSettings.Get(nameof(UserName)); } }
        public static string Password { get { return ConfigurationManager.AppSettings.Get(nameof(Password)); } }

        public static bool IsValid
        {
            get
            {
                return !(
                    string.IsNullOrWhiteSpace(EnvironmentName) ||
                    string.IsNullOrWhiteSpace(AzureAppId) ||
                    string.IsNullOrWhiteSpace(UserName) ||
                    string.IsNullOrWhiteSpace(Password)
                );
            }
        }
    }
}

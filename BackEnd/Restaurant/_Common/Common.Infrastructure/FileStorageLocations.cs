namespace Common.Infrastructure
{
    public class FileStorageLocations
    {
        public const string ConfigurationSettingName = "FileStorageLocations";
        public string Root { get; set; } = string.Empty;

        public string RestaurantsFolder { get; set; } = string.Empty;

        public string UsersFolder { get; set; } = string.Empty;

        public string UserDefaultPhoto { get; set; } = string.Empty;
    }
}
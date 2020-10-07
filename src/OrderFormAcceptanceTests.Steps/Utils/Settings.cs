using Microsoft.Extensions.Configuration;

namespace OrderFormAcceptanceTests.Steps.Utils
{
    public class Settings
    {
        private readonly IConfiguration configuration;

        public Settings(IConfiguration config)
        {
            configuration = config;
            HubUrl = config.GetValue<string>("hubUrl");
            PublicBrowseUrl = config.GetValue<string>("pbUrl");
            Browser = config.GetValue<string>("browser");
        }

        public string HubUrl { get; }
        public string PublicBrowseUrl { get; }
        public string Browser { get; }

        internal string GetDbString(string db)
        {
            var databaseSettings = configuration.GetSection(db).Get<DatabaseSettings>();
            return databaseSettings.ConnectionString;
        }
    }
}

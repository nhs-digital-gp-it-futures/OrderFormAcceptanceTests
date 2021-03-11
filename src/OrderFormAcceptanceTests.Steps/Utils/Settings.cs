namespace OrderFormAcceptanceTests.Steps.Utils
{
    using Microsoft.Extensions.Configuration;

    public class Settings
    {
        private readonly IConfiguration configuration;

        public Settings(IConfiguration config)
        {
            configuration = config;
            HubUrl = config.GetValue<string>("hubUrl");
            OrderFormUrl = config.GetValue<string>("ofUrl");
            PbUrl = config.GetValue<string>("pbUrl");
            Browser = config.GetValue<string>("browser");
            OdsUrl = config.GetValue<string>("odsUrl");
        }

        public string HubUrl { get; }

        public string OrderFormUrl { get; }

        public string Browser { get; }

        public string OdsUrl { get; }

        public string PbUrl { get; set; }

        internal string GetDbString(string db)
        {
            var databaseSettings = configuration.GetSection(db).Get<DatabaseSettings>();
            return databaseSettings.ConnectionString;
        }
    }
}

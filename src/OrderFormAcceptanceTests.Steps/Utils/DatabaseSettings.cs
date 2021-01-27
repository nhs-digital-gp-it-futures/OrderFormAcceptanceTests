namespace OrderFormAcceptanceTests.Steps.Utils
{
    public class DatabaseSettings
    {
        public string ServerUrl { get; set; }

        public string DatabaseName { get; set; }

        public string User { get; set; }

        public string Password { get; set; }

        public string ConnectionString { get => $"Server={ServerUrl};Initial Catalog={DatabaseName};Persist Security Info=false;User Id={User};Password={Password}"; }
    }
}

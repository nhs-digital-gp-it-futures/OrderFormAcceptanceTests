using Newtonsoft.Json;

namespace OrderFormAcceptanceTests.Actions.Utils
{
    public class User
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
using Newtonsoft.Json;

namespace CiFarm.Scripts.Services.NakamaServices
{
    public class Credentials
    {
        [JsonProperty("message")]
        public string message;

        [JsonProperty("publickey")]
        public string publicKey;

        [JsonProperty("signature")]
        public string signature;

        [JsonProperty("chainkey")]
        public string chainKey;
    }

}

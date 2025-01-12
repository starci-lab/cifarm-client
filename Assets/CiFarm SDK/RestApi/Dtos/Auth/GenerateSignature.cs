using System;
using CiFarm.Core.Credentials;
using Newtonsoft.Json;

namespace CiFarm.RestApi
{
    [Serializable]
    public class GenerateSignatureRequest
    {
        [UnityEngine.SerializeField]
        private SupportedChainKey _chainKey;

        [UnityEngine.SerializeField]
        private Network _network;

        [UnityEngine.SerializeField]
        private int _accountNumber;

        [JsonProperty("chainKey")]
        public SupportedChainKey ChainKey
        {
            get => _chainKey;
            set => _chainKey = value;
        }

        [JsonProperty("network")]
        public Network Network
        {
            get => _network;
            set => _network = value;
        }

        [JsonProperty("accountNumber")]
        public int AccountNumber
        {
            get => _accountNumber;
            set => _accountNumber = value;
        }
    }

    [Serializable]
    public class GenerateSignatureResponse
    {
        [UnityEngine.SerializeField]
        private SupportedChainKey _chainKey;

        [UnityEngine.SerializeField]
        private string _message;

        [UnityEngine.SerializeField]
        private string _publicKey;

        [UnityEngine.SerializeField]
        private string _signature;

        [UnityEngine.SerializeField]
        private Network _network;

        [UnityEngine.SerializeField]
        private string _telegramInitDataRaw;

        [UnityEngine.SerializeField]
        private string _accountAddress;

        [JsonProperty("chainKey")]
        public SupportedChainKey ChainKey
        {
            get => _chainKey;
            set => _chainKey = value;
        }

        [JsonProperty("message")]
        public string Message
        {
            get => _message;
            set => _message = value;
        }

        [JsonProperty("publicKey")]
        public string PublicKey
        {
            get => _publicKey;
            set => _publicKey = value;
        }

        [JsonProperty("signature")]
        public string Signature
        {
            get => _signature;
            set => _signature = value;
        }

        [JsonProperty("network")]
        public Network Network
        {
            get => _network;
            set => _network = value;
        }

        [JsonProperty("telegramInitDataRaw")]
        public string TelegramInitDataRaw
        {
            get => _telegramInitDataRaw;
            set => _telegramInitDataRaw = value;
        }

        [JsonProperty("accountAddress")]
        public string AccountAddress
        {
            get => _accountAddress;
            set => _accountAddress = value;
        }
    }
}

using System;
using CiFarm.Core.Credentials;
using Newtonsoft.Json;

namespace CiFarm.RestApi
{
    [Serializable]
    public class VerifySignatureRequest
    {
        // Backing field for the Message property
        [UnityEngine.SerializeField]
        private string _message;

        [JsonProperty("message")]
        public string Message
        {
            get => _message;
            set => _message = value;
        }

        // Backing field for the PublicKey property
        [UnityEngine.SerializeField]
        private string _publicKey;

        [JsonProperty("publicKey")]
        public string PublicKey
        {
            get => _publicKey;
            set => _publicKey = value;
        }

        // Backing field for the Signature property
        [UnityEngine.SerializeField]
        private string _signature;

        [JsonProperty("signature")]
        public string Signature
        {
            get => _signature;
            set => _signature = value;
        }

        // Backing field for the ChainKey property
        [UnityEngine.SerializeField]
        private SupportedChainKey? _chainKey;

        [JsonProperty("chainKey")]
        public SupportedChainKey? ChainKey
        {
            get => _chainKey;
            set => _chainKey = value;
        }

        // Backing field for the Network property
        [UnityEngine.SerializeField]
        private Network? _network;

        [JsonProperty("network")]
        public Network? Network
        {
            get => _network;
            set => _network = value;
        }

        // Backing field for the AccountAddress property
        [UnityEngine.SerializeField]
        private string _accountAddress;

        [JsonProperty("accountAddress")]
        public string AccountAddress
        {
            get => _accountAddress;
            set => _accountAddress = value;
        }
    }

    // Model to represent the VerifySignatureResponse
    [Serializable]
    public class VerifySignatureResponse
    {
        // Backing field for the AccessToken property
        [UnityEngine.SerializeField]
        private string _accessToken;

        [JsonProperty("accessToken")]
        public string AccessToken
        {
            get => _accessToken;
            set => _accessToken = value;
        }

        // Backing field for the RefreshToken property
        [UnityEngine.SerializeField]
        private string _refreshToken;

        [JsonProperty("refreshToken")]
        public string RefreshToken
        {
            get => _refreshToken;
            set => _refreshToken = value;
        }
    }
}

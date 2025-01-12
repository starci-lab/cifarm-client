using System;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.RestApi
{
    [Serializable]
    public class RefreshRequest
    {
        [SerializeField]
        private string _refreshToken;

        [JsonProperty("refreshToken")]
        public string RefreshToken
        {
            get => _refreshToken;
            set => _refreshToken = value;
        }
    }

    [Serializable]
    public class RefreshResponse
    {
        [SerializeField]
        private string _accessToken;

        [JsonProperty("accessToken")]
        public string AccessToken
        {
            get => _accessToken;
            set => _accessToken = value;
        }

        [SerializeField]
        private string _refreshToken;

        [JsonProperty("refreshToken")]
        public string RefreshToken
        {
            get => _refreshToken;
            set => _refreshToken = value;
        }
    }
}

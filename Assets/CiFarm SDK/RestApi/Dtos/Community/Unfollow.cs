using System;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.RestApi
{
    [Serializable]
    public class UnfollowRequest
    {
        [SerializeField]
        private string _unfollowedUserId;

        [JsonProperty("unfollowedUserId")]
        public string UnfollowedUserId
        {
            get => _unfollowedUserId;
            set => _unfollowedUserId = value;
        }
    }

    [Serializable]
    public class UnfollowResponse { }
}

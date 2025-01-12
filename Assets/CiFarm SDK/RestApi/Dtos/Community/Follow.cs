using System;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.RestApi
{
    public class FollowRequest
    {
        [SerializeField]
        private string _followedUserId;

        [JsonProperty("followedUserId")]
        public string FollowedUserId
        {
            get => _followedUserId;
            set => _followedUserId = value;
        }
    }

    [Serializable]
    public class FollowResponse { }
}

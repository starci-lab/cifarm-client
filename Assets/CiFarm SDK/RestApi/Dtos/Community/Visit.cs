using System;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.RestApi
{
    [Serializable]
    public class VisitRequest
    {
        [SerializeField]
        private string _visitingUserId;

        [JsonProperty("visitingUserId")]
        public string VisitingUserId
        {
            get => _visitingUserId;
            set => _visitingUserId = value;
        }

        [SerializeField]
        private bool _isRandom;

        [JsonProperty("isRandom")]
        public bool IsRandom
        {
            get => _isRandom;
            set => _isRandom = value;
        }
    }

    [Serializable]
    public class VisitResponse { }
}

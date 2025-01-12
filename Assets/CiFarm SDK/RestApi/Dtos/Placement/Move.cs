using System;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.RestApi
{
    [Serializable]
    public class MoveRequest
    {
        [SerializeField]
        private string _placedItemId;

        [JsonProperty("placedItemId")]
        public string PlacedItemId
        {
            get => _placedItemId;
            set => _placedItemId = value;
        }

        [SerializeField]
        private Position _position;

        [JsonProperty("position")]
        public Position Position
        {
            get => _position;
            set => _position = value;
        }
    }

    [Serializable]
    public class MoveResponse { }
}

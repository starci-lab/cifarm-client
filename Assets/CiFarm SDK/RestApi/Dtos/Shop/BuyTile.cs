using System;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.RestApi
{
    [Serializable]
    public class BuyTileRequest
    {
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
    public class BuyTileResponse { }
}

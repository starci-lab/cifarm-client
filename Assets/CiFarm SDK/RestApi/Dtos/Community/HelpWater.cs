using System;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.RestApi
{
    [Serializable]
    public class HelpWaterRequest : NeighborAndUserIdRequest
    {
        [SerializeField]
        private string _placedItemTileId;

        [JsonProperty("placedItemTileId")]
        public string PlacedItemTileId
        {
            get => _placedItemTileId;
            set => _placedItemTileId = value;
        }
    }

    [Serializable]
    public class HelpWaterResponse { }
}

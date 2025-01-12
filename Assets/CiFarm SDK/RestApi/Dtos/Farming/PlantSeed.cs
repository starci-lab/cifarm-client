using System;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.RestApi
{
    [Serializable]
    public class PlantSeedRequest
    {
        [SerializeField]
        private string _inventorySeedId;

        [JsonProperty("inventorySeedId")]
        public string InventorySeedId
        {
            get => _inventorySeedId;
            set => _inventorySeedId = value;
        }

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
    public class PlantSeedResponse { }
}

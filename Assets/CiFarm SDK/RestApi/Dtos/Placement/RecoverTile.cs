using System;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.RestApi
{
    [Serializable]
    public class RecoverTileRequest
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
    public class RecoverTileResponse
    {
        [SerializeField]
        private string _inventoryTileId;

        [JsonProperty("inventoryTileId")]
        public string InventoryTileId
        {
            get => _inventoryTileId;
            set => _inventoryTileId = value;
        }
    }
}

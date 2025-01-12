using System;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.RestApi
{
    [Serializable]
    public class PlaceTileRequest
    {
        [SerializeField]
        private string _inventoryTileId;

        [JsonProperty("inventoryTileId")]
        public string InventoryTileId
        {
            get => _inventoryTileId;
            set => _inventoryTileId = value;
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
    public class PlaceTileResponse
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
}

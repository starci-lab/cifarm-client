using System;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.Core.Databases
{
    // Represents a tile entity
    [Serializable]
    public class TileEntity : StringAbstractEntity
    {
        // Public properties for TileEntity without SerializeField attributes

        [JsonProperty("price")]
        public float Price { get; set; }

        [JsonProperty("maxOwnership")]
        public int MaxOwnership { get; set; }

        [JsonProperty("isNFT")]
        public bool IsNFT { get; set; }

        [JsonProperty("availableInShop")]
        public bool AvailableInShop { get; set; }

        [JsonProperty("inventoryType")]
        public InventoryTypeEntity InventoryType { get; set; }

        [JsonProperty("placedItemType")]
        public PlacedItemTypeEntity PlacedItemType { get; set; }
    }
}

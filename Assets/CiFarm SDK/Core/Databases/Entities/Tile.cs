using System;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.Core.Databases
{
    // Represents a tile entity
    [Serializable]
    public class TileEntity : StringAbstractEntity
    {
        // Represents the price of the tile
        [SerializeField] // Unity serialization
        private float _price;

        [JsonProperty("price")]
        public float Price
        {
            get => _price;
            set => _price = value;
        }

        // Represents the maximum ownership of the tile
        [SerializeField] // Unity serialization
        private int _maxOwnership;

        [JsonProperty("maxOwnership")]
        public int MaxOwnership
        {
            get => _maxOwnership;
            set => _maxOwnership = value;
        }

        // Represents whether the tile is an NFT
        [SerializeField] // Unity serialization
        private bool _isNFT;

        [JsonProperty("isNFT")]
        public bool IsNFT
        {
            get => _isNFT;
            set => _isNFT = value;
        }

        // Represents whether the tile is available in the shop
        [SerializeField] // Unity serialization
        private bool _availableInShop;

        [JsonProperty("availableInShop")]
        public bool AvailableInShop
        {
            get => _availableInShop;
            set => _availableInShop = value;
        }

        // Represents the inventory type associated with the tile (nullable)
        [SerializeField] // Unity serialization
        private InventoryTypeEntity _inventoryType;

        [JsonProperty("inventoryType")]
        public InventoryTypeEntity InventoryType
        {
            get => _inventoryType;
            set => _inventoryType = value;
        }

        // Represents the placed item type associated with the tile (nullable)
        [SerializeField] // Unity serialization
        private PlacedItemTypeEntity _placedItemType;

        [JsonProperty("placedItemType")]
        public PlacedItemTypeEntity PlacedItemType
        {
            get => _placedItemType;
            set => _placedItemType = value;
        }
    }
}

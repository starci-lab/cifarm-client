using System;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.Core.Databases
{
    // Represents a tile entity
    [Serializable]
    public class TileEntity : StringAbstractEntity
    {
        // Private backing fields with SerializeField for Unity serialization

        [SerializeField]
        private float _price;

        [SerializeField]
        private int _maxOwnership;

        [SerializeField]
        private bool _isNFT;

        [SerializeField]
        private bool _availableInShop;

        [SerializeField]
        private string _inventoryTypeId;

        [SerializeField]
        private string _placedItemTypeId;

        // Public properties with getters and setters

        [JsonProperty("price")]
        public float Price
        {
            get => _price;
            set => _price = value;
        }

        [JsonProperty("maxOwnership")]
        public int MaxOwnership
        {
            get => _maxOwnership;
            set => _maxOwnership = value;
        }

        [JsonProperty("isNFT")]
        public bool IsNFT
        {
            get => _isNFT;
            set => _isNFT = value;
        }

        [JsonProperty("availableInShop")]
        public bool AvailableInShop
        {
            get => _availableInShop;
            set => _availableInShop = value;
        }

        [JsonProperty("inventoryTypeId")]
        public string InventoryTypeId
        {
            get => _inventoryTypeId;
            set => _inventoryTypeId = value;
        }

        [JsonProperty("placedItemTypeId")]
        public string PlacedItemTypeId
        {
            get => _placedItemTypeId;
            set => _placedItemTypeId = value;
        }
    }
}

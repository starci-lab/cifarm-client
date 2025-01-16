using System;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.Core.Databases
{
    [Serializable]
    public class AnimalEntity : StringAbstractEntity
    {
        // Private backing fields
        [SerializeField]
        private int _yieldTime;

        [SerializeField]
        private int _offspringPrice;

        [SerializeField]
        private bool _isNFT;

        [SerializeField]
        private int? _price;

        [SerializeField]
        private int _growthTime;

        [SerializeField]
        private bool _availableInShop;

        [SerializeField]
        private int _hungerTime;

        [SerializeField]
        private int _minHarvestQuantity;

        [SerializeField]
        private int _maxHarvestQuantity;

        [SerializeField]
        private int _basicHarvestExperiences;

        [SerializeField]
        private int _premiumHarvestExperiences;

        [SerializeField]
        private AnimalType _type;

        [SerializeField]
        private string _productId;

        [SerializeField]
        private string _inventoryTypeId;

        [SerializeField]
        private string _placedItemTypeId;

        // Public properties with getters and setters

        [JsonProperty("yieldTime")]
        public int YieldTime
        {
            get => _yieldTime;
            set => _yieldTime = value;
        }

        [JsonProperty("offspringPrice")]
        public int OffspringPrice
        {
            get => _offspringPrice;
            set => _offspringPrice = value;
        }

        [JsonProperty("isNFT")]
        public bool IsNFT
        {
            get => _isNFT;
            set => _isNFT = value;
        }

        [JsonProperty("price")]
        public int? Price
        {
            get => _price;
            set => _price = value;
        }

        [JsonProperty("growthTime")]
        public int GrowthTime
        {
            get => _growthTime;
            set => _growthTime = value;
        }

        [JsonProperty("availableInShop")]
        public bool AvailableInShop
        {
            get => _availableInShop;
            set => _availableInShop = value;
        }

        [JsonProperty("hungerTime")]
        public int HungerTime
        {
            get => _hungerTime;
            set => _hungerTime = value;
        }

        [JsonProperty("minHarvestQuantity")]
        public int MinHarvestQuantity
        {
            get => _minHarvestQuantity;
            set => _minHarvestQuantity = value;
        }

        [JsonProperty("maxHarvestQuantity")]
        public int MaxHarvestQuantity
        {
            get => _maxHarvestQuantity;
            set => _maxHarvestQuantity = value;
        }

        [JsonProperty("basicHarvestExperiences")]
        public int BasicHarvestExperiences
        {
            get => _basicHarvestExperiences;
            set => _basicHarvestExperiences = value;
        }

        [JsonProperty("premiumHarvestExperiences")]
        public int PremiumHarvestExperiences
        {
            get => _premiumHarvestExperiences;
            set => _premiumHarvestExperiences = value;
        }

        [JsonProperty("type")]
        public AnimalType Type
        {
            get => _type;
            set => _type = value;
        }

        [JsonProperty("productId")]
        public string ProductId
        {
            get => _productId;
            set => _productId = value;
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

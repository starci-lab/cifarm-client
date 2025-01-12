using System;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.Core.Databases
{
    [Serializable]
    public class AnimalEntity : StringAbstractEntity
    {
        // Private fields for AnimalEntity properties
        [SerializeField]
        private int _yieldTime;

        [JsonProperty("yieldTime")]
        public int YieldTime
        {
            get => _yieldTime;
            set => _yieldTime = value;
        }

        [SerializeField]
        private int _offspringPrice;

        [JsonProperty("offspringPrice")]
        public int OffspringPrice
        {
            get => _offspringPrice;
            set => _offspringPrice = value;
        }

        [SerializeField]
        private bool _isNFT;

        [JsonProperty("isNFT")]
        public bool IsNFT
        {
            get => _isNFT;
            set => _isNFT = value;
        }

        [SerializeField]
        private int? _price;

        [JsonProperty("price")]
        public int? Price
        {
            get => _price;
            set => _price = value;
        }

        [SerializeField]
        private int _growthTime;

        [JsonProperty("growthTime")]
        public int GrowthTime
        {
            get => _growthTime;
            set => _growthTime = value;
        }

        [SerializeField]
        private bool _availableInShop;

        [JsonProperty("availableInShop")]
        public bool AvailableInShop
        {
            get => _availableInShop;
            set => _availableInShop = value;
        }

        [SerializeField]
        private int _hungerTime;

        [JsonProperty("hungerTime")]
        public int HungerTime
        {
            get => _hungerTime;
            set => _hungerTime = value;
        }

        [SerializeField]
        private int _minHarvestQuantity;

        [JsonProperty("minHarvestQuantity")]
        public int MinHarvestQuantity
        {
            get => _minHarvestQuantity;
            set => _minHarvestQuantity = value;
        }

        [SerializeField]
        private int _maxHarvestQuantity;

        [JsonProperty("maxHarvestQuantity")]
        public int MaxHarvestQuantity
        {
            get => _maxHarvestQuantity;
            set => _maxHarvestQuantity = value;
        }

        [SerializeField]
        private int _basicHarvestExperiences;

        [JsonProperty("basicHarvestExperiences")]
        public int BasicHarvestExperiences
        {
            get => _basicHarvestExperiences;
            set => _basicHarvestExperiences = value;
        }

        [SerializeField]
        private int _premiumHarvestExperiences;

        [JsonProperty("premiumHarvestExperiences")]
        public int PremiumHarvestExperiences
        {
            get => _premiumHarvestExperiences;
            set => _premiumHarvestExperiences = value;
        }

        [SerializeField]
        private string _type;

        [JsonProperty("type")]
        public string Type
        {
            get => _type;
            set => _type = value;
        }

        // Navigation properties (One-to-One relationships)
        [SerializeField]
        private ProductEntity _product;

        [JsonProperty("product")]
        public ProductEntity Product
        {
            get => _product;
            set => _product = value;
        }

        [SerializeField]
        private InventoryTypeEntity _inventoryType;

        [JsonProperty("inventoryType")]
        public InventoryTypeEntity InventoryType
        {
            get => _inventoryType;
            set => _inventoryType = value;
        }

        [SerializeField]
        private PlacedItemTypeEntity _placedItemType;

        [JsonProperty("placedItemType")]
        public PlacedItemTypeEntity PlacedItemType
        {
            get => _placedItemType;
            set => _placedItemType = value;
        }
    }
}

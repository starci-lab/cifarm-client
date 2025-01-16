using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine; // Required for Unity's [SerializeField] attribute

namespace CiFarm.Core.Databases
{
    [Serializable] // Makes the class serializable for Unity
    public class CropEntity : StringAbstractEntity
    {
        // Private backing fields with SerializeField for Unity serialization
        [SerializeField]
        private int _growthStageDuration;

        [SerializeField]
        private int _growthStages;

        [SerializeField]
        private int _price;

        [SerializeField]
        private bool _premium;

        [SerializeField]
        private int _perennialCount;

        [SerializeField]
        private int _nextGrowthStageAfterHarvest;

        [SerializeField]
        private int _minHarvestQuantity;

        [SerializeField]
        private int _maxHarvestQuantity;

        [SerializeField]
        private int _basicHarvestExperiences;

        [SerializeField]
        private int _premiumHarvestExperiences;

        [SerializeField]
        private bool _availableInShop;

        [SerializeField]
        private int _maxStack;

        [SerializeField]
        private string _productId;

        [SerializeField]
        private string _inventoryTypeId;

        [SerializeField]
        private List<string> _spinPrizeIds;

        // Public properties with getters and setters
        [JsonProperty("growthStageDuration")]
        public int GrowthStageDuration
        {
            get => _growthStageDuration;
            set => _growthStageDuration = value;
        }

        [JsonProperty("growthStages")]
        public int GrowthStages
        {
            get => _growthStages;
            set => _growthStages = value;
        }

        [JsonProperty("price")]
        public int Price
        {
            get => _price;
            set => _price = value;
        }

        [JsonProperty("premium")]
        public bool Premium
        {
            get => _premium;
            set => _premium = value;
        }

        [JsonProperty("perennialCount")]
        public int PerennialCount
        {
            get => _perennialCount;
            set => _perennialCount = value;
        }

        [JsonProperty("nextGrowthStageAfterHarvest")]
        public int NextGrowthStageAfterHarvest
        {
            get => _nextGrowthStageAfterHarvest;
            set => _nextGrowthStageAfterHarvest = value;
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

        [JsonProperty("availableInShop")]
        public bool AvailableInShop
        {
            get => _availableInShop;
            set => _availableInShop = value;
        }

        [JsonProperty("maxStack")]
        public int MaxStack
        {
            get => _maxStack;
            set => _maxStack = value;
        }

        // Navigation properties
        [JsonProperty("productId")]
        public string ProductId
        {
            get => _productId;
            set => _productId = value;
        }

        [JsonProperty("inventoryTypeId")]
        public string InventoryType
        {
            get => _inventoryTypeId;
            set => _inventoryTypeId = value;
        }

        [JsonProperty("spinPrizeIds")]
        public List<string> SpinPrizes
        {
            get => _spinPrizeIds;
            set => _spinPrizeIds = value;
        }
    }
}

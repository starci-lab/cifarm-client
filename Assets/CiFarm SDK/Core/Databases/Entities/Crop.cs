using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine; // Required for Unity's [SerializeField] attribute

namespace CiFarm.Core.Databases
{
    [Serializable] // Makes the class serializable for Unity
    public class CropEntity : UuidAbstractEntity
    {
        // Private backing field for growthStageDuration
        [SerializeField] // Serialize this private field in Unity
        private int _growthStageDuration;

        // Public property for growthStageDuration
        [JsonProperty("growthStageDuration")] // Custom JSON property name in camelCase
        public int GrowthStageDuration
        {
            get => _growthStageDuration;
            set => _growthStageDuration = value;
        }

        // Private backing field for growthStages
        [SerializeField] // Serialize this private field in Unity
        private int _growthStages;

        // Public property for growthStages
        [JsonProperty("growthStages")] // Custom JSON property name in camelCase
        public int GrowthStages
        {
            get => _growthStages;
            set => _growthStages = value;
        }

        // Private backing field for price
        [SerializeField] // Serialize this private field in Unity
        private int _price;

        // Public property for price
        [JsonProperty("price")] // Custom JSON property name in camelCase
        public int Price
        {
            get => _price;
            set => _price = value;
        }

        // Private backing field for premium
        [SerializeField] // Serialize this private field in Unity
        private bool _premium;

        // Public property for premium
        [JsonProperty("premium")] // Custom JSON property name in camelCase
        public bool Premium
        {
            get => _premium;
            set => _premium = value;
        }

        // Private backing field for perennialCount
        [SerializeField] // Serialize this private field in Unity
        private int _perennialCount = 1;

        // Public property for perennialCount
        [JsonProperty("perennialCount")] // Custom JSON property name in camelCase
        public int PerennialCount
        {
            get => _perennialCount;
            set => _perennialCount = value;
        }

        // Private backing field for nextGrowthStageAfterHarvest
        [SerializeField] // Serialize this private field in Unity
        private int _nextGrowthStageAfterHarvest;

        // Public property for nextGrowthStageAfterHarvest
        [JsonProperty("nextGrowthStageAfterHarvest")] // Custom JSON property name in camelCase
        public int NextGrowthStageAfterHarvest
        {
            get => _nextGrowthStageAfterHarvest;
            set => _nextGrowthStageAfterHarvest = value;
        }

        // Private backing field for minHarvestQuantity
        [SerializeField] // Serialize this private field in Unity
        private int _minHarvestQuantity;

        // Public property for minHarvestQuantity
        [JsonProperty("minHarvestQuantity")] // Custom JSON property name in camelCase
        public int MinHarvestQuantity
        {
            get => _minHarvestQuantity;
            set => _minHarvestQuantity = value;
        }

        // Private backing field for maxHarvestQuantity
        [SerializeField] // Serialize this private field in Unity
        private int _maxHarvestQuantity;

        // Public property for maxHarvestQuantity
        [JsonProperty("maxHarvestQuantity")] // Custom JSON property name in camelCase
        public int MaxHarvestQuantity
        {
            get => _maxHarvestQuantity;
            set => _maxHarvestQuantity = value;
        }

        // Private backing field for basicHarvestExperiences
        [SerializeField] // Serialize this private field in Unity
        private int _basicHarvestExperiences;

        // Public property for basicHarvestExperiences
        [JsonProperty("basicHarvestExperiences")] // Custom JSON property name in camelCase
        public int BasicHarvestExperiences
        {
            get => _basicHarvestExperiences;
            set => _basicHarvestExperiences = value;
        }

        // Private backing field for premiumHarvestExperiences
        [SerializeField] // Serialize this private field in Unity
        private int _premiumHarvestExperiences;

        // Public property for premiumHarvestExperiences
        [JsonProperty("premiumHarvestExperiences")] // Custom JSON property name in camelCase
        public int PremiumHarvestExperiences
        {
            get => _premiumHarvestExperiences;
            set => _premiumHarvestExperiences = value;
        }

        // Private backing field for availableInShop
        [SerializeField] // Serialize this private field in Unity
        private bool _availableInShop;

        // Public property for availableInShop
        [JsonProperty("availableInShop")] // Custom JSON property name in camelCase
        public bool AvailableInShop
        {
            get => _availableInShop;
            set => _availableInShop = value;
        }

        // Private backing field for maxStack
        [SerializeField] // Serialize this private field in Unity
        private int _maxStack = 16;

        // Public property for maxStack
        [JsonProperty("maxStack")] // Custom JSON property name in camelCase
        public int MaxStack
        {
            get => _maxStack;
            set => _maxStack = value;
        }

        // Navigation property for ProductEntity (one-to-one relationship)
        [JsonProperty("product")] // Custom JSON property name in camelCase
        public ProductEntity Product { get; set; }

        // Navigation property for InventoryTypeEntity (one-to-one relationship)
        [JsonProperty("inventoryType")] // Custom JSON property name in camelCase
        public InventoryTypeEntity InventoryType { get; set; }

        // Navigation property for SpinPrizeEntity (one-to-many relationship)
        [JsonProperty("spinPrizes")] // Custom JSON property name in camelCase
        public List<SpinPrizeEntity> SpinPrizes { get; set; } = new List<SpinPrizeEntity>();
    }
}

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine; // Required for Unity's [SerializeField] attribute

namespace CiFarm.Core.Databases
{
    [Serializable] // Makes the class serializable for Unity
    public class CropEntity : UuidAbstractEntity
    {
        // Auto-implemented properties
        [JsonProperty("growthStageDuration")]
        public int GrowthStageDuration { get; set; }

        [JsonProperty("growthStages")]
        public int GrowthStages { get; set; }

        [JsonProperty("price")]
        public int Price { get; set; }

        [JsonProperty("premium")]
        public bool Premium { get; set; }

        [JsonProperty("perennialCount")]
        public int PerennialCount { get; set; }

        [JsonProperty("nextGrowthStageAfterHarvest")]
        public int NextGrowthStageAfterHarvest { get; set; }

        [JsonProperty("minHarvestQuantity")]
        public int MinHarvestQuantity { get; set; }

        [JsonProperty("maxHarvestQuantity")]
        public int MaxHarvestQuantity { get; set; }

        [JsonProperty("basicHarvestExperiences")]
        public int BasicHarvestExperiences { get; set; }

        [JsonProperty("premiumHarvestExperiences")]
        public int PremiumHarvestExperiences { get; set; }

        [JsonProperty("availableInShop")]
        public bool AvailableInShop { get; set; }

        [JsonProperty("maxStack")]
        public int MaxStack { get; set; }

        // Navigation properties (still use regular fields with Unity's [SerializeField] for object references)
        [JsonProperty("product")]
        public ProductEntity Product { get; set; }

        [JsonProperty("inventoryType")]
        public InventoryTypeEntity InventoryType { get; set; }

        [JsonProperty("spinPrizes")]
        public List<SpinPrizeEntity> SpinPrizes { get; set; }
    }
}

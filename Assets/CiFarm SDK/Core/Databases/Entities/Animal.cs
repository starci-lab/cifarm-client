using System;
using Newtonsoft.Json;

namespace CiFarm.Core.Databases
{
    [Serializable]
    public class AnimalEntity : StringAbstractEntity
    {
        // Public property for YieldTime
        [JsonProperty("yieldTime")]
        public int YieldTime { get; set; }

        // Public property for OffspringPrice
        [JsonProperty("offspringPrice")]
        public int OffspringPrice { get; set; }

        // Public property for IsNFT
        [JsonProperty("isNFT")]
        public bool IsNFT { get; set; }

        // Public property for Price (nullable)
        [JsonProperty("price")]
        public int? Price { get; set; }

        // Public property for GrowthTime
        [JsonProperty("growthTime")]
        public int GrowthTime { get; set; }

        // Public property for AvailableInShop
        [JsonProperty("availableInShop")]
        public bool AvailableInShop { get; set; }

        // Public property for HungerTime
        [JsonProperty("hungerTime")]
        public int HungerTime { get; set; }

        // Public property for MinHarvestQuantity
        [JsonProperty("minHarvestQuantity")]
        public int MinHarvestQuantity { get; set; }

        // Public property for MaxHarvestQuantity
        [JsonProperty("maxHarvestQuantity")]
        public int MaxHarvestQuantity { get; set; }

        // Public property for BasicHarvestExperiences
        [JsonProperty("basicHarvestExperiences")]
        public int BasicHarvestExperiences { get; set; }

        // Public property for PremiumHarvestExperiences
        [JsonProperty("premiumHarvestExperiences")]
        public int PremiumHarvestExperiences { get; set; }

        // Public property for Type
        [JsonProperty("type")]
        public AnimalType Type { get; set; }

        // Navigation properties (One-to-One relationships)
        [JsonProperty("product")]
        public ProductEntity Product { get; set; }

        [JsonProperty("inventoryType")]
        public InventoryTypeEntity InventoryType { get; set; }

        [JsonProperty("placedItemType")]
        public PlacedItemTypeEntity PlacedItemType { get; set; }
    }
}

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CiFarm.Core.Databases
{
    [Serializable] // Makes the class serializable for Unity
    public class SupplyEntity : StringAbstractEntity
    {
        // Supply type property (Enum)
        [JsonProperty("type")] // JSON property in camelCase
        public SupplyType Type { get; set; }

        // Price property (float)
        [JsonProperty("price")] // JSON property in camelCase
        public float Price { get; set; }

        // Available in shop property (bool)
        [JsonProperty("availableInShop")] // JSON property in camelCase
        public bool AvailableInShop { get; set; }

        // Max stack property (int)
        [JsonProperty("maxStack")] // JSON property in camelCase
        public int MaxStack { get; set; }

        // Fertilizer effect time reduction (nullable int)
        [JsonProperty("fertilizerEffectTimeReduce")] // JSON property in camelCase
        public int? FertilizerEffectTimeReduce { get; set; }

        // Inventory type (Navigation property to InventoryTypeEntity)
        [JsonProperty("inventoryType")] // JSON property in camelCase
        public InventoryTypeEntity InventoryType { get; set; }

        // Spin prizes (One-to-many relationship to SpinPrizeEntity)
        [JsonProperty("spinPrizes")] // JSON property in camelCase
        public List<SpinPrizeEntity> SpinPrizes { get; set; } = new List<SpinPrizeEntity>();
    }
}

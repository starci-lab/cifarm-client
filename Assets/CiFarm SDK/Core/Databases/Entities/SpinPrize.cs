using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CiFarm.Core.Databases
{
    [Serializable] // Makes the class serializable for Unity
    public class SpinPrizeEntity : UuidAbstractEntity
    {
        // Public property for type
        [JsonProperty("type")] // Custom JSON property name in camelCase
        public SpinPrizeType Type { get; set; }

        // Public property for cropId
        [JsonProperty("cropId")] // Custom JSON property name in camelCase
        public string CropId { get; set; }

        // Navigation property for CropEntity
        [JsonProperty("crop")] // Custom JSON property name in camelCase
        public CropEntity Crop { get; set; }

        // Public property for supplyId
        [JsonProperty("supplyId")] // Custom JSON property name in camelCase
        public string SupplyId { get; set; }

        // Navigation property for SupplyEntity
        [JsonProperty("supply")] // Custom JSON property name in camelCase
        public SupplyEntity Supply { get; set; }

        // Public property for golds (nullable int)
        [JsonProperty("golds")] // Custom JSON property name in camelCase
        public int? Golds { get; set; }

        // Public property for tokens (nullable float)
        [JsonProperty("tokens")] // Custom JSON property name in camelCase
        public float? Tokens { get; set; }

        // Public property for quantity (nullable int)
        [JsonProperty("quantity")] // Custom JSON property name in camelCase
        public int? Quantity { get; set; }

        // Public property for appearanceChance
        [JsonProperty("appearanceChance")] // Custom JSON property name in camelCase
        public AppearanceChance AppearanceChance { get; set; }

        // Navigation property for SpinSlotEntity
        [JsonProperty("spinSlots")] // Custom JSON property name in camelCase
        public List<SpinSlotEntity> SpinSlots { get; set; } = new List<SpinSlotEntity>();
    }
}

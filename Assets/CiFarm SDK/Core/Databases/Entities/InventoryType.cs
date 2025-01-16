using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CiFarm.Core.Databases
{
    [Serializable] // Makes the class serializable for Unity
    public class InventoryTypeEntity : UuidAbstractEntity
    {
        // Public properties with auto-implemented getters and setters

        [JsonProperty("type")] // Custom JSON property name in camelCase
        public InventoryType Type { get; set; }

        [JsonProperty("placeable")] // Custom JSON property name in camelCase
        public bool Placeable { get; set; } = false;

        [JsonProperty("deliverable")] // Custom JSON property name in camelCase
        public bool Deliverable { get; set; } = false;

        [JsonProperty("asTool")] // Custom JSON property name in camelCase
        public bool AsTool { get; set; } = false;

        [JsonProperty("maxStack")] // Custom JSON property name in camelCase
        public int MaxStack { get; set; } = 16;

        [JsonProperty("cropId")] // Custom JSON property name in camelCase
        public string CropId { get; set; }

        [JsonProperty("crop")] // Custom JSON property name in camelCase
        public CropEntity Crop { get; set; }

        [JsonProperty("animalId")] // Custom JSON property name in camelCase
        public string AnimalId { get; set; }

        [JsonProperty("animal")] // Custom JSON property name in camelCase
        public AnimalEntity Animal { get; set; }

        [JsonProperty("supplyId")] // Custom JSON property name in camelCase
        public string SupplyId { get; set; }

        [JsonProperty("supply")] // Custom JSON property name in camelCase
        public SupplyEntity Supply { get; set; }

        [JsonProperty("productId")] // Custom JSON property name in camelCase
        public string ProductId { get; set; }

        [JsonProperty("product")] // Custom JSON property name in camelCase
        public ProductEntity Product { get; set; }

        [JsonProperty("tileId")] // Custom JSON property name in camelCase
        public string TileId { get; set; }

        [JsonProperty("tile")] // Custom JSON property name in camelCase
        public TileEntity Tile { get; set; }

        // Navigation property for InventoryEntity (one-to-many relationship)
        [JsonProperty("inventories")] // Custom JSON property name in camelCase
        public List<InventoryEntity> Inventories { get; set; } = new List<InventoryEntity>();
    }
}

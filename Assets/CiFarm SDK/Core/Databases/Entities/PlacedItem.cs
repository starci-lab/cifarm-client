using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CiFarm.Core.Databases
{
    [Serializable] // Makes the class serializable for Unity
    public class PlacedItemEntity : UuidAbstractEntity
    {
        // Public properties for coordinates
        [JsonProperty("x")] // Custom JSON property name in camelCase
        public int X { get; set; }

        [JsonProperty("y")] // Custom JSON property name in camelCase
        public int Y { get; set; }

        // Public property for userId (nullable)
        [JsonProperty("userId")] // Custom JSON property name in camelCase
        public string UserId { get; set; }

        // Navigation property for UserEntity (many-to-one relationship)
        [JsonProperty("user")] // Custom JSON property name in camelCase
        public UserEntity User { get; set; }

        // Public property for inventoryId (nullable)
        [JsonProperty("inventoryId")] // Custom JSON property name in camelCase
        public string InventoryId { get; set; }

        // Navigation property for SeedGrowthInfoEntity (one-to-one relationship)
        [JsonProperty("seedGrowthInfo")] // Custom JSON property name in camelCase
        public SeedGrowthInfoEntity SeedGrowthInfo { get; set; }

        // Navigation property for AnimalInfoEntity (one-to-one relationship)
        [JsonProperty("animalInfo")] // Custom JSON property name in camelCase
        public AnimalInfoEntity AnimalInfo { get; set; }

        // Navigation property for BuildingInfoEntity (one-to-one relationship)
        [JsonProperty("buildingInfo")] // Custom JSON property name in camelCase
        public BuildingInfoEntity BuildingInfo { get; set; }

        // Public property for placedItems (one-to-many relationship)
        [JsonProperty("placedItems")] // Custom JSON property name in camelCase
        public List<PlacedItemEntity> PlacedItems { get; set; } = new List<PlacedItemEntity>();

        // Public property for parentId (nullable)
        [JsonProperty("parentId")] // Custom JSON property name in camelCase
        public string ParentId { get; set; }

        // Navigation property for parent PlacedItemEntity (self-reference)
        [JsonProperty("parent")] // Custom JSON property name in camelCase
        public PlacedItemEntity Parent { get; set; }

        // Public property for placedItemTypeId (nullable)
        [JsonProperty("placedItemTypeId")] // Custom JSON property name in camelCase
        public string PlacedItemTypeId { get; set; }

        // Navigation property for PlacedItemTypeEntity (many-to-one relationship)
        [JsonProperty("placedItemType")] // Custom JSON property name in camelCase
        public PlacedItemTypeEntity PlacedItemType { get; set; }
    }
}

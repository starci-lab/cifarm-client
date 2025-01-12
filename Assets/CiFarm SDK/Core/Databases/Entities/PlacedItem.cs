using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.Core.Databases
{
    [Serializable] // Makes the class serializable for Unity
    public class PlacedItemEntity : UuidAbstractEntity
    {
        // Private backing fields for coordinates
        [SerializeField] // Expose this field for Unity serialization
        private int _x;

        [SerializeField] // Expose this field for Unity serialization
        private int _y;

        // Public properties for coordinates
        [JsonProperty("x")] // Custom JSON property name in camelCase
        public int X
        {
            get => _x;
            set => _x = value;
        }

        [JsonProperty("y")] // Custom JSON property name in camelCase
        public int Y
        {
            get => _y;
            set => _y = value;
        }

        // Private backing field for userId (nullable)
        [SerializeField] // Expose this field for Unity serialization
        private string _userId;

        // Public property for userId (nullable)
        [JsonProperty("userId")] // Custom JSON property name in camelCase
        public string UserId
        {
            get => _userId;
            set => _userId = value;
        }

        // Navigation property for UserEntity (many-to-one relationship)
        [JsonProperty("user")] // Custom JSON property name in camelCase
        public UserEntity User { get; set; }

        // Private backing field for inventoryId (nullable)
        [SerializeField] // Expose this field for Unity serialization
        private string _inventoryId;

        // Public property for inventoryId (nullable)
        [JsonProperty("inventoryId")] // Custom JSON property name in camelCase
        public string InventoryId
        {
            get => _inventoryId;
            set => _inventoryId = value;
        }

        // Navigation property for SeedGrowthInfoEntity (one-to-one relationship)
        [JsonProperty("seedGrowthInfo")] // Custom JSON property name in camelCase
        public SeedGrowthInfoEntity SeedGrowthInfo { get; set; }

        // Navigation property for AnimalInfoEntity (one-to-one relationship)
        [JsonProperty("animalInfo")] // Custom JSON property name in camelCase
        public AnimalInfoEntity AnimalInfo { get; set; }

        // Navigation property for BuildingInfoEntity (one-to-one relationship)
        [JsonProperty("buildingInfo")] // Custom JSON property name in camelCase
        public BuildingInfoEntity BuildingInfo { get; set; }

        // Private backing field for placedItems (one-to-many relationship)
        [SerializeField] // Expose this field for Unity serialization
        private List<PlacedItemEntity> _placedItems = new List<PlacedItemEntity>();

        // Public property for placedItems (one-to-many relationship)
        [JsonProperty("placedItems")] // Custom JSON property name in camelCase
        public List<PlacedItemEntity> PlacedItems
        {
            get => _placedItems;
            set => _placedItems = value;
        }

        // Private backing field for parentId (nullable)
        [SerializeField] // Expose this field for Unity serialization
        private string _parentId;

        // Public property for parentId (nullable)
        [JsonProperty("parentId")] // Custom JSON property name in camelCase
        public string ParentId
        {
            get => _parentId;
            set => _parentId = value;
        }

        // Navigation property for parent PlacedItemEntity (self-reference)
        [JsonProperty("parent")] // Custom JSON property name in camelCase
        public PlacedItemEntity Parent { get; set; }

        // Private backing field for placedItemTypeId (nullable)
        [SerializeField] // Expose this field for Unity serialization
        private string _placedItemTypeId;

        // Public property for placedItemTypeId (nullable)
        [JsonProperty("placedItemTypeId")] // Custom JSON property name in camelCase
        public string PlacedItemTypeId
        {
            get => _placedItemTypeId;
            set => _placedItemTypeId = value;
        }

        // Navigation property for PlacedItemTypeEntity (many-to-one relationship)
        [JsonProperty("placedItemType")] // Custom JSON property name in camelCase
        public PlacedItemTypeEntity PlacedItemType { get; set; }
    }
}

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.Core.Databases
{
    [Serializable] // Makes the class serializable for Unity
    public class InventoryTypeEntity : UuidAbstractEntity
    {
        // Private backing field for type
        [SerializeField] // Expose this field for Unity serialization
        private InventoryType _type;

        // Public property for type
        [JsonProperty("type")] // Custom JSON property name in camelCase
        public InventoryType Type
        {
            get => _type;
            set => _type = value;
        }

        // Private backing field for placeable
        [SerializeField] // Expose this field for Unity serialization
        private bool _placeable = false;

        // Public property for placeable
        [JsonProperty("placeable")] // Custom JSON property name in camelCase
        public bool Placeable
        {
            get => _placeable;
            set => _placeable = value;
        }

        // Private backing field for deliverable
        [SerializeField] // Expose this field for Unity serialization
        private bool _deliverable = false;

        // Public property for deliverable
        [JsonProperty("deliverable")] // Custom JSON property name in camelCase
        public bool Deliverable
        {
            get => _deliverable;
            set => _deliverable = value;
        }

        // Private backing field for asTool
        [SerializeField] // Expose this field for Unity serialization
        private bool _asTool = false;

        // Public property for asTool
        [JsonProperty("asTool")] // Custom JSON property name in camelCase
        public bool AsTool
        {
            get => _asTool;
            set => _asTool = value;
        }

        // Private backing field for maxStack
        [SerializeField] // Expose this field for Unity serialization
        private int _maxStack = 16;

        // Public property for maxStack
        [JsonProperty("maxStack")] // Custom JSON property name in camelCase
        public int MaxStack
        {
            get => _maxStack;
            set => _maxStack = value;
        }

        // Private backing field for cropId (nullable)
        [SerializeField] // Expose this field for Unity serialization
        private string _cropId;

        // Public property for cropId
        [JsonProperty("cropId")] // Custom JSON property name in camelCase
        public string CropId
        {
            get => _cropId;
            set => _cropId = value;
        }

        // Crop navigation property (one-to-one relationship)
        [JsonProperty("crop")] // Custom JSON property name in camelCase
        public CropEntity Crop { get; set; }

        // Private backing field for animalId (nullable)
        [SerializeField] // Expose this field for Unity serialization
        private string _animalId;

        // Public property for animalId
        [JsonProperty("animalId")] // Custom JSON property name in camelCase
        public string AnimalId
        {
            get => _animalId;
            set => _animalId = value;
        }

        // Animal navigation property (one-to-one relationship)
        [JsonProperty("animal")] // Custom JSON property name in camelCase
        public AnimalEntity Animal { get; set; }

        // Private backing field for supplyId (nullable)
        [SerializeField] // Expose this field for Unity serialization
        private string _supplyId;

        // Public property for supplyId
        [JsonProperty("supplyId")] // Custom JSON property name in camelCase
        public string SupplyId
        {
            get => _supplyId;
            set => _supplyId = value;
        }

        // Supply navigation property (one-to-one relationship)
        [JsonProperty("supply")] // Custom JSON property name in camelCase
        public SupplyEntity Supply { get; set; }

        // Private backing field for productId (nullable)
        [SerializeField] // Expose this field for Unity serialization
        private string _productId;

        // Public property for productId
        [JsonProperty("productId")] // Custom JSON property name in camelCase
        public string ProductId
        {
            get => _productId;
            set => _productId = value;
        }

        // Product navigation property (one-to-one relationship)
        [JsonProperty("product")] // Custom JSON property name in camelCase
        public ProductEntity Product { get; set; }

        // Private backing field for tileId (nullable)
        [SerializeField] // Expose this field for Unity serialization
        private string _tileId;

        // Public property for tileId
        [JsonProperty("tileId")] // Custom JSON property name in camelCase
        public string TileId
        {
            get => _tileId;
            set => _tileId = value;
        }

        // Tile navigation property (one-to-one relationship)
        [JsonProperty("tile")] // Custom JSON property name in camelCase
        public TileEntity Tile { get; set; }

        // Navigation property for InventoryEntity (one-to-many relationship)
        [JsonProperty("inventories")] // Custom JSON property name in camelCase
        public List<InventoryEntity> Inventories { get; set; } = new List<InventoryEntity>();
    }
}

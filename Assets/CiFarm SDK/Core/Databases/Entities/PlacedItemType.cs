using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.Core.Databases
{
    [Serializable] // Makes the class serializable for Unity
    public class PlacedItemTypeEntity : StringAbstractEntity
    {
        // Private backing field for type
        [SerializeField] // Expose this field for Unity serialization
        private PlacedItemType _type;

        // Public property for type
        [JsonProperty("type")] // Custom JSON property name in camelCase
        public PlacedItemType Type
        {
            get => _type;
            set => _type = value;
        }

        // Private backing field for tileId (nullable)
        [SerializeField] // Expose this field for Unity serialization
        private string _tileId;

        // Public property for tileId (nullable)
        [JsonProperty("tileId")] // Custom JSON property name in camelCase
        public string TileId
        {
            get => _tileId;
            set => _tileId = value;
        }

        // Navigation property for TileEntity (one-to-one relationship)
        [JsonProperty("tile")] // Custom JSON property name in camelCase
        public TileEntity Tile { get; set; }

        // Private backing field for buildingId (nullable)
        [SerializeField] // Expose this field for Unity serialization
        private string _buildingId;

        // Public property for buildingId (nullable)
        [JsonProperty("buildingId")] // Custom JSON property name in camelCase
        public string BuildingId
        {
            get => _buildingId;
            set => _buildingId = value;
        }

        // Navigation property for BuildingEntity (one-to-one relationship)
        [JsonProperty("building")] // Custom JSON property name in camelCase
        public BuildingEntity Building { get; set; }

        // Private backing field for animalId (nullable)
        [SerializeField] // Expose this field for Unity serialization
        private string _animalId;

        // Public property for animalId (nullable)
        [JsonProperty("animalId")] // Custom JSON property name in camelCase
        public string AnimalId
        {
            get => _animalId;
            set => _animalId = value;
        }

        // Navigation property for AnimalEntity (one-to-one relationship)
        [JsonProperty("animal")] // Custom JSON property name in camelCase
        public AnimalEntity Animal { get; set; }

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
    }
}

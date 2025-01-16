using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CiFarm.Core.Databases
{
    [Serializable] // Makes the class serializable for Unity
    public class PlacedItemTypeEntity : StringAbstractEntity
    {
        // Public property for type
        [JsonProperty("type")] // Custom JSON property name in camelCase
        public PlacedItemType Type { get; set; }

        // Public property for tileId (nullable)
        [JsonProperty("tileId")] // Custom JSON property name in camelCase
        public string TileId { get; set; }

        // Navigation property for TileEntity
        [JsonProperty("tile")] // Custom JSON property name in camelCase
        public TileEntity Tile { get; set; }

        // Public property for buildingId (nullable)
        [JsonProperty("buildingId")] // Custom JSON property name in camelCase
        public string BuildingId { get; set; }

        // Navigation property for BuildingEntity
        [JsonProperty("building")] // Custom JSON property name in camelCase
        public BuildingEntity Building { get; set; }

        // Public property for animalId (nullable)
        [JsonProperty("animalId")] // Custom JSON property name in camelCase
        public string AnimalId { get; set; }

        // Navigation property for AnimalEntity
        [JsonProperty("animal")] // Custom JSON property name in camelCase
        public AnimalEntity Animal { get; set; }

        // Public property for placedItems (one-to-many relationship)
        [JsonProperty("placedItems")] // Custom JSON property name in camelCase
        public List<PlacedItemEntity> PlacedItems { get; set; }
    }
}

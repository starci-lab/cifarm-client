using System;
using Newtonsoft.Json;

namespace CiFarm.Core.Databases
{
    [Serializable] // Makes the class serializable for Unity
    public class BuildingInfoEntity : UuidAbstractEntity
    {
        // Public property for currentUpgrade
        [JsonProperty("currentUpgrade")]
        public int? CurrentUpgrade { get; set; }

        // Public property for occupancy
        [JsonProperty("occupancy")]
        public int? Occupancy { get; set; }

        // Public property for buildingId
        [JsonProperty("buildingId")]
        public string BuildingId { get; set; }

        // Navigation property for BuildingEntity (one-to-many relationship)
        [JsonProperty("building")]
        public BuildingEntity Building { get; set; }

        // Public property for placedItemId
        [JsonProperty("placedItemId")]
        public string PlacedItemId { get; set; }

        // Navigation property for PlacedItemEntity (one-to-one relationship)
        [JsonProperty("placedItem")]
        public PlacedItemEntity PlacedItem { get; set; }

        // Optionally, you could also consider adding a constructor if you need to initialize any fields at the time of instantiation.
    }
}

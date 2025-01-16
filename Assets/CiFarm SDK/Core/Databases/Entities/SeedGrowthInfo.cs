using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CiFarm.Core.Databases
{
    [Serializable] // Makes the class serializable for Unity
    public class SeedGrowthInfoEntity : UuidAbstractEntity
    {
        // Public property for currentStage
        [JsonProperty("currentStage")] // Custom JSON property name in camelCase
        public int CurrentStage { get; set; }

        // Public property for currentStageTimeElapsed
        [JsonProperty("currentStageTimeElapsed")] // Custom JSON property name in camelCase
        public float CurrentStageTimeElapsed { get; set; }

        // Public property for totalTimeElapsed
        [JsonProperty("totalTimeElapsed")] // Custom JSON property name in camelCase
        public float TotalTimeElapsed { get; set; }

        // Public property for currentPerennialCount
        [JsonProperty("currentPerennialCount")] // Custom JSON property name in camelCase
        public int CurrentPerennialCount { get; set; }

        // Public property for harvestQuantityRemaining
        [JsonProperty("harvestQuantityRemaining")] // Custom JSON property name in camelCase
        public int HarvestQuantityRemaining { get; set; }

        // Public property for cropId
        [JsonProperty("cropId")] // Custom JSON property name in camelCase
        public string CropId { get; set; }

        // Navigation property for CropEntity
        [JsonProperty("crop")] // Custom JSON property name in camelCase
        public CropEntity Crop { get; set; }

        // Public property for currentState
        [JsonProperty("currentState")] // Custom JSON property name in camelCase
        public CropCurrentState CurrentState { get; set; }

        // Navigation property for Users who thiefed the item
        [JsonProperty("thiefedBy")] // Custom JSON property name in camelCase
        public List<UserEntity> ThiefedBy { get; set; }

        // Public property for isFertilized
        [JsonProperty("isFertilized")] // Custom JSON property name in camelCase
        public bool IsFertilized { get; set; }

        // Public property for placedItemId
        [JsonProperty("placedItemId")] // Custom JSON property name in camelCase
        public string PlacedItemId { get; set; }

        // Navigation property for PlacedItemEntity
        [JsonProperty("placedItem")] // Custom JSON property name in camelCase
        public PlacedItemEntity PlacedItem { get; set; }
    }
}

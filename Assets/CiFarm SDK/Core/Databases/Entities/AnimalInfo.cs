using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CiFarm.Core.Databases
{
    [Serializable] // Makes the class serializable
    public class AnimalInfoEntity : UuidAbstractEntity
    {
        // Public property for currentGrowthTime
        [JsonProperty("currentGrowthTime")] // Custom JSON property name
        public float CurrentGrowthTime { get; set; }

        // Public property for currentHungryTime
        [JsonProperty("currentHungryTime")] // Custom JSON property name
        public float CurrentHungryTime { get; set; }

        // Public property for currentYieldTime
        [JsonProperty("currentYieldTime")] // Custom JSON property name
        public float CurrentYieldTime { get; set; }

        // Public property for isAdult
        [JsonProperty("isAdult")] // Custom JSON property name
        public bool IsAdult { get; set; }

        // Public property for animalId
        [JsonProperty("animalId")] // Custom JSON property name
        public string AnimalId { get; set; }

        // Navigation property for AnimalEntity
        [JsonProperty("animal")] // Custom JSON property name
        public AnimalEntity Animal { get; set; }

        // Public property for currentState
        [JsonProperty("currentState")] // Custom JSON property name
        public AnimalCurrentState CurrentState { get; set; }

        // Public property for harvestQuantityRemaining (nullable)
        [JsonProperty("harvestQuantityRemaining")] // Custom JSON property name
        public int? HarvestQuantityRemaining { get; set; }

        // Public property for thiefedBy (many-to-many relationship)
        [JsonProperty("thiefedBy")] // Custom JSON property name
        public List<UserEntity> ThiefedBy { get; set; }

        // Public property for alreadySick
        [JsonProperty("alreadySick")] // Custom JSON property name
        public bool AlreadySick { get; set; }

        // Public property for placedItemId
        [JsonProperty("placedItemId")] // Custom JSON property name
        public string PlacedItemId { get; set; }

        // Navigation property for PlacedItemEntity
        [JsonProperty("placedItem")] // Custom JSON property name
        public PlacedItemEntity PlacedItem { get; set; }
    }
}

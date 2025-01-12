using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.Core.Databases
{
    [Serializable] // Makes the class serializable for Unity
    public class SeedGrowthInfoEntity : UuidAbstractEntity
    {
        // Private backing field for currentStage
        [SerializeField] // Expose this field for Unity serialization
        private int _currentStage;

        // Public property for currentStage
        [JsonProperty("currentStage")] // Custom JSON property name in camelCase
        public int CurrentStage
        {
            get => _currentStage;
            set => _currentStage = value;
        }

        // Private backing field for currentStageTimeElapsed
        [SerializeField] // Expose this field for Unity serialization
        private float _currentStageTimeElapsed;

        // Public property for currentStageTimeElapsed
        [JsonProperty("currentStageTimeElapsed")] // Custom JSON property name in camelCase
        public float CurrentStageTimeElapsed
        {
            get => _currentStageTimeElapsed;
            set => _currentStageTimeElapsed = value;
        }

        // Private backing field for totalTimeElapsed
        [SerializeField] // Expose this field for Unity serialization
        private float _totalTimeElapsed;

        // Public property for totalTimeElapsed
        [JsonProperty("totalTimeElapsed")] // Custom JSON property name in camelCase
        public float TotalTimeElapsed
        {
            get => _totalTimeElapsed;
            set => _totalTimeElapsed = value;
        }

        // Private backing field for currentPerennialCount
        [SerializeField] // Expose this field for Unity serialization
        private int _currentPerennialCount;

        // Public property for currentPerennialCount
        [JsonProperty("currentPerennialCount")] // Custom JSON property name in camelCase
        public int CurrentPerennialCount
        {
            get => _currentPerennialCount;
            set => _currentPerennialCount = value;
        }

        // Private backing field for harvestQuantityRemaining
        [SerializeField] // Expose this field for Unity serialization
        private int _harvestQuantityRemaining;

        // Public property for harvestQuantityRemaining
        [JsonProperty("harvestQuantityRemaining")] // Custom JSON property name in camelCase
        public int HarvestQuantityRemaining
        {
            get => _harvestQuantityRemaining;
            set => _harvestQuantityRemaining = value;
        }

        // Private backing field for cropId
        [SerializeField] // Expose this field for Unity serialization
        private string _cropId;

        // Public property for cropId
        [JsonProperty("cropId")] // Custom JSON property name in camelCase
        public string CropId
        {
            get => _cropId;
            set => _cropId = value;
        }

        // Navigation property for CropEntity (many-to-one relationship)
        [JsonProperty("crop")] // Custom JSON property name in camelCase
        public CropEntity Crop { get; set; }

        // Private backing field for currentState (enum)
        [SerializeField] // Expose this field for Unity serialization
        private CropCurrentState _currentState;

        // Public property for currentState
        [JsonProperty("currentState")] // Custom JSON property name in camelCase
        public CropCurrentState CurrentState
        {
            get => _currentState;
            set => _currentState = value;
        }

        // Navigation property for Users who thiefed the item (many-to-many relationship)
        [JsonProperty("thiefedBy")] // Custom JSON property name in camelCase
        public List<UserEntity> ThiefedBy { get; set; }

        // Private backing field for isFertilized
        [SerializeField] // Expose this field for Unity serialization
        private bool _isFertilized;

        // Public property for isFertilized
        [JsonProperty("isFertilized")] // Custom JSON property name in camelCase
        public bool IsFertilized
        {
            get => _isFertilized;
            set => _isFertilized = value;
        }

        // Private backing field for placedItemId
        [SerializeField] // Expose this field for Unity serialization
        private string _placedItemId;

        // Public property for placedItemId
        [JsonProperty("placedItemId")] // Custom JSON property name in camelCase
        public string PlacedItemId
        {
            get => _placedItemId;
            set => _placedItemId = value;
        }

        // Navigation property for PlacedItemEntity (one-to-one relationship)
        [JsonProperty("placedItem")] // Custom JSON property name in camelCase
        public PlacedItemEntity PlacedItem { get; set; }
    }
}

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.Core.Databases
{
    [Serializable] // Makes the class serializable
    public class AnimalInfoEntity : UuidAbstractEntity
    {
        // Private backing field for currentGrowthTime
        [SerializeField] // Expose this field for Unity serialization
        private float _currentGrowthTime = 0;

        // Public property with getter and setter
        [JsonProperty("currentGrowthTime")] // Custom JSON property name
        public float CurrentGrowthTime
        {
            get => _currentGrowthTime;
            set => _currentGrowthTime = value;
        }

        // Private backing field for currentHungryTime
        [SerializeField] // Expose this field for Unity serialization
        private float _currentHungryTime = 0;

        // Public property with getter and setter
        [JsonProperty("currentHungryTime")] // Custom JSON property name
        public float CurrentHungryTime
        {
            get => _currentHungryTime;
            set => _currentHungryTime = value;
        }

        // Private backing field for currentYieldTime
        [SerializeField] // Expose this field for Unity serialization
        private float _currentYieldTime = 0;

        // Public property with getter and setter
        [JsonProperty("currentYieldTime")] // Custom JSON property name
        public float CurrentYieldTime
        {
            get => _currentYieldTime;
            set => _currentYieldTime = value;
        }

        // Private backing field for isAdult
        [SerializeField] // Expose this field for Unity serialization
        private bool _isAdult = false;

        // Public property with getter and setter
        [JsonProperty("isAdult")] // Custom JSON property name
        public bool IsAdult
        {
            get => _isAdult;
            set => _isAdult = value;
        }

        // Private backing field for animalId
        [SerializeField] // Expose this field for Unity serialization
        private string _animalId;

        // Public property with getter and setter
        [JsonProperty("animalId")] // Custom JSON property name
        public string AnimalId
        {
            get => _animalId;
            set => _animalId = value;
        }

        // Animal reference (navigation property)
        [JsonProperty("animal")] // Custom JSON property name
        public AnimalEntity Animal { get; set; }

        // Private backing field for currentState
        [SerializeField] // Expose this field for Unity serialization
        private AnimalCurrentState _currentState = AnimalCurrentState.Normal;

        // Public property with getter and setter
        [JsonProperty("currentState")] // Custom JSON property name
        public AnimalCurrentState CurrentState
        {
            get => _currentState;
            set => _currentState = value;
        }

        // Private backing field for harvestQuantityRemaining (nullable)
        [SerializeField] // Expose this field for Unity serialization
        private int? _harvestQuantityRemaining;

        // Public property with getter and setter
        [JsonProperty("harvestQuantityRemaining")] // Custom JSON property name
        public int? HarvestQuantityRemaining
        {
            get => _harvestQuantityRemaining;
            set => _harvestQuantityRemaining = value;
        }

        // Thiefed by users (many-to-many relationship)
        [JsonProperty("thiefedBy")] // Custom JSON property name
        public List<UserEntity> ThiefedBy { get; set; } = new List<UserEntity>();

        // Private backing field for alreadySick
        [SerializeField] // Expose this field for Unity serialization
        private bool _alreadySick = false;

        // Public property with getter and setter
        [JsonProperty("alreadySick")] // Custom JSON property name
        public bool AlreadySick
        {
            get => _alreadySick;
            set => _alreadySick = value;
        }

        // Private backing field for placedItemId
        [SerializeField] // Expose this field for Unity serialization
        private string _placedItemId;

        // Public property with getter and setter
        [JsonProperty("placedItemId")] // Custom JSON property name
        public string PlacedItemId
        {
            get => _placedItemId;
            set => _placedItemId = value;
        }

        // Placed item reference (one-to-one relationship)
        [JsonProperty("placedItem")] // Custom JSON property name
        public PlacedItemEntity PlacedItem { get; set; }
    }
}

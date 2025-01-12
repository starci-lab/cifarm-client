using System;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.Core.Databases
{
    [Serializable]
    public class BuildingInfoEntity : UuidAbstractEntity
    {
        // Private fields for BuildingInfoEntity properties

        [SerializeField]
        private int? _currentUpgrade;

        [JsonProperty("currentUpgrade")]
        public int? CurrentUpgrade
        {
            get => _currentUpgrade;
            set => _currentUpgrade = value;
        }

        [SerializeField]
        private int? _occupancy;

        [JsonProperty("occupancy")]
        public int? Occupancy
        {
            get => _occupancy;
            set => _occupancy = value;
        }

        [SerializeField]
        private string _buildingId;

        [JsonProperty("buildingId")]
        public string BuildingId
        {
            get => _buildingId;
            set => _buildingId = value;
        }

        // Navigation properties (One-to-Many and One-to-One relationships)

        [SerializeField]
        private BuildingEntity _building;

        [JsonProperty("building")]
        public BuildingEntity Building
        {
            get => _building;
            set => _building = value;
        }

        [SerializeField]
        private string _placedItemId;

        [JsonProperty("placedItemId")]
        public string PlacedItemId
        {
            get => _placedItemId;
            set => _placedItemId = value;
        }

        [SerializeField]
        private PlacedItemEntity _placedItem;

        [JsonProperty("placedItem")]
        public PlacedItemEntity PlacedItem
        {
            get => _placedItem;
            set => _placedItem = value;
        }
    }
}

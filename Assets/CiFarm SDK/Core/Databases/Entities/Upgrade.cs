using System;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.Core.Databases
{
    // Represents an upgrade entity
    [Serializable]
    public class UpgradeEntity : StringAbstractEntity
    {
        // Private backing fields with SerializeField for Unity serialization

        [SerializeField]
        private int _upgradePrice;

        [SerializeField]
        private int _capacity;

        [SerializeField]
        private int _upgradeLevel;

        [SerializeField]
        private string _buildingId;

        // Public properties with getters and setters

        [JsonProperty("upgradePrice")]
        public int UpgradePrice
        {
            get => _upgradePrice;
            set => _upgradePrice = value;
        }

        [JsonProperty("capacity")]
        public int Capacity
        {
            get => _capacity;
            set => _capacity = value;
        }

        [JsonProperty("upgradeLevel")]
        public int UpgradeLevel
        {
            get => _upgradeLevel;
            set => _upgradeLevel = value;
        }

        [JsonProperty("buildingId")]
        public string BuildingId
        {
            get => _buildingId;
            set => _buildingId = value;
        }
    }
}

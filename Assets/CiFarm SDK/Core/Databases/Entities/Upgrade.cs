using System;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.Core.Databases
{
    // Represents an upgrade entity
    [Serializable]
    public class UpgradeEntity : StringAbstractEntity
    {
        // Private backing field for upgrade price
        [SerializeField] // Unity serialization
        private int _upgradePrice;

        [JsonProperty("upgradePrice")]
        public int UpgradePrice
        {
            get => _upgradePrice;
            set => _upgradePrice = value;
        }

        // Private backing field for capacity
        [SerializeField] // Unity serialization
        private int _capacity;

        [JsonProperty("capacity")]
        public int Capacity
        {
            get => _capacity;
            set => _capacity = value;
        }

        // Private backing field for upgrade level
        [SerializeField] // Unity serialization
        private int _upgradeLevel;

        [JsonProperty("upgradeLevel")]
        public int UpgradeLevel
        {
            get => _upgradeLevel;
            set => _upgradeLevel = value;
        }

        // Navigation property for the associated BuildingEntity
        [SerializeField] // Unity serialization
        private BuildingEntity _building;

        [JsonProperty("building")]
        public BuildingEntity Building
        {
            get => _building;
            set => _building = value;
        }
    }
}

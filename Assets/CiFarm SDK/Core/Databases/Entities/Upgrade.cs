using System;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.Core.Databases
{
    // Represents an upgrade entity
    [Serializable]
    public class UpgradeEntity : StringAbstractEntity
    {
        // Public properties for UpgradeEntity without SerializeField attributes

        [JsonProperty("upgradePrice")]
        public int UpgradePrice { get; set; }

        [JsonProperty("capacity")]
        public int Capacity { get; set; }

        [JsonProperty("upgradeLevel")]
        public int UpgradeLevel { get; set; }

        [JsonProperty("building")]
        public BuildingEntity Building { get; set; }
    }
}

using System;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.Core.Databases
{
    // Represents a temporary entity for data storage
    [Serializable]
    public class TempEntity : StringAbstractEntity
    {
        // Public property for value without SerializeField
        [JsonProperty("value")]
        public string Value { get; set; }
    }

    // Represents the last growth schedule for animal
    [Serializable]
    public class AnimalGrowthLastSchedule
    {
        // Public property for date without SerializeField
        [JsonProperty("date")]
        public DateTime Date { get; set; }
    }

    // Represents the last growth schedule for crops
    [Serializable]
    public class CropGrowthLastSchedule
    {
        // Public property for date without SerializeField
        [JsonProperty("date")]
        public DateTime Date { get; set; }
    }

    // Represents the last growth schedule for energy regeneration
    [Serializable]
    public class EnergyGrowthLastSchedule
    {
        // Public property for date without SerializeField
        [JsonProperty("date")]
        public DateTime Date { get; set; }
    }
}

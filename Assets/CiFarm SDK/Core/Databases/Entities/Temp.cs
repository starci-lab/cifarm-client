using System;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.Core.Databases
{
    // Represents a temporary entity for data storage
    [Serializable]
    public class TempEntity : StringAbstractEntity
    {
        // Private backing field for value
        [SerializeField]
        private string _value;

        // Public property for value
        [JsonProperty("value")]
        public string Value
        {
            get => _value;
            set => _value = value;
        }
    }

    // Represents the last growth schedule for animal
    [Serializable]
    public class AnimalGrowthLastSchedule
    {
        [SerializeField]
        private DateTime _date;

        [JsonProperty("date")]
        public DateTime Date
        {
            get => _date;
            set => _date = value;
        }
    }

    // Represents the last growth schedule for crops
    [Serializable]
    public class CropGrowthLastSchedule
    {
        [SerializeField]
        private DateTime _date;

        [JsonProperty("date")]
        public DateTime Date
        {
            get => _date;
            set => _date = value;
        }
    }

    // Represents the last growth schedule for energy regeneration
    [Serializable]
    public class EnergyGrowthLastSchedule
    {
        [SerializeField]
        private DateTime _date;

        [JsonProperty("date")]
        public DateTime Date
        {
            get => _date;
            set => _date = value;
        }
    }
}

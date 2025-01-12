using System;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.Core.Databases
{
    [Serializable] // Makes the class serializable
    public class DailyRewardEntity : StringAbstractEntity
    {
        // Private backing field for golds
        [SerializeField] // Expose this field for Unity serialization
        private int? _golds;

        // Public property for golds
        [JsonProperty("golds")] // Custom JSON property name
        public int? Golds
        {
            get => _golds;
            set => _golds = value;
        }

        // Private backing field for tokens
        [SerializeField] // Expose this field for Unity serialization
        private float? _tokens;

        // Public property for tokens
        [JsonProperty("tokens")] // Custom JSON property name
        public float? Tokens
        {
            get => _tokens;
            set => _tokens = value;
        }

        // Private backing field for day
        [SerializeField] // Expose this field for Unity serialization
        private int _day;

        // Public property for day
        [JsonProperty("day")] // Custom JSON property name
        public int Day
        {
            get => _day;
            set => _day = value;
        }

        // Private backing field for lastDay
        [SerializeField] // Expose this field for Unity serialization
        private bool _lastDay = false;

        // Public property for lastDay
        [JsonProperty("lastDay")] // Custom JSON property name
        public bool LastDay
        {
            get => _lastDay;
            set => _lastDay = value;
        }
    }
}

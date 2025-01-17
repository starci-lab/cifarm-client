using System;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.Core.Databases
{
    [Serializable] // Makes the class serializable
    public class DailyRewardEntity : StringAbstractEntity
    {
        // Private backing fields with SerializeField for Unity serialization

        [SerializeField]
        private int? _golds;

        [SerializeField]
        private float? _tokens;

        [SerializeField]
        private int? _day;

        [SerializeField]
        private bool _lastDay;

        // Public properties with getters and setters

        [JsonProperty("golds")]
        public int? Golds
        {
            get => _golds;
            set => _golds = value;
        }

        [JsonProperty("tokens")]
        public float? Tokens
        {
            get => _tokens;
            set => _tokens = value;
        }

        [JsonProperty("day")]
        public int? Day
        {
            get => _day;
            set => _day = value;
        }

        [JsonProperty("lastDay")]
        public bool LastDay
        {
            get => _lastDay;
            set => _lastDay = value;
        }
    }
}

using System;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.Core.Databases
{
    [Serializable] // Makes the class serializable
    public class DailyRewardEntity : StringAbstractEntity
    {
        // Public auto-implemented properties with nullable types
        [JsonProperty("golds")] // Custom JSON property name
        public int? Golds { get; set; }

        [JsonProperty("tokens")] // Custom JSON property name
        public float? Tokens { get; set; }

        [JsonProperty("day")] // Custom JSON property name
        public int Day { get; set; }

        [JsonProperty("lastDay")] // Custom JSON property name
        public bool LastDay { get; set; }
    }
}

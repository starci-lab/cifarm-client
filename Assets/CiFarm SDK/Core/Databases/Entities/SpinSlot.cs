using System;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.Core.Databases
{
    [Serializable] // Makes the class serializable for Unity
    public class SpinSlotEntity : UuidAbstractEntity
    {
        // Private backing field for spinPrizeId
        [SerializeField] // Expose this field for Unity serialization
        private string _spinPrizeId;

        // Public property for spinPrizeId
        [JsonProperty("spinPrizeId")] // Custom JSON property name in camelCase
        public string SpinPrizeId
        {
            get => _spinPrizeId;
            set => _spinPrizeId = value;
        }

        [SerializeField] // Expose this field for Unity serialization
        private SpinPrizeEntity _spinPrize;

        // Navigation property for SpinPrizeEntity
        [JsonProperty("spinPrize")] // Custom JSON property name in camelCase
        public SpinPrizeEntity SpinPrize
        {
            get => _spinPrize;
            set => _spinPrize = value;
        }
    }
}

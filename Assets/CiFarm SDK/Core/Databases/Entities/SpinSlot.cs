using System;
using Newtonsoft.Json;
using UnityEngine; // For [SerializeField] attribute

namespace CiFarm.Core.Databases
{
    [Serializable] // Makes the class serializable for Unity
    public class SpinSlotEntity : UuidAbstractEntity
    {
        // Private backing field for SpinPrizeId for Unity serialization
        [SerializeField] // Makes the field visible in Unity Inspector
        private string _spinPrizeId;

        // Public property for spinPrizeId with custom JSON property name in camelCase
        [JsonProperty("spinPrizeId")]
        public string SpinPrizeId
        {
            get { return _spinPrizeId; }
            set { _spinPrizeId = value; }
        }
    }
}

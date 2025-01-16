using System;
using Newtonsoft.Json;

namespace CiFarm.Core.Databases
{
    [Serializable] // Makes the class serializable for Unity
    public class SpinSlotEntity : UuidAbstractEntity
    {
        // Public property for spinPrizeId
        [JsonProperty("spinPrizeId")] // Custom JSON property name in camelCase
        public string SpinPrizeId { get; set; }

        // Public property for spinPrize (Navigation property to SpinPrizeEntity)
        [JsonProperty("spinPrize")] // Custom JSON property name in camelCase
        public SpinPrizeEntity SpinPrize { get; set; }
    }
}

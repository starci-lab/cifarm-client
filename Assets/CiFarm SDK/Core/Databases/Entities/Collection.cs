using System;
using Newtonsoft.Json;

namespace CiFarm.Core.Databases
{
    [Serializable] // Makes the class serializable for Unity
    public class CollectionEntity : UuidAbstractEntity
    {
        // Public property for collection
        [JsonProperty("collection")] // Custom JSON property name
        public string Collection { get; set; }

        // Public property for data (use a specific type instead of object, e.g., Dictionary or a custom class)
        [JsonProperty("data")]
        public object Data { get; set; }

        // Default constructor for Unity serialization
        public CollectionEntity()
        {
            // Initialize properties if needed
        }
    }

    // Example class for SpeedUpData
    public class SpeedUpData
    {
        [JsonProperty("time")]
        public double Time { get; set; }
    }
}

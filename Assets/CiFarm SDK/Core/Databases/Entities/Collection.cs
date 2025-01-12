using System;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.Core.Databases
{
    [Serializable] // Makes the class serializable for Unity
    public class CollectionEntity : UuidAbstractEntity
    {
        // Private backing field for collection
        [SerializeField] // Expose this field for Unity serialization
        private string _collection;

        // Public property with getter and setter
        [JsonProperty("collection")] // Custom JSON property name
        public string Collection
        {
            get => _collection;
            set => _collection = value;
        }

        // Private backing field for data (using a dictionary for JSONB equivalent)
        [SerializeField] // Expose this field for Unity serialization
        private object _data;

        // Public property with getter and setter
        [JsonProperty("data")] // Custom JSON property name
        public object Data
        {
            get => _data;
            set => _data = value;
        }

        // Constructor
        public CollectionEntity()
        {
            // Default constructor for Unity serialization.
        }
    }

    // Class for SpeedUpData
    public class SpeedUpData
    {
        [JsonProperty("time")]
        public double Time { get; set; }
    }
}

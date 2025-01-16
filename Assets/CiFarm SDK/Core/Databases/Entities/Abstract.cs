using System;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.XR.WSA;

namespace CiFarm.Core.Databases
{
    // Abstract Base Entity Class
    [Serializable] // Makes the class serializable
    public abstract class AbstractEntity
    {
        // Public property for CreatedAt
        [JsonProperty("created_at")] // Custom JSON property name
        public DateTime CreatedAt { get; set; }

        // Public property for UpdatedAt
        [JsonProperty("updated_at")] // Custom JSON property name
        public DateTime UpdatedAt { get; set; }
    }

    // Since guid cannot be serialized in Unity, we need to use string as ID
    // Abstract Entity with UUID (Guid) as ID
    [Serializable] // Makes this class serializable
    public abstract class UuidAbstractEntity : AbstractEntity
    {
        [SerializeField] // Serialize field for Unity
        private string _id; // Private backing field for Id

        // Public property for Id (string)
        [JsonProperty("id")] // Custom JSON property name
        public string Id
        {
            get => _id;
            set => _id = value;
        }
    }

    // Abstract Entity with String as ID (typically for GUIDs stored as strings)
    [Serializable] // Makes this class serializable
    public abstract class StringAbstractEntity : AbstractEntity
    {
        [SerializeField] // Serialize field for Unity
        private string _id; // Private backing field for Id

        // Public property for Id (string)
        [JsonProperty("id")] // Custom JSON property name
        public string Id
        {
            get => _id;
            set => _id = value;
        }
    }
}

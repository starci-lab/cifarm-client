using System;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.Core.Databases
{
    // Abstract Base Entity Class
    [Serializable] // Makes the class serializable
    public abstract class AbstractEntity
    {
        // Private backing field for CreatedAt
        [SerializeField] // Exposes the field for serialization in Unity (if applicable)
        private DateTime _createdAt;

        // Public property with getter and setter
        [JsonProperty("created_at")] // Custom JSON property name
        public DateTime CreatedAt
        {
            get => _createdAt;
            set => _createdAt = value;
        }

        // Private backing field for UpdatedAt
        [SerializeField] // Exposes the field for serialization in Unity (if applicable)
        private DateTime _updatedAt;

        // Public property with getter and setter
        [JsonProperty("updated_at")] // Custom JSON property name
        public DateTime UpdatedAt
        {
            get => _updatedAt;
            set => _updatedAt = value;
        }
    }

    // Abstract Entity with UUID (Guid) as ID
    [Serializable] // Makes this class serializable
    public abstract class UuidAbstractEntity : AbstractEntity
    {
        // Private backing field for Id
        [SerializeField] // Exposes the field for serialization in Unity (if applicable)
        private Guid _id;

        // Public property with getter and setter
        [JsonProperty("id")] // Custom JSON property name
        public Guid Id
        {
            get => _id;
            set => _id = value;
        }
    }

    // Abstract Entity with String as ID (typically for GUIDs stored as strings)
    [Serializable] // Makes this class serializable
    public abstract class StringAbstractEntity : AbstractEntity
    {
        // Private backing field for Id
        [SerializeField] // Exposes the field for serialization in Unity (if applicable)
        private string _id;

        // Public property with getter and setter
        [JsonProperty("id")] // Custom JSON property name
        public string Id
        {
            get => _id;
            set => _id = value;
        }
    }
}

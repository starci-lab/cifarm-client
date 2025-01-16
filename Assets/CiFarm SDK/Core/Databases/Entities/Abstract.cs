using System;
using Newtonsoft.Json;

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

    // Abstract Entity with UUID (Guid) as ID
    [Serializable] // Makes this class serializable
    public abstract class UuidAbstractEntity : AbstractEntity
    {
        // Public property for Id (Guid)
        [JsonProperty("id")] // Custom JSON property name
        public Guid Id { get; set; }
    }

    // Abstract Entity with String as ID (typically for GUIDs stored as strings)
    [Serializable] // Makes this class serializable
    public abstract class StringAbstractEntity : AbstractEntity
    {
        // Public property for Id (string)
        [JsonProperty("id")] // Custom JSON property name
        public string Id { get; set; }
    }
}

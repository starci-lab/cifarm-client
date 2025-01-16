using System;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.Core.Databases
{
    [Serializable] // Makes the class serializable for Unity
    public class InventoryEntity : UuidAbstractEntity
    {
        // Public properties with auto-implemented getters and setters

        [JsonProperty("quantity")] // Custom JSON property name in camelCase
        public int Quantity { get; set; } = 1;

        [JsonProperty("tokenId")] // Custom JSON property name in camelCase
        public string TokenId { get; set; }

        [JsonProperty("premium")] // Custom JSON property name in camelCase
        public bool Premium { get; set; } = false;

        [JsonProperty("isPlaced")] // Custom JSON property name in camelCase
        public bool IsPlaced { get; set; } = false;

        [JsonProperty("userId")] // Custom JSON property name in camelCase
        public string UserId { get; set; }

        // Navigation property for UserEntity (many-to-one relationship)
        [JsonProperty("user")] // Custom JSON property name in camelCase
        public UserEntity User { get; set; }

        [JsonProperty("inventoryTypeId")] // Custom JSON property name in camelCase
        public string InventoryTypeId { get; set; }

        // Navigation property for InventoryTypeEntity (many-to-one relationship)
        [JsonProperty("inventoryType")] // Custom JSON property name in camelCase
        public InventoryTypeEntity InventoryType { get; set; }
    }
}

using System;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.Core.Databases
{
    [Serializable] // Makes the class serializable for Unity
    public class DeliveringProductEntity : UuidAbstractEntity
    {
        // Public properties with auto-implemented getters and setters

        [JsonProperty("quantity")] // Custom JSON property name in camelCase
        public int Quantity { get; set; }

        [JsonProperty("index")] // Custom JSON property name in camelCase
        public int Index { get; set; }

        [JsonProperty("premium")] // Custom JSON property name in camelCase
        public bool Premium { get; set; }

        [JsonProperty("userId")] // Custom JSON property name in camelCase
        public string UserId { get; set; }

        // Navigation property for UserEntity (many-to-one relationship)
        [JsonProperty("user")] // Custom JSON property name in camelCase
        public UserEntity User { get; set; }

        [JsonProperty("productId")] // Custom JSON property name in camelCase
        public string ProductId { get; set; }

        // Navigation property for ProductEntity (many-to-one relationship)
        [JsonProperty("product")] // Custom JSON property name in camelCase
        public ProductEntity Product { get; set; }
    }
}

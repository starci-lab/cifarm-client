using System;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.Core.Databases
{
    [Serializable] // Makes the class serializable for Unity
    public class DeliveringProductEntity : UuidAbstractEntity
    {
        // Private backing field for quantity
        [SerializeField] // Expose this field for Unity serialization
        private int _quantity;

        // Public property for quantity
        [JsonProperty("quantity")] // Custom JSON property name in camelCase
        public int Quantity
        {
            get => _quantity;
            set => _quantity = value;
        }

        // Private backing field for index
        [SerializeField] // Expose this field for Unity serialization
        private int _index;

        // Public property for index
        [JsonProperty("index")] // Custom JSON property name in camelCase
        public int Index
        {
            get => _index;
            set => _index = value;
        }

        // Private backing field for premium
        [SerializeField] // Expose this field for Unity serialization
        private bool _premium;

        // Public property for premium
        [JsonProperty("premium")] // Custom JSON property name in camelCase
        public bool Premium
        {
            get => _premium;
            set => _premium = value;
        }

        // Private backing field for userId (nullable)
        [SerializeField] // Expose this field for Unity serialization
        private string _userId;

        // Public property for userId
        [JsonProperty("userId")] // Custom JSON property name in camelCase
        public string UserId
        {
            get => _userId;
            set => _userId = value;
        }

        // User navigation property (many-to-one relationship)
        [JsonProperty("user")] // Custom JSON property name in camelCase
        public UserEntity User { get; set; }

        // Private backing field for productId (nullable)
        [SerializeField] // Expose this field for Unity serialization
        private string _productId;

        // Public property for productId
        [JsonProperty("productId")] // Custom JSON property name in camelCase
        public string ProductId
        {
            get => _productId;
            set => _productId = value;
        }

        // Product navigation property (many-to-one relationship)
        [JsonProperty("product")] // Custom JSON property name in camelCase
        public ProductEntity Product { get; set; }
    }
}

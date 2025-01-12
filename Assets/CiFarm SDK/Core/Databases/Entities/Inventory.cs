using System;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.Core.Databases
{
    [Serializable] // Makes the class serializable for Unity
    public class InventoryEntity : UuidAbstractEntity
    {
        // Private backing field for quantity
        [SerializeField] // Expose this field for Unity serialization
        private int _quantity = 1;

        // Public property for quantity
        [JsonProperty("quantity")] // Custom JSON property name in camelCase
        public int Quantity
        {
            get => _quantity;
            set => _quantity = value;
        }

        // Private backing field for tokenId (nullable)
        [SerializeField] // Expose this field for Unity serialization
        private string _tokenId;

        // Public property for tokenId (nullable)
        [JsonProperty("tokenId")] // Custom JSON property name in camelCase
        public string TokenId
        {
            get => _tokenId;
            set => _tokenId = value;
        }

        // Private backing field for premium
        [SerializeField] // Expose this field for Unity serialization
        private bool _premium = false;

        // Public property for premium
        [JsonProperty("premium")] // Custom JSON property name in camelCase
        public bool Premium
        {
            get => _premium;
            set => _premium = value;
        }

        // Private backing field for isPlaced
        [SerializeField] // Expose this field for Unity serialization
        private bool _isPlaced = false;

        // Public property for isPlaced
        [JsonProperty("isPlaced")] // Custom JSON property name in camelCase
        public bool IsPlaced
        {
            get => _isPlaced;
            set => _isPlaced = value;
        }

        // Private backing field for userId (nullable)
        [SerializeField] // Expose this field for Unity serialization
        private string _userId;

        // Public property for userId (nullable)
        [JsonProperty("userId")] // Custom JSON property name in camelCase
        public string UserId
        {
            get => _userId;
            set => _userId = value;
        }

        // Navigation property for UserEntity (many-to-one relationship)
        [JsonProperty("user")] // Custom JSON property name in camelCase
        public UserEntity User { get; set; }

        // Private backing field for inventoryTypeId (nullable)
        [SerializeField] // Expose this field for Unity serialization
        private string _inventoryTypeId;

        // Public property for inventoryTypeId (nullable)
        [JsonProperty("inventoryTypeId")] // Custom JSON property name in camelCase
        public string InventoryTypeId
        {
            get => _inventoryTypeId;
            set => _inventoryTypeId = value;
        }

        // Navigation property for InventoryTypeEntity (many-to-one relationship)
        [JsonProperty("inventoryType")] // Custom JSON property name in camelCase
        public InventoryTypeEntity InventoryType { get; set; }
    }
}

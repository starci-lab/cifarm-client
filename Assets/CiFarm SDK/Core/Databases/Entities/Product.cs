using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.Core.Databases
{
    [Serializable] // Makes the class serializable for Unity
    public class ProductEntity : StringAbstractEntity
    {
        // Private backing field for isPremium
        [SerializeField] // Expose this field for Unity serialization
        private bool _isPremium;

        // Public property for isPremium
        [JsonProperty("isPremium")] // Custom JSON property name in camelCase
        public bool IsPremium
        {
            get => _isPremium;
            set => _isPremium = value;
        }

        // Private backing field for goldAmount
        [SerializeField] // Expose this field for Unity serialization
        private int _goldAmount;

        // Public property for goldAmount
        [JsonProperty("goldAmount")] // Custom JSON property name in camelCase
        public int GoldAmount
        {
            get => _goldAmount;
            set => _goldAmount = value;
        }

        // Private backing field for tokenAmount
        [SerializeField] // Expose this field for Unity serialization
        private float _tokenAmount;

        // Public property for tokenAmount
        [JsonProperty("tokenAmount")] // Custom JSON property name in camelCase
        public float TokenAmount
        {
            get => _tokenAmount;
            set => _tokenAmount = value;
        }

        // Private backing field for type (enum)
        [SerializeField] // Expose this field for Unity serialization
        private ProductType _type;

        // Public property for type (enum)
        [JsonProperty("type")] // Custom JSON property name in camelCase
        public ProductType Type
        {
            get => _type;
            set => _type = value;
        }

        // Private backing field for cropId (nullable)
        [SerializeField] // Expose this field for Unity serialization
        private string _cropId;

        // Public property for cropId (nullable)
        [JsonProperty("cropId")] // Custom JSON property name in camelCase
        public string CropId
        {
            get => _cropId;
            set => _cropId = value;
        }

        // Navigation property for CropEntity (one-to-one relationship)
        [JsonProperty("crop")] // Custom JSON property name in camelCase
        public CropEntity Crop { get; set; }

        // Private backing field for animalId (nullable)
        [SerializeField] // Expose this field for Unity serialization
        private string _animalId;

        // Public property for animalId (nullable)
        [JsonProperty("animalId")] // Custom JSON property name in camelCase
        public string AnimalId
        {
            get => _animalId;
            set => _animalId = value;
        }

        // Navigation property for AnimalEntity (one-to-one relationship)
        [JsonProperty("animal")] // Custom JSON property name in camelCase
        public AnimalEntity Animal { get; set; }

        // Navigation property for InventoryTypeEntity (one-to-one relationship)
        [JsonProperty("inventoryType")] // Custom JSON property name in camelCase
        public InventoryTypeEntity InventoryType { get; set; }

        // Private backing field for deliveringProducts (one-to-many relationship)
        [SerializeField] // Expose this field for Unity serialization
        private List<DeliveringProductEntity> _deliveringProducts;

        // Public property for deliveringProducts (one-to-many relationship)
        [JsonProperty("deliveringProducts")] // Custom JSON property name in camelCase
        public List<DeliveringProductEntity> DeliveringProducts
        {
            get => _deliveringProducts;
            set => _deliveringProducts = value;
        }
    }
}

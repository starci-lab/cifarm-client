using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CiFarm.Core.Databases
{
    [Serializable] // Makes the class serializable for Unity
    public class ProductEntity : StringAbstractEntity
    {
        // Public property for isPremium
        [JsonProperty("isPremium")] // Custom JSON property name in camelCase
        public bool IsPremium { get; set; }

        // Public property for goldAmount
        [JsonProperty("goldAmount")] // Custom JSON property name in camelCase
        public int GoldAmount { get; set; }

        // Public property for tokenAmount
        [JsonProperty("tokenAmount")] // Custom JSON property name in camelCase
        public float TokenAmount { get; set; }

        // Public property for type (enum)
        [JsonProperty("type")] // Custom JSON property name in camelCase
        public ProductType Type { get; set; }

        // Public property for cropId (nullable)
        [JsonProperty("cropId")] // Custom JSON property name in camelCase
        public string CropId { get; set; }

        // Navigation property for CropEntity
        [JsonProperty("crop")] // Custom JSON property name in camelCase
        public CropEntity Crop { get; set; }

        // Public property for animalId (nullable)
        [JsonProperty("animalId")] // Custom JSON property name in camelCase
        public string AnimalId { get; set; }

        // Navigation property for AnimalEntity
        [JsonProperty("animal")] // Custom JSON property name in camelCase
        public AnimalEntity Animal { get; set; }

        // Navigation property for InventoryTypeEntity
        [JsonProperty("inventoryType")] // Custom JSON property name in camelCase
        public InventoryTypeEntity InventoryType { get; set; }

        // Public property for deliveringProducts (one-to-many relationship)
        [JsonProperty("deliveringProducts")] // Custom JSON property name in camelCase
        public List<DeliveringProductEntity> DeliveringProducts { get; set; }
    }
}

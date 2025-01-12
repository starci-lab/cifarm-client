using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.Core.Databases
{
    [Serializable] // Makes the class serializable for Unity
    public class BuildingEntity : UuidAbstractEntity
    {
        // Private backing field for availableInShop
        [SerializeField] // Expose this field for Unity serialization
        private bool _availableInShop;

        // Public property with getter and setter
        [JsonProperty("available_in_shop")] // Custom JSON property name
        public bool AvailableInShop
        {
            get => _availableInShop;
            set => _availableInShop = value;
        }

        // Private backing field for type (enum)
        [SerializeField] // Expose this field for Unity serialization
        private AnimalType? _type;

        // Public property with getter and setter
        [JsonProperty("type")] // Custom JSON property name
        public AnimalType? Type
        {
            get => _type;
            set => _type = value;
        }

        // Private backing field for maxUpgrade
        [SerializeField] // Expose this field for Unity serialization
        private int _maxUpgrade;

        // Public property with getter and setter
        [JsonProperty("max_upgrade")] // Custom JSON property name
        public int MaxUpgrade
        {
            get => _maxUpgrade;
            set => _maxUpgrade = value;
        }

        // Private backing field for price (nullable)
        [SerializeField] // Expose this field for Unity serialization
        private int? _price;

        // Public property with getter and setter
        [JsonProperty("price")] // Custom JSON property name
        public int? Price
        {
            get => _price;
            set => _price = value;
        }

        // List of upgrades (One-to-many relationship) - No initialization here
        [JsonProperty("upgrades")] // Custom JSON property name
        public List<UpgradeEntity> Upgrades { get; set; }

        // Private backing field for placedItemTypeId
        [SerializeField] // Expose this field for Unity serialization
        private string _placedItemTypeId;

        // Public property with getter and setter
        [JsonProperty("placed_item_type_id")] // Custom JSON property name
        public string PlacedItemTypeId
        {
            get => _placedItemTypeId;
            set => _placedItemTypeId = value;
        }

        // Placed item type (One-to-one relationship) - No initialization here
        [JsonProperty("placed_item_type")] // Custom JSON property name
        public PlacedItemTypeEntity PlacedItemType { get; set; }
    }
}

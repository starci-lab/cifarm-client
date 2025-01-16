using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CiFarm.Core.Databases
{
    [Serializable] // Makes the class serializable for Unity
    public class BuildingEntity : UuidAbstractEntity
    {
        // Public property for availableInShop
        [JsonProperty("available_in_shop")] // Custom JSON property name
        public bool AvailableInShop { get; set; }

        // Public property for type (enum)
        [JsonProperty("type")] // Custom JSON property name
        public AnimalType? Type { get; set; }

        // Public property for maxUpgrade
        [JsonProperty("max_upgrade")] // Custom JSON property name
        public int MaxUpgrade { get; set; }

        // Public property for price (nullable)
        [JsonProperty("price")] // Custom JSON property name
        public int? Price { get; set; }

        // List of upgrades (One-to-many relationship)
        [JsonProperty("upgrades")] // Custom JSON property name
        public List<UpgradeEntity> Upgrades { get; set; }

        // Public property for placedItemTypeId
        [JsonProperty("placed_item_type_id")] // Custom JSON property name
        public string PlacedItemTypeId { get; set; }

        // Placed item type (One-to-one relationship)
        [JsonProperty("placed_item_type")] // Custom JSON property name
        public PlacedItemTypeEntity PlacedItemType { get; set; }
    }
}

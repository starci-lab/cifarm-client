using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.Core.Databases
{
    [Serializable] // Makes the class serializable for Unity
    public class SpinPrizeEntity : UuidAbstractEntity
    {
        // Private backing field for type
        [SerializeField] // Expose this field for Unity serialization
        private SpinPrizeType _type;

        // Public property for type
        [JsonProperty("type")] // Custom JSON property name in camelCase
        public SpinPrizeType Type
        {
            get => _type;
            set => _type = value;
        }

        // Private backing field for cropId
        [SerializeField] // Expose this field for Unity serialization
        private string _cropId;

        // Public property for cropId
        [JsonProperty("cropId")] // Custom JSON property name in camelCase
        public string CropId
        {
            get => _cropId;
            set => _cropId = value;
        }

        // Navigation property for CropEntity
        [JsonProperty("crop")] // Custom JSON property name in camelCase
        public CropEntity Crop { get; set; }

        // Private backing field for supplyId
        [SerializeField] // Expose this field for Unity serialization
        private string _supplyId;

        // Public property for supplyId
        [JsonProperty("supplyId")] // Custom JSON property name in camelCase
        public string SupplyId
        {
            get => _supplyId;
            set => _supplyId = value;
        }

        // Navigation property for SupplyEntity
        [JsonProperty("supply")] // Custom JSON property name in camelCase
        public SupplyEntity Supply { get; set; }

        // Private backing field for golds
        [SerializeField] // Expose this field for Unity serialization
        private int? _golds;

        // Public property for golds
        [JsonProperty("golds")] // Custom JSON property name in camelCase
        public int? Golds
        {
            get => _golds;
            set => _golds = value;
        }

        // Private backing field for tokens
        [SerializeField] // Expose this field for Unity serialization
        private float? _tokens;

        // Public property for tokens
        [JsonProperty("tokens")] // Custom JSON property name in camelCase
        public float? Tokens
        {
            get => _tokens;
            set => _tokens = value;
        }

        // Private backing field for quantity
        [SerializeField] // Expose this field for Unity serialization
        private int? _quantity;

        // Public property for quantity
        [JsonProperty("quantity")] // Custom JSON property name in camelCase
        public int? Quantity
        {
            get => _quantity;
            set => _quantity = value;
        }

        // Private backing field for appearanceChance
        [SerializeField] // Expose this field for Unity serialization
        private AppearanceChance _appearanceChance;

        // Public property for appearanceChance
        [JsonProperty("appearanceChance")] // Custom JSON property name in camelCase
        public AppearanceChance AppearanceChance
        {
            get => _appearanceChance;
            set => _appearanceChance = value;
        }

        // Navigation property for SpinSlotEntity
        [JsonProperty("spinSlots")] // Custom JSON property name in camelCase
        public List<SpinSlotEntity> SpinSlots { get; set; } = new List<SpinSlotEntity>();
    }
}

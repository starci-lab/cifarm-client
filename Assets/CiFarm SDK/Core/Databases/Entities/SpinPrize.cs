using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.Core.Databases
{
    [Serializable] // Makes the class serializable for Unity
    public class SpinPrizeEntity : UuidAbstractEntity
    {
        // Private field for Type
        [SerializeField]
        private SpinPrizeType _type;

        // Private field for CropId (nullable)
        [SerializeField]
        private string _cropId;

        // Private field for SupplyId (nullable)
        [SerializeField]
        private string _supplyId;

        // Private field for Golds (nullable int)
        [SerializeField]
        private int? _golds;

        // Private field for Tokens (nullable float)
        [SerializeField]
        private float? _tokens;

        // Private field for Quantity (nullable int)
        [SerializeField]
        private int? _quantity;

        // Private field for AppearanceChance
        [SerializeField]
        private AppearanceChance _appearanceChance;

        // Private field for SpinSlots
        [SerializeField]
        private List<string> _spinSlotIds;

        // Public property for Type
        [JsonProperty("type")]
        public SpinPrizeType Type
        {
            get => _type;
            set => _type = value;
        }

        // Public property for CropId
        [JsonProperty("cropId")]
        public string CropId
        {
            get => _cropId;
            set => _cropId = value;
        }

        // Public property for SupplyId
        [JsonProperty("supplyId")]
        public string SupplyId
        {
            get => _supplyId;
            set => _supplyId = value;
        }

        // Public property for Golds
        [JsonProperty("golds")]
        public int? Golds
        {
            get => _golds;
            set => _golds = value;
        }

        // Public property for Tokens
        [JsonProperty("tokens")]
        public float? Tokens
        {
            get => _tokens;
            set => _tokens = value;
        }

        // Public property for Quantity
        [JsonProperty("quantity")]
        public int? Quantity
        {
            get => _quantity;
            set => _quantity = value;
        }

        // Public property for AppearanceChance
        [JsonProperty("appearanceChance")]
        public AppearanceChance AppearanceChance
        {
            get => _appearanceChance;
            set => _appearanceChance = value;
        }

        // Public property for SpinSlots
        [JsonProperty("spinSlots")]
        public List<string> SpinSlotIds
        {
            get => _spinSlotIds;
            set => _spinSlotIds = value;
        }
    }
}

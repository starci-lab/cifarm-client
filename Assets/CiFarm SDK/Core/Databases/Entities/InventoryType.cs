using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.Core.Databases
{
    [Serializable] // Makes the class serializable for Unity
    public class InventoryTypeEntity : UuidAbstractEntity
    {
        // Private backing fields with SerializeField for Unity serialization

        [SerializeField]
        private InventoryType _type;

        [SerializeField]
        private bool _placeable = false;

        [SerializeField]
        private bool _deliverable = false;

        [SerializeField]
        private bool _asTool = false;

        [SerializeField]
        private int _maxStack = 16;

        [SerializeField]
        private string _cropId;

        [SerializeField]
        private string _animalId;

        [SerializeField]
        private string _supplyId;

        [SerializeField]
        private string _productId;

        [SerializeField]
        private string _tileId;

        // Public properties with getters and setters

        [JsonProperty("type")]
        public InventoryType Type
        {
            get => _type;
            set => _type = value;
        }

        [JsonProperty("placeable")]
        public bool Placeable
        {
            get => _placeable;
            set => _placeable = value;
        }

        [JsonProperty("deliverable")]
        public bool Deliverable
        {
            get => _deliverable;
            set => _deliverable = value;
        }

        [JsonProperty("asTool")]
        public bool AsTool
        {
            get => _asTool;
            set => _asTool = value;
        }

        [JsonProperty("maxStack")]
        public int MaxStack
        {
            get => _maxStack;
            set => _maxStack = value;
        }

        [JsonProperty("cropId")]
        public string CropId
        {
            get => _cropId;
            set => _cropId = value;
        }

        [JsonProperty("animalId")]
        public string AnimalId
        {
            get => _animalId;
            set => _animalId = value;
        }

        [JsonProperty("supplyId")]
        public string SupplyId
        {
            get => _supplyId;
            set => _supplyId = value;
        }

        [JsonProperty("productId")]
        public string ProductId
        {
            get => _productId;
            set => _productId = value;
        }

        [JsonProperty("tileId")]
        public string TileId
        {
            get => _tileId;
            set => _tileId = value;
        }
    }
}

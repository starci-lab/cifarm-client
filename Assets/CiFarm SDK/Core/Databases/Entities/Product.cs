using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.Core.Databases
{
    [Serializable] // Makes the class serializable for Unity
    public class ProductEntity : StringAbstractEntity
    {
        // Private backing fields with SerializeField for Unity serialization

        [SerializeField]
        private bool _isPremium;

        [SerializeField]
        private int _goldAmount;

        [SerializeField]
        private float _tokenAmount;

        [SerializeField]
        private ProductType _type;

        [SerializeField]
        private string _cropId;

        [SerializeField]
        private string _animalId;

        [SerializeField]
        private string _inventoryTypeId;

        // Public properties with getters and setters

        [JsonProperty("isPremium")]
        public bool IsPremium
        {
            get => _isPremium;
            set => _isPremium = value;
        }

        [JsonProperty("goldAmount")]
        public int GoldAmount
        {
            get => _goldAmount;
            set => _goldAmount = value;
        }

        [JsonProperty("tokenAmount")]
        public float TokenAmount
        {
            get => _tokenAmount;
            set => _tokenAmount = value;
        }

        [JsonProperty("type")]
        public ProductType Type
        {
            get => _type;
            set => _type = value;
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

        [JsonProperty("inventoryTypeId")]
        public string InventoryTypeId
        {
            get => _inventoryTypeId;
            set => _inventoryTypeId = value;
        }
    }
}

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.Core.Databases
{
    [Serializable] // Makes the class serializable for Unity
    public class SupplyEntity : StringAbstractEntity
    {
        // Private backing fields with SerializeField for Unity serialization
        [SerializeField]
        private SupplyType _type;

        [SerializeField]
        private float _price;

        [SerializeField]
        private bool _availableInShop;

        [SerializeField]
        private int? _fertilizerEffectTimeReduce;

        [SerializeField]
        private string _inventoryTypeId;

        [SerializeField]
        private List<string> _spinPrizeIds;

        // Public properties with getters and setters
        [JsonProperty("type")] // Custom JSON property name
        public SupplyType Type
        {
            get => _type;
            set => _type = value;
        }

        [JsonProperty("price")] // Custom JSON property name
        public float Price
        {
            get => _price;
            set => _price = value;
        }

        [JsonProperty("availableInShop")] // Custom JSON property name
        public bool AvailableInShop
        {
            get => _availableInShop;
            set => _availableInShop = value;
        }

        [JsonProperty("fertilizerEffectTimeReduce")] // Custom JSON property name
        public int? FertilizerEffectTimeReduce
        {
            get => _fertilizerEffectTimeReduce;
            set => _fertilizerEffectTimeReduce = value;
        }

        [JsonProperty("inventoryTypeId")] // Custom JSON property name
        public string InventoryTypeId
        {
            get => _inventoryTypeId;
            set => _inventoryTypeId = value;
        }

        [JsonProperty("spinPrizeIds")] // Custom JSON property name
        public List<string> SpinPrizes
        {
            get => _spinPrizeIds;
            set => _spinPrizeIds = value;
        }
    }
}

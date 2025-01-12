using System;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.RestApi
{
    [Serializable]
    public class DeliverProductRequest
    {
        [SerializeField]
        private int _index;

        [JsonProperty("index")]
        public int Index
        {
            get => _index;
            set => _index = value;
        }

        [SerializeField]
        private string _inventoryId;

        [JsonProperty("inventoryId")]
        public string InventoryId
        {
            get => _inventoryId;
            set => _inventoryId = value;
        }

        [SerializeField]
        private int _quantity;

        [JsonProperty("quantity")]
        public int Quantity
        {
            get => _quantity;
            set => _quantity = value;
        }
    }

    [Serializable]
    public class DeliverProductResponse { }
}

using System;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.RestApi
{
    [Serializable]
    public class BuySuppliesRequest
    {
        [SerializeField]
        private string _supplyId;

        [JsonProperty("supplyId")]
        public string SupplyId
        {
            get => _supplyId;
            set => _supplyId = value;
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
    public class BuySuppliesResponse { }
}

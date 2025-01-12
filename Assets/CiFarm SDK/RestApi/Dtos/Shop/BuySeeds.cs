using System;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.RestApi
{
    [Serializable]
    public class BuySeedsRequest
    {
        [SerializeField]
        private string _cropId;

        [JsonProperty("cropId")]
        public string CropId
        {
            get => _cropId;
            set => _cropId = value;
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
    public class BuySeedsResponse { }
}

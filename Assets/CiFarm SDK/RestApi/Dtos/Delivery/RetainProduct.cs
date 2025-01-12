using System;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.RestApi
{
    public class RetainProductRequest
    {
        [SerializeField]
        private string _deliveringProductId;

        [JsonProperty("deliveringProductId")]
        public string DeliveringProductId
        {
            get => _deliveringProductId;
            set => _deliveringProductId = value;
        }
    }

    [Serializable]
    public class RetainProductResponse { }
}

using System;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.RestApi
{
    [Serializable]
    public class ThiefAnimalProductRequest : NeighborAndUserIdRequest
    {
        [SerializeField]
        private string _placedItemAnimalId;

        [JsonProperty("placedItemAnimalId")]
        public string PlacedItemAnimalId
        {
            get => _placedItemAnimalId;
            set => _placedItemAnimalId = value;
        }
    }

    [Serializable]
    public class ThiefAnimalProductResponse
    {
        [SerializeField]
        private int _quantity;

        [JsonProperty("quantity")]
        public int Quantity
        {
            get => _quantity;
            set => _quantity = value;
        }
    }
}

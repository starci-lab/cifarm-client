using System;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.RestApi
{
    [Serializable]
    public class CollectAnimalProductRequest
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
    public class CollectAnimalProductResponse { }
}

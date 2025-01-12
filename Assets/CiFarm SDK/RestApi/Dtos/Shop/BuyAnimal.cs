using System;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.RestApi
{
    [Serializable]
    public class BuyAnimalRequest
    {
        [SerializeField]
        private string _animalId;

        [JsonProperty("animalId")]
        public string AnimalId
        {
            get => _animalId;
            set => _animalId = value;
        }

        [SerializeField]
        private string _placedItemBuildingId;

        [JsonProperty("placedItemBuildingId")]
        public string PlacedItemBuildingId
        {
            get => _placedItemBuildingId;
            set => _placedItemBuildingId = value;
        }

        [SerializeField]
        private Position _position;

        [JsonProperty("position")]
        public Position Position
        {
            get => _position;
            set => _position = value;
        }
    }

    [Serializable]
    public class BuyAnimalResponse { }
}

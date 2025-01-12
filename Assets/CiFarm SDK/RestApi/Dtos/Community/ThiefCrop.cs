using System;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.RestApi
{
    [Serializable]
    public class ThiefCropRequest : NeighborAndUserIdRequest
    {
        [SerializeField]
        private string _placedItemTileId;

        [JsonProperty("placedItemTileId")]
        public string PlacedItemTileId
        {
            get => _placedItemTileId;
            set => _placedItemTileId = value;
        }
    }

    [Serializable]
    public class ThiefCropResponse
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

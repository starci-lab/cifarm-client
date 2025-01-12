using System;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.RestApi
{
    [Serializable]
    public class UpgradeBuildingRequest
    {
        [SerializeField]
        private string _placedItemBuildingId;

        [JsonProperty("placedItemBuildingId")]
        public string PlacedItemId
        {
            get => _placedItemBuildingId;
            set => _placedItemBuildingId = value;
        }
    }

    [Serializable]
    public class UpgradeBuildingResponse { }
}

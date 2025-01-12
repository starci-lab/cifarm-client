using System;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.RestApi
{
    [Serializable]
    public class ConstructBuildingRequest
    {
        [SerializeField]
        private string _buildingId;

        [JsonProperty("buildingId")]
        public string BuildingId
        {
            get => _buildingId;
            set => _buildingId = value;
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
    public class ConstructBuildingResponse { }
}

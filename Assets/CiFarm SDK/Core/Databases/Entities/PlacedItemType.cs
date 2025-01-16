using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.Core.Databases
{
    [Serializable] // Makes the class serializable for Unity
    public class PlacedItemTypeEntity : StringAbstractEntity
    {
        // Private field for Type
        [SerializeField]
        private PlacedItemType _type;

        // Private field for TileId (nullable)
        [SerializeField]
        private string _tileId;

        // Private field for BuildingId (nullable)
        [SerializeField]
        private string _buildingId;

        // Private field for AnimalId (nullable)
        [SerializeField]
        private string _animalId;

        // Public property for Type
        [JsonProperty("type")]
        public PlacedItemType Type
        {
            get => _type;
            set => _type = value;
        }

        // Public property for TileId
        [JsonProperty("tileId")]
        public string TileId
        {
            get => _tileId;
            set => _tileId = value;
        }

        // Public property for BuildingId
        [JsonProperty("buildingId")]
        public string BuildingId
        {
            get => _buildingId;
            set => _buildingId = value;
        }

        // Public property for AnimalId
        [JsonProperty("animalId")]
        public string AnimalId
        {
            get => _animalId;
            set => _animalId = value;
        }
    }
}

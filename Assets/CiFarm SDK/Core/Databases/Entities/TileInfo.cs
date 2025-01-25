using System;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.Core.Databases
{
    // Represents a tile entity
    [Serializable]
    public class TileInfoEntity : UuidAbstractEntity
    {
        [JsonProperty("harvestCount")]
        [field: SerializeField]
        public int HarvestCount { get; set; }

        [JsonProperty("placedItemTypeId")]
        [field: SerializeField]
        public string PlacedItemTypeId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using CiFarm.Core.Databases;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.IO
{
    [Serializable]
    public class PlacedItemsSyncedMessage
    {
        [field: SerializeField]
        [JsonProperty("placedItems")]
        public List<PlacedItemEntity> PlacedItems { get; set; }
    }

    [Serializable]
    public class VisitPayload
    {
        [field: SerializeField]
        [JsonProperty("userId")]
        public string UserId { get; set; }
    }
}

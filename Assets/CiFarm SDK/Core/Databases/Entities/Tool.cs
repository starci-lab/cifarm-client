using System;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.Core.Databases
{
    // Represents a tool entity
    [Serializable]
    public class ToolEntity : StringAbstractEntity
    {
        // Public properties for ToolEntity without SerializeField attributes

        [JsonProperty("availableIn")]
        public AvailableInType AvailableIn { get; set; }

        [JsonProperty("index")]
        public int Index { get; set; }
    }
}

using System;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.Core.Databases
{
    // Represents a tool entity
    [Serializable]
    public class ToolEntity : StringAbstractEntity
    {
        // Private backing field for availableIn
        [SerializeField] // Unity serialization
        private AvailableInType _availableIn;

        [JsonProperty("availableIn")]
        public AvailableInType AvailableIn
        {
            get => _availableIn;
            set => _availableIn = value;
        }

        // Private backing field for index
        [SerializeField] // Unity serialization
        private int _index;

        [JsonProperty("index")]
        public int Index
        {
            get => _index;
            set => _index = value;
        }
    }
}

using System;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.Core.Databases
{
    // Represents a tool entity
    [Serializable]
    public class ToolEntity : StringAbstractEntity
    {
        // Private backing fields with SerializeField for Unity serialization

        [SerializeField]
        private AvailableInType _availableIn;

        [SerializeField]
        private int _index;

        // Public properties with getters and setters

        [JsonProperty("availableIn")]
        public AvailableInType AvailableIn
        {
            get => _availableIn;
            set => _availableIn = value;
        }

        [JsonProperty("index")]
        public int Index
        {
            get => _index;
            set => _index = value;
        }
    }
}

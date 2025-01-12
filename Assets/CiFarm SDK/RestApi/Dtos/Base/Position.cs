using System;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.RestApi
{
    [Serializable]
    public class Position
    {
        [SerializeField]
        private float _x;

        [JsonProperty("x")]
        public float X
        {
            get => _x;
            set => _x = value;
        }

        [SerializeField]
        private float _y;

        [JsonProperty("y")]
        public float Y
        {
            get => _y;
            set => _y = value;
        }
    }
}

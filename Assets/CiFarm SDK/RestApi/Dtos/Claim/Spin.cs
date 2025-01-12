using System;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.RestApi
{
    [Serializable]
    public class SpinRequest { }

    [Serializable]
    public class SpinResponse
    {
        [SerializeField]
        private string _spinSlotId;

        [JsonProperty("spinSlotId")]
        public string SpinSlotId
        {
            get => _spinSlotId;
            set => _spinSlotId = value;
        }
    }
}

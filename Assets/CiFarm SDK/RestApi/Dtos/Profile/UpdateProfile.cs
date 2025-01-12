using System;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.RestApi
{
    [Serializable]
    public class UpdateTutorialRequest
    {
        [SerializeField]
        private int _tutorialIndex;

        [JsonProperty("tutorialIndex")]
        public int TutorialIndex
        {
            get => _tutorialIndex;
            set => _tutorialIndex = value;
        }

        [SerializeField]
        private int _stepIndex;

        [JsonProperty("stepIndex")]
        public int StepIndex
        {
            get => _stepIndex;
            set => _stepIndex = value;
        }
    }

    [Serializable]
    public class UpdateTutorialResponse { }
}

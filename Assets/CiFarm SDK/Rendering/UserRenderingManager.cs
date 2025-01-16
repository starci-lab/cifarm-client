using System;
using System.Collections;
using CiFarm.Core.Credentials;
using Imba.Utils;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm
{
    /// <summary>
    /// Manager for fetching and rendering user data, easier to manage and maintain
    /// </summary>
    public class UserRenderingManager : ManualSingletonMono<UserRenderingManager>
    {
        /// <summary>
        /// Renderable user entity for rendering user data
        /// </summary>
        [SerializeField]
        private InspectorUser _user;

        private IEnumerator Start()
        {
            // Wait until the user is authenticated
            yield return new WaitUntil(() => CiFarmSDK.Instance.Authenticated);

            // Query the user data
            QueryUserAsync();
        }

        /// <summary>
        /// Query the user data asynchronously
        /// </summary>
        private async void QueryUserAsync()
        {
            // Query the user data
            var user = await CiFarmSDK.Instance.GraphQLClient.QueryUserAsync();
            _user = new InspectorUser
            {
                Username = user.Username,
                ChainKey = user.ChainKey,
                Network = user.Network,
                AccountAddress = user.AccountAddress,
                Golds = user.Golds,
                Tokens = user.Tokens,
                Experiences = user.Experiences,
                Energy = user.Energy,
                EnergyRegenTime = user.EnergyRegenTime,
                Level = user.Level,
                TutorialIndex = user.TutorialIndex,
                StepIndex = user.StepIndex,
                DailyRewardStreak = user.DailyRewardStreak,
                DailyRewardLastClaimTime = user.DailyRewardLastClaimTime,
                DailyRewardNumberOfClaim = user.DailyRewardNumberOfClaim,
                SpinLastTime = user.SpinLastTime,
                SpinCount = user.SpinCount,
            };
        }
    }

    [Serializable]
    class InspectorUser
    {
        [SerializeField]
        private string _username;

        [JsonProperty("username")]
        public string Username
        {
            get => _username;
            set => _username = value;
        }

        [SerializeField]
        private SupportedChainKey _chainKey;

        [JsonProperty("chainKey")]
        public SupportedChainKey ChainKey
        {
            get => _chainKey;
            set => _chainKey = value;
        }

        [SerializeField]
        private Network _network;

        [JsonProperty("network")]
        public Network Network
        {
            get => _network;
            set => _network = value;
        }

        [SerializeField]
        private string _accountAddress;

        [JsonProperty("accountAddress")]
        public string AccountAddress
        {
            get => _accountAddress;
            set => _accountAddress = value;
        }

        [SerializeField]
        private int _golds;

        [JsonProperty("golds")]
        public int Golds
        {
            get => _golds;
            set => _golds = value;
        }

        [SerializeField]
        private float _tokens;

        [JsonProperty("tokens")]
        public float Tokens
        {
            get => _tokens;
            set => _tokens = value;
        }

        [SerializeField]
        private int _experiences;

        [JsonProperty("experiences")]
        public int Experiences
        {
            get => _experiences;
            set => _experiences = value;
        }

        [SerializeField]
        private int _energy;

        [JsonProperty("energy")]
        public int Energy
        {
            get => _energy;
            set => _energy = value;
        }

        [SerializeField]
        private float _energyRegenTime;

        [JsonProperty("energyRegenTime")]
        public float EnergyRegenTime
        {
            get => _energyRegenTime;
            set => _energyRegenTime = value;
        }

        [SerializeField]
        private int _level;

        [JsonProperty("level")]
        public int Level
        {
            get => _level;
            set => _level = value;
        }

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

        [SerializeField]
        private int _dailyRewardStreak;

        [JsonProperty("dailyRewardStreak")]
        public int DailyRewardStreak
        {
            get => _dailyRewardStreak;
            set => _dailyRewardStreak = value;
        }

        [SerializeField]
        private DateTime? _dailyRewardLastClaimTime;

        [JsonProperty("dailyRewardLastClaimTime")]
        public DateTime? DailyRewardLastClaimTime
        {
            get => _dailyRewardLastClaimTime;
            set => _dailyRewardLastClaimTime = value;
        }

        [SerializeField]
        private int _dailyRewardNumberOfClaim;

        [JsonProperty("dailyRewardNumberOfClaim")]
        public int DailyRewardNumberOfClaim
        {
            get => _dailyRewardNumberOfClaim;
            set => _dailyRewardNumberOfClaim = value;
        }

        [SerializeField]
        private DateTime? _spinLastTime;

        [JsonProperty("spinLastTime")]
        public DateTime? SpinLastTime
        {
            get => _spinLastTime;
            set => _spinLastTime = value;
        }

        [SerializeField]
        private int _spinCount;

        [JsonProperty("spinCount")]
        public int SpinCount
        {
            get => _spinCount;
            set => _spinCount = value;
        }
    }
}

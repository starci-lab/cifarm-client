using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.Core.Databases
{
    [Serializable]
    public class UserEntity : UuidAbstractEntity
    {
        // Private fields for UserEntity properties

        [SerializeField]
        private string _username;

        [JsonProperty("username")]
        public string Username
        {
            get => _username;
            set => _username = value;
        }

        [SerializeField]
        private string _chainKey;

        [JsonProperty("chainKey")]
        public string ChainKey
        {
            get => _chainKey;
            set => _chainKey = value;
        }

        [SerializeField]
        private string _network;

        [JsonProperty("network")]
        public string Network
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

        [SerializeField]
        private string _visitingUserId;

        [JsonProperty("visitingUserId")]
        public string VisitingUserId
        {
            get => _visitingUserId;
            set => _visitingUserId = value;
        }

        [SerializeField]
        private bool? _isRandom;

        [JsonProperty("isRandom")]
        public bool? IsRandom
        {
            get => _isRandom;
            set => _isRandom = value;
        }

        // Private backing fields for navigation properties
        [SerializeField]
        private UserEntity _visitingUser;

        [JsonProperty("visitingUser")]
        public UserEntity VisitingUser
        {
            get => _visitingUser;
            set => _visitingUser = value;
        }

        [SerializeField]
        private List<InventoryEntity> _inventories;

        [JsonProperty("inventories")]
        public List<InventoryEntity> Inventories
        {
            get => _inventories;
            set => _inventories = value;
        }

        [SerializeField]
        private List<PlacedItemEntity> _placedItems;

        [JsonProperty("placedItems")]
        public List<PlacedItemEntity> PlacedItems
        {
            get => _placedItems;
            set => _placedItems = value;
        }

        [SerializeField]
        private List<DeliveringProductEntity> _deliveringProducts;

        [JsonProperty("deliveringProducts")]
        public List<DeliveringProductEntity> DeliveringProducts
        {
            get => _deliveringProducts;
            set => _deliveringProducts = value;
        }

        [SerializeField]
        private List<UsersFollowingUsersEntity> _followingUsers;

        [JsonProperty("followingUsers")]
        public List<UsersFollowingUsersEntity> FollowingUsers
        {
            get => _followingUsers;
            set => _followingUsers = value;
        }

        [SerializeField]
        private List<UsersFollowingUsersEntity> _followedByUsers;

        [JsonProperty("followedByUsers")]
        public List<UsersFollowingUsersEntity> FollowedByUsers
        {
            get => _followedByUsers;
            set => _followedByUsers = value;
        }

        [SerializeField]
        private List<SessionEntity> _sessions;

        [JsonProperty("sessions")]
        public List<SessionEntity> Sessions
        {
            get => _sessions;
            set => _sessions = value;
        }
    }
}

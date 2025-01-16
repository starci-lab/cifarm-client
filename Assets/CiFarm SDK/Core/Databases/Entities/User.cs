using System;
using System.Collections.Generic;
using CiFarm.Core.Credentials;
using Newtonsoft.Json;

namespace CiFarm.Core.Databases
{
    [Serializable]
    public class UserEntity : UuidAbstractEntity
    {
        // Public properties for UserEntity without the SerializeField attribute

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("chainKey")]
        public SupportedChainKey ChainKey { get; set; }

        [JsonProperty("network")]
        public Network Network { get; set; }

        [JsonProperty("accountAddress")]
        public string AccountAddress { get; set; }

        [JsonProperty("golds")]
        public int Golds { get; set; }

        [JsonProperty("tokens")]
        public float Tokens { get; set; }

        [JsonProperty("experiences")]
        public int Experiences { get; set; }

        [JsonProperty("energy")]
        public int Energy { get; set; }

        [JsonProperty("energyRegenTime")]
        public float EnergyRegenTime { get; set; }

        [JsonProperty("level")]
        public int Level { get; set; }

        [JsonProperty("tutorialIndex")]
        public int TutorialIndex { get; set; }

        [JsonProperty("stepIndex")]
        public int StepIndex { get; set; }

        [JsonProperty("dailyRewardStreak")]
        public int DailyRewardStreak { get; set; }

        [JsonProperty("dailyRewardLastClaimTime")]
        public DateTime? DailyRewardLastClaimTime { get; set; }

        [JsonProperty("dailyRewardNumberOfClaim")]
        public int DailyRewardNumberOfClaim { get; set; }

        [JsonProperty("spinLastTime")]
        public DateTime? SpinLastTime { get; set; }

        [JsonProperty("spinCount")]
        public int SpinCount { get; set; }

        [JsonProperty("inventories")]
        public List<InventoryEntity> Inventories { get; set; }

        [JsonProperty("placedItems")]
        public List<PlacedItemEntity> PlacedItems { get; set; }

        [JsonProperty("deliveringProducts")]
        public List<DeliveringProductEntity> DeliveringProducts { get; set; }

        [JsonProperty("followingUsers")]
        public List<UsersFollowingUsersEntity> FollowingUsers { get; set; }

        [JsonProperty("followedByUsers")]
        public List<UsersFollowingUsersEntity> FollowedByUsers { get; set; }

        [JsonProperty("sessions")]
        public List<SessionEntity> Sessions { get; set; }
    }
}

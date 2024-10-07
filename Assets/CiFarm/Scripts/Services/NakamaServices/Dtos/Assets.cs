using Newtonsoft.Json;
using System;

namespace CiFarm.Scripts.Services.NakamaServices
{
    public enum InventoryType
    {
        Seed,
        Tile,
        Animal,
        PlantHarvested,
        Building,
    }

    [Serializable]
    public class Inventory
    {
        [JsonProperty("key")]
        public string key;

        [JsonProperty("referenceKey")]
        public string referenceKey;

        [JsonProperty("type")]
        public InventoryType type;

        [JsonProperty("quantity")]
        public int quantity;

        [JsonProperty("isPremium")]
        public bool isPremium;  
        
        [JsonProperty("unique")]
        public bool unique;
        
        [JsonProperty("tokenId")]
        public string tokenId;
    }

    [Serializable]
    public class Wallet
    {
        [JsonProperty("golds")]
        public int golds;
    }


    [Serializable]
    public class TelegramData
    {
        [JsonProperty("userId")]
        public int userId;
    }


    [Serializable]
    public class Metadata
    {
        [JsonProperty("key")]
        public string key;

        [JsonProperty("accountAddress")]
        public string accountAddress;

        [JsonProperty("chainKey")]
        public string chainKey;

        [JsonProperty("network")]
        public string network;

        [JsonProperty("telegramData")]
        public TelegramData telegramData;

    }

    [Serializable]
    public class PlayerStats
    {
        [JsonProperty("key")]
        public string key;

        [JsonProperty("experiences")]
        public long experiences;

        [JsonProperty("experienceQuota")]
        public long experienceQuota;

        [JsonProperty("level")]
        public int level;

        [JsonProperty("tutorialIndex")]
        public int tutorialIndex;

        [JsonProperty("stepIndex")]
        public int stepIndex;
    }

    [Serializable]
    public class VisitState
    {
        [JsonProperty("key")]
        public string key;

        [JsonProperty("userId")]
        public string userId;
    }
}
using Newtonsoft.Json;
using System;

namespace CiFarm.Scripts.Services.NakamaServices
{
    public enum InventoryType
    {
        Seed,
        Tile,
        Animal,
        PlantHarvested
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
    }

    [Serializable]
    public class Wallet
    {
        [JsonProperty("golds")]
        public int golds;
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
    }
}
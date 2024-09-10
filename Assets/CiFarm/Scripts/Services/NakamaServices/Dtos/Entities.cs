using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CiFarm.Scripts.Services.NakamaServices
{
    [Serializable]
    public class Seed
    {
        [JsonProperty("key")]
        public string key;

        [JsonProperty("price")]
        public int price;

        [JsonProperty("premium")]
        public bool premium;

        [JsonProperty("perennial")]
        public bool perennial;

        [JsonProperty("referenceId")]
        public string referenceId;

        [JsonProperty("growthStages")]
        public int growthStages;

        [JsonProperty("maxHarvestQuantity")]
        public int maxHarvestQuantity;

        [JsonProperty("minHarvestQuantity")]
        public int minHarvestQuantity;

        [JsonProperty("growthStageDuration")]
        public int growthStageDuration;

        [JsonProperty("nextGrowthStageAfterHarvest")]
        public int nextGrowthStageAfterHarvest;
    }

    [Serializable]
    public class Tile
    {
        [JsonProperty("key")]
        public string key;

        [JsonProperty("price")]
        public int Price { get; set; }

        [JsonProperty("referenceId")]
        public string ReferenceId { get; set; }

        [JsonProperty("maxOwnership")]
        public int MaxOwnership { get; set; }
    }

    [Serializable]
    public class Animal
    {
        [JsonProperty("key")]
        public string key;

        [JsonProperty("premium")]
        public bool premium;

        [JsonProperty("yieldTime")]
        public int yieldTime;

        [JsonProperty("growthTime")]
        public int growthTime;

        [JsonProperty("referenceId")]
        public string referenceId;

        [JsonProperty("offspringPrice")]
        public int offspringPrice;
    }

    [Serializable]
    public class CentralMatchInfo
    {
        [JsonProperty("matchId")]
        public string matchId;
    }


    [Serializable]
    public class Position
    {
        [JsonProperty("x")]
        public float x;

        [JsonProperty("y")]
        public float y;
    }

    [Serializable]
    public class SeedGrowthInfo
    {
        [JsonProperty("seed")]
        public Seed seed; 

        [JsonProperty("isWeedy")]
        public bool isWeedy; 

        [JsonProperty("isInfested")]
        public bool isInfested;

        [JsonProperty("currentStage")]
        public int currentStage; 

        [JsonProperty("totalTimeElapsed")]
        public float totalTimeElapsed; 

        [JsonProperty("currentStageTimeElapsed")]
        public float currentStageTimeElapsed; 

        [JsonProperty("harvestQuantityRemaining")]
        public int harvestQuantityRemaining; 
    }

    [Serializable]
    public class PlacedItem
    {
        [JsonProperty("type")]
        public int type;

        [JsonProperty("position")]
        public Position position;

        [JsonProperty("isPlanted")]
        public bool isPlanted;

        [JsonProperty("referenceId")]
        public string referenceId;

        [JsonProperty("fullyMatured")]
        public bool fullyMatured;

        [JsonProperty("seedGrowthInfo")]
        public SeedGrowthInfo seedGrowthInfo;
    }

    [Serializable]
    public class PlacedItems
    {
        [JsonProperty("placedItems")]
        public List<PlacedItem> placedItems;
    }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CiFarm.Scripts.Services.NakamaServices
{   
    [Serializable]
    public class Crop
    {
        [JsonProperty("key")]
        public string key;

        [JsonProperty("price")]
        public int price;

        [JsonProperty("premium")]
        public bool premium;

        [JsonProperty("perennial")]
        public bool perennial;

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

        [JsonProperty("offspringPrice")]
        public int offspringPrice;
    }

    [Serializable]
    public class MatchInfo
    {
        [JsonProperty("centralMatchId")]
        public string centralMatchId;

        [JsonProperty("timerMatchId")]
        public string timerMatchId;
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
        [JsonProperty("crop")]
        public Crop crop; 

        [JsonProperty("currentStage")]
        public int currentStage; 

        [JsonProperty("totalTimeElapsed")]
        public float totalTimeElapsed; 

        [JsonProperty("currentStageTimeElapsed")]
        public float currentStageTimeElapsed; 

        [JsonProperty("harvestQuantityRemaining")]
        public int harvestQuantityRemaining; 
        
        [JsonProperty("plantCurrentState")]
        public PlantCurrentState plantCurrentState;

        [JsonProperty("thiefedBy")]
        public List<string> thiefedBy;
    }
    public enum PlantCurrentState
    {
        Normal,
        NeedWater,
        IsWeedy,
        IsInfested,
    }
    
    public enum PlacedItemType
    {
        Tile,
        Building
    }

    [Serializable]
    public class PlacedItem
    {
        [JsonProperty("key")]
        public string key;

        [JsonProperty("type")]
        public PlacedItemType type;

        [JsonProperty("position")]
        public Position position;

        [JsonProperty("isPlanted")]
        public bool isPlanted;

        [JsonProperty("referenceKey")]
        public string referenceKey;

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


    [Serializable]
    public class NextDeliveryTime
    {
        [JsonProperty("time")]
        public long time;
    }


    [Serializable]
    public class DeliveringProduct
    {
        [JsonProperty("key")]
        public string key;

        [JsonProperty("referenceKey")]
        public string referenceKey;

        [JsonProperty("type")]
        public int type;

        [JsonProperty("quantity")]
        public int quantity;

        [JsonProperty("isPremium")]
        public bool isPremium;
        
        [JsonProperty("index")]
        public int index;
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
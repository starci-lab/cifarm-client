using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CiFarm.Core.Databases
{
    // Represents the System entity
    [Serializable]
    public class SystemEntity : StringAbstractEntity
    {
        // Public property for value
        [JsonProperty("value")]
        public string Value { get; set; }
    }

    // Represents different activities
    [Serializable]
    public class Activities
    {
        [JsonProperty("water")]
        public ActivityInfo Water { get; set; }

        [JsonProperty("feedAnimal")]
        public ActivityInfo FeedAnimal { get; set; }

        [JsonProperty("usePesticide")]
        public ActivityInfo UsePesticide { get; set; }

        [JsonProperty("useFertilizer")]
        public ActivityInfo UseFertilizer { get; set; }

        [JsonProperty("useHerbicide")]
        public ActivityInfo UseHerbicide { get; set; }

        [JsonProperty("helpUseHerbicide")]
        public ActivityInfo HelpUseHerbicide { get; set; }

        [JsonProperty("helpUsePesticide")]
        public ActivityInfo HelpUsePesticide { get; set; }

        [JsonProperty("helpWater")]
        public ActivityInfo HelpWater { get; set; }

        [JsonProperty("thiefCrop")]
        public ActivityInfo ThiefCrop { get; set; }

        [JsonProperty("thiefAnimalProduct")]
        public ActivityInfo ThiefAnimalProduct { get; set; }

        [JsonProperty("cureAnimal")]
        public ActivityInfo CureAnimal { get; set; }

        [JsonProperty("helpCureAnimal")]
        public ActivityInfo HelpCureAnimal { get; set; }

        [JsonProperty("harvestCrop")]
        public ActivityInfo HarvestCrop { get; set; }
    }

    // Represents activity info containing experience gained and energy consumed
    [Serializable]
    public class ActivityInfo
    {
        [JsonProperty("experiencesGain")]
        public int ExperiencesGain { get; set; }

        [JsonProperty("energyConsume")]
        public int EnergyConsume { get; set; }
    }

    // Represents crop randomness values
    [Serializable]
    public class CropRandomness
    {
        [JsonProperty("thief3")]
        public int Thief3 { get; set; }

        [JsonProperty("thief2")]
        public int Thief2 { get; set; }

        [JsonProperty("needWater")]
        public int NeedWater { get; set; }

        [JsonProperty("isWeedyOrInfested")]
        public int IsWeedyOrInfested { get; set; }
    }

    // Represents animal randomness values
    [Serializable]
    public class AnimalRandomness
    {
        [JsonProperty("sickChance")]
        public int SickChance { get; set; }

        [JsonProperty("thief3")]
        public int Thief3 { get; set; }

        [JsonProperty("thief2")]
        public int Thief2 { get; set; }
    }

    // Represents positions such as starter tiles and home
    [Serializable]
    public class Positions
    {
        [JsonProperty("tiles")]
        public List<Position> Tiles { get; set; }

        [JsonProperty("home")]
        public Position Home { get; set; }
    }

    // Represents the starter info with golds and positions
    [Serializable]
    public class Starter
    {
        [JsonProperty("golds")]
        public int Golds { get; set; }

        [JsonProperty("positions")]
        public Positions Positions { get; set; }
    }

    // Represents spin info, including appearance chance slots
    [Serializable]
    public class SpinInfo
    {
        [JsonProperty("appearanceChanceSlots")]
        public Dictionary<AppearanceChance, SlotInfo> AppearanceChanceSlots { get; set; }
    }

    // Represents slot information in a spin
    [Serializable]
    public class SlotInfo
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("thresholdMin")]
        public int ThresholdMin { get; set; }

        [JsonProperty("thresholdMax")]
        public int ThresholdMax { get; set; }
    }

    // Represents energy regeneration time in milliseconds
    [Serializable]
    public class EnergyRegenTime
    {
        [JsonProperty("time")]
        public int Time { get; set; }
    }

    // Represents Position data structure (placeholder class)
    [Serializable]
    public class Position
    {
        [JsonProperty("x")]
        public int X { get; set; }

        [JsonProperty("y")]
        public int Y { get; set; }
    }
}

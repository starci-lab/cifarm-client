using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.Core.Databases
{
    // Represents the System entity
    [Serializable]
    public class SystemEntity : StringAbstractEntity
    {
        // Private backing field for value
        [SerializeField]
        private string _value;

        // Public property for value
        [JsonProperty("value")]
        public string Value
        {
            get => _value;
            set => _value = value;
        }
    }

    // Represents different activities
    [Serializable]
    public class Activities
    {
        [SerializeField]
        private ActivityInfo _water;

        [JsonProperty("water")]
        public ActivityInfo Water
        {
            get => _water;
            set => _water = value;
        }

        [SerializeField]
        private ActivityInfo _feedAnimal;

        [JsonProperty("feedAnimal")]
        public ActivityInfo FeedAnimal
        {
            get => _feedAnimal;
            set => _feedAnimal = value;
        }

        [SerializeField]
        private ActivityInfo _usePesticide;

        [JsonProperty("usePesticide")]
        public ActivityInfo UsePesticide
        {
            get => _usePesticide;
            set => _usePesticide = value;
        }

        [SerializeField]
        private ActivityInfo _useFertilizer;

        [JsonProperty("useFertilizer")]
        public ActivityInfo UseFertilizer
        {
            get => _useFertilizer;
            set => _useFertilizer = value;
        }

        [SerializeField]
        private ActivityInfo _useHerbicide;

        [JsonProperty("useHerbicide")]
        public ActivityInfo UseHerbicide
        {
            get => _useHerbicide;
            set => _useHerbicide = value;
        }

        [SerializeField]
        private ActivityInfo _helpUseHerbicide;

        [JsonProperty("helpUseHerbicide")]
        public ActivityInfo HelpUseHerbicide
        {
            get => _helpUseHerbicide;
            set => _helpUseHerbicide = value;
        }

        [SerializeField]
        private ActivityInfo _helpUsePesticide;

        [JsonProperty("helpUsePesticide")]
        public ActivityInfo HelpUsePesticide
        {
            get => _helpUsePesticide;
            set => _helpUsePesticide = value;
        }

        [SerializeField]
        private ActivityInfo _helpUseFertilizer;

        [JsonProperty("helpWater")]
        public ActivityInfo HelpWater
        {
            get => _helpUseFertilizer;
            set => _helpUseFertilizer = value;
        }

        [SerializeField]
        private ActivityInfo _thiefCrop;

        [JsonProperty("thiefCrop")]
        public ActivityInfo ThiefCrop
        {
            get => _thiefCrop;
            set => _thiefCrop = value;
        }

        [SerializeField]
        private ActivityInfo _thiefAnimalProduct;

        [JsonProperty("thiefAnimalProduct")]
        public ActivityInfo ThiefAnimalProduct
        {
            get => _thiefAnimalProduct;
            set => _thiefAnimalProduct = value;
        }

        [SerializeField]
        private ActivityInfo _cureAnimal;

        [JsonProperty("cureAnimal")]
        public ActivityInfo CureAnimal
        {
            get => _cureAnimal;
            set => _cureAnimal = value;
        }

        [SerializeField]
        private ActivityInfo _helpCureAnimal;

        [JsonProperty("helpCureAnimal")]
        public ActivityInfo HelpCureAnimal
        {
            get => _helpCureAnimal;
            set => _helpCureAnimal = value;
        }

        [SerializeField]
        private ActivityInfo _harvestCrop;

        [JsonProperty("harvestCrop")]
        public ActivityInfo HarvestCrop
        {
            get => _harvestCrop;
            set => _harvestCrop = value;
        }
    }

    // Represents activity info containing experience gained and energy consumed
    [Serializable]
    public class ActivityInfo
    {
        [SerializeField]
        private int _experiencesGain;

        [JsonProperty("experiencesGain")]
        public int ExperiencesGain
        {
            get => _experiencesGain;
            set => _experiencesGain = value;
        }

        [SerializeField]
        private int _energyConsume;

        [JsonProperty("energyConsume")]
        public int EnergyConsume
        {
            get => _energyConsume;
            set => _energyConsume = value;
        }
    }

    // Represents crop randomness values
    [Serializable]
    public class CropRandomness
    {
        [SerializeField]
        private int thief3;

        [JsonProperty("thief3")]
        public int Thief3
        {
            get => thief3;
            set => thief3 = value;
        }

        [SerializeField]
        private int thief2;

        [JsonProperty("thief2")]
        public int Thief2
        {
            get => thief2;
            set => thief2 = value;
        }

        [SerializeField]
        private int needWater;

        [JsonProperty("needWater")]
        public int NeedWater
        {
            get => needWater;
            set => needWater = value;
        }

        [SerializeField]
        private int isWeedyOrInfested;

        [JsonProperty("isWeedyOrInfested")]
        public int IsWeedyOrInfested
        {
            get => isWeedyOrInfested;
            set => isWeedyOrInfested = value;
        }
    }

    // Represents animal randomness values
    [Serializable]
    public class AnimalRandomness
    {
        [SerializeField]
        private int _sickChance;

        [JsonProperty("sickChance")]
        public int SickChance
        {
            get => _sickChance;
            set => _sickChance = value;
        }

        [SerializeField]
        private int _thief3;

        [JsonProperty("thief3")]
        public int Thief3
        {
            get => _thief3;
            set => _thief3 = value;
        }

        [SerializeField]
        private int _thief2;

        [JsonProperty("thief2")]
        public int Thief2
        {
            get => _thief2;
            set => _thief2 = value;
        }
    }

    // Represents positions such as starter tiles and home
    [Serializable]
    public class Positions
    {
        [SerializeField]
        private List<Position> _tiles;

        [JsonProperty("tiles")]
        public List<Position> Tiles
        {
            get => _tiles;
            set => _tiles = value;
        }

        [SerializeField]
        private Position _home;

        [JsonProperty("home")]
        public Position Home
        {
            get => _home;
            set => _home = value;
        }
    }

    // Represents the starter info with golds and positions
    [Serializable]
    public class Starter
    {
        [SerializeField]
        private int _golds;

        [JsonProperty("golds")]
        public int Golds
        {
            get => _golds;
            set => _golds = value;
        }

        [SerializeField]
        private Positions _positions;

        [JsonProperty("positions")]
        public Positions Positions
        {
            get => _positions;
            set => _positions = value;
        }
    }

    // Represents spin info, including appearance chance slots
    [Serializable]
    public class SpinInfo
    {
        private Dictionary<AppearanceChance, SlotInfo> _appearanceChanceSlots;

        [JsonProperty("appearanceChanceSlots")]
        public Dictionary<AppearanceChance, SlotInfo> AppearanceChanceSlots
        {
            get => _appearanceChanceSlots;
            set => _appearanceChanceSlots = value;
        }
    }

    // Represents slot information in a spin
    [Serializable]
    public class SlotInfo
    {
        [SerializeField]
        private int _count;

        [JsonProperty("count")]
        public int Count
        {
            get => _count;
            set => _count = value;
        }

        [SerializeField]
        private int thresholdMin;

        [JsonProperty("thresholdMin")]
        public int ThresholdMin
        {
            get => thresholdMin;
            set => thresholdMin = value;
        }

        [SerializeField]
        private int thresholdMax;

        [JsonProperty("thresholdMax")]
        public int ThresholdMax
        {
            get => thresholdMax;
            set => thresholdMax = value;
        }
    }

    // Represents energy regeneration time in milliseconds
    [Serializable]
    public class EnergyRegenTime
    {
        [SerializeField]
        private int _time;

        [JsonProperty("time")]
        public int Time
        {
            get => _time;
            set => _time = value;
        }
    }

    // Represents Position data structure (placeholder class)
    [Serializable]
    public class Position
    {
        [SerializeField]
        private int _x;

        [JsonProperty("x")]
        public int X
        {
            get => _x;
            set => _x = value;
        }

        [SerializeField]
        private int _y;

        [JsonProperty("y")]
        public int Y
        {
            get => _y;
            set => _y = value;
        }
    }
}

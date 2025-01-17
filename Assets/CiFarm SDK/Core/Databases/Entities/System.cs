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
        [SerializeField]
        private string _value;

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

        [SerializeField]
        private ActivityInfo _feedAnimal;

        [SerializeField]
        private ActivityInfo _usePesticide;

        [SerializeField]
        private ActivityInfo _useFertilizer;

        [SerializeField]
        private ActivityInfo _useHerbicide;

        [SerializeField]
        private ActivityInfo _helpUseHerbicide;

        [SerializeField]
        private ActivityInfo _helpUsePesticide;

        [SerializeField]
        private ActivityInfo _helpWater;

        [SerializeField]
        private ActivityInfo _thiefCrop;

        [SerializeField]
        private ActivityInfo _thiefAnimalProduct;

        [SerializeField]
        private ActivityInfo _cureAnimal;

        [SerializeField]
        private ActivityInfo _helpCureAnimal;

        [SerializeField]
        private ActivityInfo _harvestCrop;

        [JsonProperty("water")]
        public ActivityInfo Water
        {
            get => _water;
            set => _water = value;
        }

        [JsonProperty("feedAnimal")]
        public ActivityInfo FeedAnimal
        {
            get => _feedAnimal;
            set => _feedAnimal = value;
        }

        [JsonProperty("usePesticide")]
        public ActivityInfo UsePesticide
        {
            get => _usePesticide;
            set => _usePesticide = value;
        }

        [JsonProperty("useFertilizer")]
        public ActivityInfo UseFertilizer
        {
            get => _useFertilizer;
            set => _useFertilizer = value;
        }

        [JsonProperty("useHerbicide")]
        public ActivityInfo UseHerbicide
        {
            get => _useHerbicide;
            set => _useHerbicide = value;
        }

        [JsonProperty("helpUseHerbicide")]
        public ActivityInfo HelpUseHerbicide
        {
            get => _helpUseHerbicide;
            set => _helpUseHerbicide = value;
        }

        [JsonProperty("helpUsePesticide")]
        public ActivityInfo HelpUsePesticide
        {
            get => _helpUsePesticide;
            set => _helpUsePesticide = value;
        }

        [JsonProperty("helpWater")]
        public ActivityInfo HelpWater
        {
            get => _helpWater;
            set => _helpWater = value;
        }

        [JsonProperty("thiefCrop")]
        public ActivityInfo ThiefCrop
        {
            get => _thiefCrop;
            set => _thiefCrop = value;
        }

        [JsonProperty("thiefAnimalProduct")]
        public ActivityInfo ThiefAnimalProduct
        {
            get => _thiefAnimalProduct;
            set => _thiefAnimalProduct = value;
        }

        [JsonProperty("cureAnimal")]
        public ActivityInfo CureAnimal
        {
            get => _cureAnimal;
            set => _cureAnimal = value;
        }

        [JsonProperty("helpCureAnimal")]
        public ActivityInfo HelpCureAnimal
        {
            get => _helpCureAnimal;
            set => _helpCureAnimal = value;
        }

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

        [SerializeField]
        private int _energyConsume;

        [JsonProperty("experiencesGain")]
        public int ExperiencesGain
        {
            get => _experiencesGain;
            set => _experiencesGain = value;
        }

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
        private int _thief3;

        [SerializeField]
        private int _thief2;

        [SerializeField]
        private int _needWater;

        [SerializeField]
        private int _isWeedyOrInfested;

        [JsonProperty("thief3")]
        public int Thief3
        {
            get => _thief3;
            set => _thief3 = value;
        }

        [JsonProperty("thief2")]
        public int Thief2
        {
            get => _thief2;
            set => _thief2 = value;
        }

        [JsonProperty("needWater")]
        public int NeedWater
        {
            get => _needWater;
            set => _needWater = value;
        }

        [JsonProperty("isWeedyOrInfested")]
        public int IsWeedyOrInfested
        {
            get => _isWeedyOrInfested;
            set => _isWeedyOrInfested = value;
        }
    }

    // Represents animal randomness values
    [Serializable]
    public class AnimalRandomness
    {
        [SerializeField]
        private int _sickChance;

        [SerializeField]
        private int _thief3;

        [SerializeField]
        private int _thief2;

        [JsonProperty("sickChance")]
        public int SickChance
        {
            get => _sickChance;
            set => _sickChance = value;
        }

        [JsonProperty("thief3")]
        public int Thief3
        {
            get => _thief3;
            set => _thief3 = value;
        }

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

        [SerializeField]
        private Position _home;

        [JsonProperty("tiles")]
        public List<Position> Tiles
        {
            get => _tiles;
            set => _tiles = value;
        }

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

        [SerializeField]
        private Positions _positions;

        [JsonProperty("golds")]
        public int Golds
        {
            get => _golds;
            set => _golds = value;
        }

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
        [SerializeField]
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

        [SerializeField]
        private int _thresholdMin;

        [SerializeField]
        private int _thresholdMax;

        [JsonProperty("count")]
        public int Count
        {
            get => _count;
            set => _count = value;
        }

        [JsonProperty("thresholdMin")]
        public int ThresholdMin
        {
            get => _thresholdMin;
            set => _thresholdMin = value;
        }

        [JsonProperty("thresholdMax")]
        public int ThresholdMax
        {
            get => _thresholdMax;
            set => _thresholdMax = value;
        }
    }

    // Represents energy regeneration time in milliseconds
    [Serializable]
    public class EnergyRegen
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

        [SerializeField]
        private int _y;

        [JsonProperty("x")]
        public int X
        {
            get => _x;
            set => _x = value;
        }

        [JsonProperty("y")]
        public int Y
        {
            get => _y;
            set => _y = value;
        }
    }
}

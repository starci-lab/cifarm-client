using System;
using System.Reflection;
using CiFarm.Scripts.Utilities;

namespace CiFarm.Scripts.Services.NakamaServices
{
   
    public enum CollectionType
    {
        [EnumStringValue("Crops")]
        Crops,

        [EnumStringValue("Animals")]
        Animals,    
        [EnumStringValue("Buildings")]
        Buildings,

        [EnumStringValue("Tiles")]
        Tiles,

        [EnumStringValue("System")]
        System,

        [EnumStringValue("Inventories")]
        Inventories,

        [EnumStringValue("Config")]
        Config
    }
    public enum ConfigKey
    {
        [EnumStringValue("visitState")]
        VisitState,
        [EnumStringValue("playerStats")]
        PlayerStats,
        [EnumStringValue("metadata")]
        Metadata,
    }

    public enum SystemKey
    {
        [EnumStringValue("matchInfo")]
        MatchInfo,
        [EnumStringValue("activities")]
        Activities,
        [EnumStringValue("rewards")]
        Rewards,
    }
}
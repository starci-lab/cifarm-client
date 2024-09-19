using System;
using System.Reflection;
using CiFarm.Scripts.Utilities;

namespace CiFarm.Scripts.Services.NakamaServices
{
   
    public enum CollectionType
    {
        [EnumStringValue("Seeds")]
        Seeds,

        [EnumStringValue("Animals")]
        Animals,

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
        [EnumStringValue("centralMatchInfo")]
        CentralMatchInfo
    }
}
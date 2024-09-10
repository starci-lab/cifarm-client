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
        System
    }

    public enum SystemKey
    {
        [EnumStringValue("centralMatchInfo")]
        CentralMatchInfo
    }
}
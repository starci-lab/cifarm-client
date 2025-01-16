using System;
using System.Collections.Generic;
using CiFarm.Core.Databases;
using Newtonsoft.Json;
using UnityEngine;

[Serializable] // Makes the class serializable for Unity
public class BuildingEntity : StringAbstractEntity
{
    // Private backing fields with SerializeField for Unity serialization
    [SerializeField]
    private bool _availableInShop;

    [SerializeField]
    private AnimalType? _type;

    [SerializeField]
    private int _maxUpgrade;

    [SerializeField]
    private int? _price;

    [SerializeField]
    private string _placedItemTypeId;

    [SerializeField]
    private List<string> _upgradeIds;

    // Public properties with getters and setters
    [JsonProperty("availableInShop")] // Custom JSON property name
    public bool AvailableInShop
    {
        get => _availableInShop;
        set => _availableInShop = value;
    }

    [JsonProperty("type")] // Custom JSON property name
    public AnimalType? Type
    {
        get => _type;
        set => _type = value;
    }

    [JsonProperty("maxUpgrade")] // Custom JSON property name
    public int MaxUpgrade
    {
        get => _maxUpgrade;
        set => _maxUpgrade = value;
    }

    [JsonProperty("price")] // Custom JSON property name
    public int? Price
    {
        get => _price;
        set => _price = value;
    }

    [JsonProperty("placedItemTypeId")] // Custom JSON property name
    public string PlacedItemTypeId
    {
        get => _placedItemTypeId;
        set => _placedItemTypeId = value;
    }

    [JsonProperty("upgradeIds")] // Custom JSON property name
    public List<string> UpgradeIds
    {
        get => _upgradeIds;
        set => _upgradeIds = value;
    }
}
